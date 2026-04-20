[Fact]
public async Task Register_ReturnsOk_WhenPasswordIsEmpty()
{
    var response = await _client.PostAsJsonAsync("/api/v1/auth/register",
        new RegisterRequest("test@example.com", ""));
    
    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
}
