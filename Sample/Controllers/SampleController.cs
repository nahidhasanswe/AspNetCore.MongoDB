using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.MongoDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Sample.Entities;

namespace Sample.Controllers
{
    [Produces("application/json")]
    [Route("api/Sample")]
    public class SampleController : Controller
    {
        private readonly IMongoOperation<SampleModel> _operation;

        public SampleController(IMongoOperation<SampleModel> operation)
        {
            _operation = operation;
        }

        [HttpPost("Save")]
        public async Task Save(SampleModel model)
        {
            // You can use userName or use Identity
            model.CreatedBy = "AspNetCore.MongoDB";

            model.CreatedDate = DateTime.Now;

            await _operation.SaveAsync(model);
        }

        [HttpPut("Update/{id}")]
        public async Task Update(string id, SampleModel model)
        {
            model.UpdatedBy = "AspNetCore.MongoDB";

            model.UpdatedDate = DateTime.Now;

            await _operation.UpdateAsync(model.Id, model);
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<SampleModel>> GetAll()
        {
            return await _operation.GetAllAsync();
        }

        [HttpGet("Get/{id}")]
        public async Task<SampleModel> GetById(string Id)
        {
            return await _operation.GetByIdAsync(Id);
        }

        [HttpGet("remove/{id}")]
        public async Task<DeleteResult> RemoveOne(string id)
        {
            return await _operation.RemoveByIdAsync(id);
        }

        [HttpGet("removeAll")]
        public async Task<DeleteResult> RemoveAll()
        {
            return await _operation.RemoveAllAsync();
        }

        public object GetAsIQueryabale()
        {
            IQueryable<SampleModel> queryable = _operation.GetQuerableAsync();

            return queryable.Select(x => new {
                Name = x.Name,
                Address = x.Address
            });
        }

        public async Task<IEnumerable<SampleModel>> GetCollection()
        {
            var builder = Builders<SampleModel>.Filter;
            var filter = builder.Eq("KeyName", "SearchText");

            return await _operation.GetMongoCollection().Find(filter).ToListAsync();
        }
    }
}