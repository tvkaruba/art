using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ects.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        // GET: api/<ImageController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<ImageController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostAsync([FromBody] string value)
        {
            using var httpClient = new HttpClient();
            await using var stream = await httpClient.GetStreamAsync(new Uri(value.Trim('\"')));

            var blobUri = new Uri("https://" +
                                  "ectsstorage" +
                                  ".blob.core.windows.net/" +
                                  "images" +
                                  "/" + Guid.NewGuid() + ".jpg");

            var connectionString =
                "DefaultEndpointsProtocol=https;AccountName=ectsstorage;AccountKey=/Y/boiXp3Pu5WlN6Gl8PmS5Ml9T4BS8JcSNGw3WEWDJl8g3FDze2EhpUkEOJfzN4lXDLMhGHMxQ1i8vk9Hpu0w==;EndpointSuffix=core.windows.net";
            var storageCredentials =
                new StorageSharedKeyCredential("ectsstorage",
                    "/Y/boiXp3Pu5WlN6Gl8PmS5Ml9T4BS8JcSNGw3WEWDJl8g3FDze2EhpUkEOJfzN4lXDLMhGHMxQ1i8vk9Hpu0w==");

            var blobClient = new BlobClient(blobUri, storageCredentials);
            await blobClient.UploadAsync(stream, false);
            var result = blobUri.ToString();
            return Ok($"\"{result}\"");
        }

        // PUT api/<ImageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/<ImageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}