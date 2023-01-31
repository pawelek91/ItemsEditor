using Common.Dto;
using Common.Interfaces;
using Repository.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Exceptions;
using AutoMapper;

namespace Services
{
    public class ItemsService : IItemsService
    {
        readonly IMapper _mapper;
        readonly IItemsRepository _repository;
        public ItemsService(IItemsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateItem(ItemDto item)
        {
            if ((await _repository.Get(item.Code)) != null)
                throw new EntityAlreadyExistsException(item.Code);

            await _repository.Create(_mapper.Map<Data.Item.Item>(item));
        }

        public async Task<ItemDto> GetItem(string id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                throw new EntityNotFoundException(id);
            return _mapper.Map<ItemDto>(entity);
        }

        public async Task<IEnumerable<ItemDto>> GetItems(int? skip, int? page, ItemDtoFilter filter)
        {
            var entities = await _repository.Get(skip, page, filter);
            if (!entities.Any())
                return new ItemDto[0];

            return entities.Select(entity =>_mapper.Map<ItemDto>(entity));
        }

        public async Task UpdateItem(ItemDto item)
        {
            var entity = await _repository.Get(item.Code);

            if (entity == null)
                throw new EntityNotFoundException(item.Code);

            entity.Color = item.Color;
            entity.Name = item.Name;
            entity.Comments = item.Comments;

            await _repository.Update(entity);
        }
    }
}
