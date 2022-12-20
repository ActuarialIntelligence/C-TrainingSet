using AI.Domain.ContainerObjects;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AI.Web.ZSpreadService.Controllers
{
    [Route("api/InMemoryObjectStore")]
    [ApiController]
    public class InMemoryObjectStoreController : ControllerBase
    {

        private ObjectStorePatternObject objectStorePatternObject;
        private ObjectByteStorePatternObject objectByteStorePatternObject;
        // GET: ReadWriteToInMemoryObject/Create
        [HttpPost("CreateString")]
        public ActionResult<ObjectStorePatternObject> Create(string data)
        {
            var stringRows = JsonSerializer.Deserialize<ObjectStorePatternObject>(data);
            objectStorePatternObject = stringRows;
            return stringRows;
        }
        [HttpPost("CreateByte")]
        public ActionResult<ObjectByteStorePatternObject> Create(byte[] bytes)
        {
            var byteRows = JsonSerializer.Deserialize<ObjectByteStorePatternObject>(bytes);
            objectByteStorePatternObject = byteRows;
            return byteRows;
        }
    }
}
