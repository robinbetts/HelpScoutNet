using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelpScout
{
    public interface IApiEndpoint<TDetail, in TCreateRequest, in TSearchQuery, TSearchListItem>
    {
        Task<TDetail> Get(long? id);
        Task<long> Create(TCreateRequest query);
        Task<IList<TSearchListItem>> Search(TSearchQuery query);
        Task Delete(long? id);
        Task Update(long? id, TCreateRequest req);
    }
}