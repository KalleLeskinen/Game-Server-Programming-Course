using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment_4.Controllers
{
    [Route("api/icontroller")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private readonly ILogger<ItemsController> _logger;
        private readonly IRepository _repo;


        public ItemsController(ILogger<ItemsController> logger, IRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }


        [HttpGet("Get/{id:Guid}/{itemId:Guid}")]
        public async Task<Item> GetItem(Guid id, Guid itemId)
        {
            return await _repo.GetItem(id, itemId);
        }


        [HttpGet("GetAll/{id:Guid}")]
        public async Task<Item[]> GetAll(Guid id)
        {
            return await _repo.GetAllItems(id);
        }

        [HttpPost("Create/{id:Guid}")]
        public async Task<Item> CreateItem(Guid id, [FromBody] NewItem item)
        {

            Item newItem = new Item
            {
                Id = Guid.NewGuid(),
                Type = item.Type,
                Level = item.Level,
                CreationTime = DateTime.UtcNow
            };

            return await _repo.CreateItem(id, newItem);
        }
        [HttpPost("Update/{id:Guid}")]
        public async Task<Item> UpdateItem(Guid itemId, [FromBody] Item item)
        {
            return await _repo.UpdateItem(itemId, item);
        }
        [HttpGet("Delete/{id:Guid}/{itemId:Guid}")]
        public async Task<Item> Delete(Guid id, Guid itemId)
        {

            Item toDelete = await _repo.GetItem(id, itemId);

            return await _repo.DeleteItem(id, toDelete);
        }

        public void Options() { }

    }
}

