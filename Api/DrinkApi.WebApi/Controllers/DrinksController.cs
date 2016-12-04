using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DrinkApi.Common.Interfaces;
using DrinkApi.Common.Models;
using DrinkApi.WebApi.Authentication;

namespace DrinkApi.WebApi.Controllers
{
    public class DrinksController : ApiController
    {
        private readonly IDrinkRepository _repository;

        public DrinksController(IDrinkRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Auth]
        [Route("drinks/{name}")]
        public HttpResponseMessage Get(string name)
        {
            var result = _repository.Get(name);
            return CreateResponse(result, null, HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Auth]
        [Route("drinks")]
        public DrinkList Get()
        {
            var list = _repository.GetAll().ToList();
            var result = new DrinkList
            {
                Count = list.Count,
                Data = list
            };
            return result;
        }

        [HttpPost]
        [Auth]
        [Route("drinks")]
        public HttpResponseMessage Post([FromBody]Drink newEntry)
        {
            var result = _repository.Create(newEntry);
            return CreateResponse(result, null, HttpStatusCode.Conflict);
        }

        [HttpPut]
        [Auth]
        [Route("drinks")]
        public HttpResponseMessage Put([FromBody]Drink newValue)
        {
            var result = _repository.Update(newValue);
            var response = result != null ? new {Message = "Ok"} : null;
            return CreateResponse(response, null, HttpStatusCode.NotFound);
        }

        [HttpDelete]
        [Auth]
        [Route("drinks/{name}")]
        public HttpResponseMessage Delete(string name)
        {
            var result = _repository.Delete(name);
            var response = result == true ? new {Message = "Ok"} : null;
            return CreateResponse(response, null, HttpStatusCode.NotFound);
        }

        private HttpResponseMessage CreateResponse(object result, object errorValue, HttpStatusCode errorStatus)
        {
            if (result == errorValue) return Request.CreateResponse(errorStatus);
            else return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
