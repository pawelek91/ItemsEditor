using Common.Dto;
using Common.Interfaces;
using ItemsEditorApi.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ItemsEditorApi.Controllers
{
    [ApiController]
    [CustomAuthorization]
    [Route("items")]
    public class ItemsController :  ControllerBase
    {
        IItemsService _service;
        public ItemsController(IItemsService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add([FromBody] ItemDto dto)
        {
            var validation = Validate(dto);
            if (!validation.isValid)
                return new BadRequestObjectResult(validation.errMessage);

            await _service.CreateItem(dto);
            return Created("items",dto.Code);
        }

        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromQuery]string code, [FromBody] ItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(code) || code != dto.Code)
                return new BadRequestObjectResult("Invalid code");

            var validation = Validate(dto);
            if (!validation.isValid)
                return new BadRequestObjectResult(validation.errMessage);

            await _service.UpdateItem(dto);
            return Ok();
        }

        [HttpGet("{itemId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
                return new BadRequestObjectResult(nameof(itemId));

            var result = await _service.GetItem(itemId);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int? skip, int? page, string? code, string? color, string? name)
        {

            var result = await _service.GetItems(skip,page,new ItemDtoFilter
            {
                CodeContains = code,
                NameContains = name,
                ColorName = color
            });
            return Ok(result);
        }
        private (bool isValid, string errMessage) Validate(ItemDto dto)
        {
            StringBuilder errors = new StringBuilder();
            bool isValid = true;

            if (dto == null)
            {
                errors.AppendLine("Invalid payload");
                isValid = false;
            }

            if (string.IsNullOrEmpty(dto.Code) || dto.Code.Length > 12)
            {
                errors.AppendLine("Invalid code");
                isValid = false;
            }

            if (string.IsNullOrEmpty(dto.Name) || dto.Name.Length > 200)
            {
                errors.AppendLine("Invalid name");
                isValid = false;
            }

            return new(isValid, isValid ? errors.ToString() : string.Empty);
        }
    }
}

