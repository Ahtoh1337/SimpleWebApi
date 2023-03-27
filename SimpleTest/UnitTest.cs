using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SimpleWebApi;

namespace SimpleTest
{
    public class UnitTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UnitTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/todoitems")]
        [InlineData("/todoitems/1")]
        [InlineData("/todoitems/complete")]
        public async Task TestTodoitemsGet(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestTodoitemsGetByIdNotFound()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/todoitems/2");

            Assert.Equal("NotFound", response.StatusCode.ToString());
        }

        [Fact]
        public async Task TestTodoitemsPost()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsJsonAsync("/todoitems", new Todo() { Name = "New todo", IsComplete = true});

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestTodoitemsPut()
        {
            var client = _factory.CreateClient();

            var response = await client.PutAsJsonAsync("/todoitems/1", new Todo() { Name = "Changed", IsComplete = false});

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestTodoitemsDelete()
        {
            var client = _factory.CreateClient();

            await client.PostAsJsonAsync("/todoitems", new Todo() { Name = "To delete", IsComplete = true });

            var response = await client.DeleteAsync("/todoitems/2");

            response.EnsureSuccessStatusCode();
        }
    }
}