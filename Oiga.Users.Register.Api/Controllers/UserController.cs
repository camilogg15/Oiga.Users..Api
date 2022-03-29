using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Oiga.Users.Register.Api.DBContext;
using Oiga.Users.Register.Api.Models;
using Oiga.Users.Register.Api.Repository;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Oiga.Users.Register.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly DaprClient _daprClient;
        public UserController(IUserRepository userRepository, DaprClient daprClient)
        {
            _userRepository = userRepository;
            _daprClient = daprClient;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <returns>the newly created user</returns>
        [HttpPost]
        public async Task<string> Post([FromBody] User userRequest)
        {
            try
            {
                await _userRepository.InsertUserAsync(userRequest);
                //var message = new HttpRequestMessage(HttpMethod.Post, "/Search");
                //await _daprClient.InvokeMethodAsync<IActionResult>(message, "Oiga.Users.Search.Api", );

                //var request = _daprClient.CreateInvokeMethodRequest("search_app", "Search", userRequest);
                //request.Method = HttpMethod.Post;
                //var fullname = await _daprClient.InvokeMethodAsync<string>(request);

                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken cancellationToken = source.Token;
                var result = _daprClient.CreateInvokeMethodRequest(HttpMethod.Post, "oiga-users-search-api", "Create", cancellationToken);
                var fullname = await _daprClient.InvokeMethodAsync<string>(result);

                //var result = await _daprClient.InvokeMethodAsync(
                //                         HttpMethod.Get,
                //                         "Oiga.Users.Search.Api",
                //                         "Create");

                

                //return Ok(userRequest);
                return fullname;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
