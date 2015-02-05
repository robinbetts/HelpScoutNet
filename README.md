# HelpScoutNet
HelpScoutNet is a .NET class library that provides an easy-to-use interface for the helpscout.net web api

##Implemented

###Help Desk API

* Conversation
    * List Conversations
    * Get Conversations
    * Get Attachment Data
* Mailboxes
    * List Mailboxes
    * Get Mailbox
    * Get Folders
* Search
    * Conversations
    * Customers
* Tags
    * List Tags
* Users
    * List Users
    * Get User
    * List Users ny Mailbox
* Workflows
    * List Workflows

##Examples 

###Initialization of the client
```csharp
var client = new HelpScoutClient(ApiKey);
```
###Search customers
```csharp

var client = new HelpScoutClient(ApiKey);
var customersSearch = client.SearchCustomers(new SearchRequest{ Query = "(customer:\"johnappleseed@gmail.com\")"});
foreach (var searchresult in customersSearch.Items)
{
    Console.WriteLine(searchresult.FirstName + searchresult.LastName);   
}

```

###List Mailboxes
```csharp

var mailboxes = client.ListMailboxes();
foreach (var mailboxStub in mailboxes.Items)
{    
    Console.WriteLine(mailboxStub.Id);
}
  
```

###Field Selectors 

Each endpoint returns a default set of fields based upon the given request. However, you can override this behavior by supplying one or more field selectors to explicitly request the data you need.

Instead of returning a complete customer object, you could return just the ID and lastname.

```csharp

client.GetCustomer(123, new CustomerRequest {Fields = new[] {"id", "lastName"}});
  
```
