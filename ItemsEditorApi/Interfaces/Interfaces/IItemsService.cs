using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IItemsService
    {
        Task<IEnumerable<ItemDto>> GetItems(int? skip, int? page, ItemDtoFilter filter);
        Task<ItemDto> GetItem(string id);
        Task UpdateItem(ItemDto item);
        Task CreateItem(ItemDto item);
    }
}
