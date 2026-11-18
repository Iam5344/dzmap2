using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using UserManager.DTOs;
using Xunit;

namespace UserManager.Tests
{
    public class UsersControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UsersControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
 
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetUsers_ReturnsSuccessStatusCode()
        {
      
            var response = await _client.GetAsync("/api/users");

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
        {
          
            var response = await _client.GetAsync("/api/users/99999");

    
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateUser_ReturnsBadRequest_WhenModelIsInvalid()
        {
          
            var invalidDto = new UserCreateDto
            {
                Username = "ab", )
                Email = "wrong-email", 
            };


            var response = await _client.PostAsJsonAsync("/api/users", invalidDto);
t
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
