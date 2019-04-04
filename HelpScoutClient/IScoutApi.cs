using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelpScout
{
    public interface IScoutApi<T, in TCriteria> where TCriteria : ISearchQuery
    {
        Task<T> Get(string id);
        Task Create(T record);
        Task Delete(string id);

        Task<IList<T>> List(TCriteria criteria);
    }
}