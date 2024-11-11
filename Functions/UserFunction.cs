using WidgetCoUser.Models;
using WidgetCoUser.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace WidgetCoUser.Functions
{
    public class UserFunction
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserFunction> logger;

        public UserFunction(IUserService userService, ILogger<UserFunction> log)
        {
            _userService = userService;
            logger = log;
        }

        [Function ("GetUserById")]
        public async Task<IActionResult> GetUserById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id}")] HttpRequestData req,
            string id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                if (user == null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting user by id");

                return new BadRequestObjectResult("Error getting user by id");
            }
        }

        [Function("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequestData req)
        {
            try
            {
                var users = await _userService.GetAllAsync();

                return new OkObjectResult(users);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all users");

                return new BadRequestObjectResult("Error getting all users");
            }
        }

        [Function("CreateUser")]
        public async Task<IActionResult> CreateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")] HttpRequestData req)
        {
            var userJson = await req.ReadAsStringAsync();

            try
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);

                var createdUser = await _userService.CreateAsync(user);

                return new OkObjectResult(createdUser);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating user");

                return new BadRequestObjectResult("Error creating user");
            }
        }

        [Function("UpdateUser")]
        public async Task<IActionResult> UpdateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/{id}")] HttpRequestData req,
            string id)
        {
            var userJson = await req.ReadAsStringAsync();

            try
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);

                user.Id = id;

                var updatedUser = await _userService.UpdateAsync(user);

                return new OkObjectResult(updatedUser);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating user");

                return new BadRequestObjectResult("Error updating user");
            }
        }

        [Function("DeleteUser")]
        public async Task<IActionResult> DeleteUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "users/{id}")] HttpRequestData req,
            string id)
        {
            try
            {
                await _userService.DeleteAsync(id);

                return new OkResult();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting user");

                return new BadRequestObjectResult("Error deleting user");
            }
        }
    }
}
