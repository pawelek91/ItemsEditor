using Common.Dto;
using Data.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Items
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> Get(int? skip, int? take, ItemDtoFilter filter);
        Task<Item> Get(string code);
        Task Create(Item item);
        Task Update(Item item);
    }
}
