using Common.Dto;
using Data.Item;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Items
{
    public class ItemsRepository : IItemsRepository
    {
        readonly ItemsDbContext _dbContext;
        public ItemsRepository(ItemsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(Item item)
        {
            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Item>> Get(int? take, int? skip, ItemDtoFilter filter)
        {
            var query = _dbContext.Items.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(filter.ColorName))
                query = query.Where(x => x.Color == filter.ColorName);

            if (!string.IsNullOrWhiteSpace(filter.NameContains))
                query = query.Where(x => x.Name.Contains(filter.NameContains));

            if (!string.IsNullOrWhiteSpace(filter.CodeContains))
                query = query.Where(x => x.Code.Contains(filter.CodeContains));

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToArrayAsync();
        }

        public async Task<Item> Get(string code)
        {
            return await _dbContext.Items.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task Update(Item item)
        {
            _dbContext.Update(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
