using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Oiga.Users.Search.Api.Helpers;
using Oiga.Users.Search.Api.Models;
using Oiga.Users.Search.Api.Repository;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Oiga.Users.Search.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IUserSearchRepository _searchRepository;
        private readonly DaprClient _daprClient;
        public SearchController(IUserSearchRepository searchRepository, DaprClient daprClient)
        {
            _searchRepository = searchRepository;
            _daprClient = daprClient;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <returns>the newly created user</returns>
        [HttpPost]
        [Route("Create")]
        public async Task<string> Create([FromBody] User userRequest)
        {
            try
            {
                await _searchRepository.InsertUserAsync(userRequest);
                return $"{userRequest.FirstName} {userRequest.LastName}";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }            
        }

        [HttpGet]
        [Route("GetUsers/{name}/{pageNumber}")]
        public PagedHelper<User> GetUsers([FromRoute] string name, [FromRoute] int pageNumber = 1)
        {
            try
            {
               return _searchRepository.SearchUsers(name, pageNumber);
            }
            catch (System.Exception ex)
            {
                return new PagedHelper<User>(new List<User>(), 0, 0, 0);
            }
        }
    }
}
