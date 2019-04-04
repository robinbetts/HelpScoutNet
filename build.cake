//cake script :)
#addin "Cake.FileHelpers"
#tool "nuget:?package=GitVersion.CommandLine"
#tool "nuget:?package=GitReleaseNotes&version=0.7.1"
#load "./helpers.cake"



var target = Argument("Target", "Default");
string artifactsFolder = "./artifacts";
var nugetPush=false;

var projectFile = "./HelpScoutClient/HelpScoutClient.csproj";
var testProjectFile = "./HelpScoutClient.Tests/HelpScoutClient.Tests.csproj";
var testartifactDiractory = "./artifacts/tests/netcoreapp2.2";
var releaseDir = System.IO.Path.Combine(artifactsFolder, "release");
var projectName = "HelpScoutClient";


GitVersion gitVersion = null;



var testBuildSetting = new DotNetCoreBuildSettings
{
    Framework = "netcoreapp2.2",
    Configuration = "release",
    OutputDirectory = testartifactDiractory
};

Task("Restore")
    .Does(() => {
    DotNetCoreRestore();
    Information("Restore completed");
});

Task("Clean")
    .Does(() => {
    CleanDirectory("./artifacts");
    Information("cleaned artificat directory");
});


Task("Tests")
    .Does(() => {
    var settings = new DotNetCoreTestSettings()
    {
        Configuration = "release",
        Framework = "netcoreapp2.2",
        OutputDirectory = testartifactDiractory
    };
    Information("Running Tests...");
    DotNetCoreTest("./HelpScoutClient.Tests/HelpScoutClient.Tests.csproj", settings);

});

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() => {

    var coreBuildSettings = new DotNetCoreBuildSettings
    {
        Framework = "netstandard2.0",
        Configuration = "release",
        OutputDirectory = "./artifacts/lib/netstandard2.0",
    };

    Information("Building netstandard2.0 artifacts");
    DotNetCoreBuild(projectFile, coreBuildSettings);


    Information("Building net451 artifacts");

    var fxBuildSettings = new DotNetCoreBuildSettings
    {
        Framework = "net451",
        Configuration = "release",
        OutputDirectory = "./artifacts/lib/net451",
    };

    DotNetCoreBuild(projectFile, fxBuildSettings);
});



Task("Version")
    .Does(() =>   {
	gitVersion = GitVersion(new GitVersionSettings
    {
        UpdateAssemblyInfo = true
    });
	
});
    

Task("Pack")
.IsDependentOn("Version")
.WithCriteria(()=>gitVersion!=null)

   .Does(() => {
    var settings = new DotNetCorePackSettings
    {
        Configuration = "release",
        OutputDirectory = "./artifacts/pack",
		//Version is available when using msbuild settings
		//MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersion(gitVersion.NuGetVersion),
    };

    DotNetCorePack(projectFile, settings);

});

Task("Push")
.WithCriteria(() => AppVeyor.IsRunningOnAppVeyor && gitVersion.BranchName=="master")
.IsDependentOn("Build")
.IsDependentOn("Tests")
.IsDependentOn("Pack")
.Does(()=>{

	foreach (var package in GetFiles($"artifacts/pack/*.nupkg"))
    {
		Information("Uploading artifacts...");
		AppVeyor.UploadArtifact(package.FullPath);
		Information("Upload completed.");

		if(nugetPush)
		{
			if(AppVeyor.Environment.Repository.Tag.IsTag)
				{
					Information($"Tag Name: {AppVeyor.Environment.Repository.Tag.Name}");
					PushToNuget(package.FullPath);
				}
			else
			{
				if(gitVersion.BranchName=="master")
				{
					PushToMyget(package.FullPath);
				}
			}
		}


    }

});

Task("Default")
    .IsDependentOn("Push");

// Executes the task specified in the target argument.
RunTarget(target);



//helper functions

void PushToMyget(string packagePath)
{

	Information("Pushing Nuget Package to Myget");
	DotNetCoreNuGetPush(packagePath, new DotNetCoreNuGetPushSettings
	{
		Source = "https://www.myget.org/F/selz/api/v2/package",
		ApiKey = EnvironmentVariable("MYGET_APIKEY")
	});
	Information("Myget Push complete.");

}

void PushToNuget(string packagePath)
{

	Information("Pushing Nuget Package to nuget");
	DotNetCoreNuGetPush(packagePath, new DotNetCoreNuGetPushSettings
	{
		Source = "https://api.nuget.org/v3/index.json",
		ApiKey = EnvironmentVariable("NUGET_APIKEY")
	});
	Information("Nuget Push complete.");

}
