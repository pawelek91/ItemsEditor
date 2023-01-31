using AutoMapper;
using Common.Exceptions;
using Data.Item;
using Moq;
using Repository.Items;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{

    internal class ItemsServiceTests
    {
        Mock<IItemsRepository> _repository = new Mock<IItemsRepository>();
        Mock<IMapper> _mapper = new Mock<IMapper>();

        [Test]
        public void CreateItemWithUniqueIdShouldNotThrowError()
        {
            string uniqueId = "123";
            _repository.Setup(x => x.Get(uniqueId)).Returns(Task.FromResult<Item>(null));

            var service = new ItemsService(_repository.Object, _mapper.Object);
            
            Assert.DoesNotThrowAsync(async () =>
            {
                await service.CreateItem(new Common.Dto.ItemDto
                {
                    Code = uniqueId,
                    Color = "pink",
                    Comments = "",
                    Name = "test_item"
                });
            });
        }

        [Test]
        public void CreateItemWithNoUniqueIdShouldThrowError()
        {
            string uniqueId = "123";
            _repository.Setup(x => x.Get(uniqueId)).Returns(Task.FromResult<Item>(new Item { }));

            var service = new ItemsService(_repository.Object, _mapper.Object);

            Assert.ThrowsAsync<EntityAlreadyExistsException>(async () =>
            {
                await service.CreateItem(new Common.Dto.ItemDto
                {
                    Code = uniqueId,
                    Color = "pink",
                    Comments = "",
                    Name = "test_item"
                });
            });
        }

        [Test]
        public async Task GetItemShouldReturnExistingItem()
        {
            _repository.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<Item>(new Item { }));
            var service = new ItemsService(_repository.Object, _mapper.Object);
            var result = await service.GetItem("123");
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetItemShouldThrowErrosWhenItemDoesntExists()
        {
            _repository.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<Item>(null));
            var service = new ItemsService(_repository.Object, _mapper.Object);

            Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                var result = await service.GetItem("123");
            });
        }


        [Test]
        public async Task UpdateItemShouldUpdateExistingItem()
        {
            _repository.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<Item>(new Item { }));
            var service = new ItemsService(_repository.Object, _mapper.Object);
            Assert.DoesNotThrowAsync(async () =>
            {
                await service.UpdateItem(new Common.Dto.ItemDto { });
            });
        }


        [Test]
        public async Task UpdateItemShouldThrowErrosWhenItemDoesntExists()
        {
            _repository.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<Item>(null));
            var service = new ItemsService(_repository.Object, _mapper.Object);

            Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await service.UpdateItem(new Common.Dto.ItemDto { });
            });
        }
    }
}
