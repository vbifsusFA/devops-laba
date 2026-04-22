[Fact]
public async Task Register_ReturnsBadRequest_WhenPasswordIsEmpty()
{
    // Arrange
    var request = new RegisterRequest("test@example.com", "");

    // Act
    var response = await _client.PostAsJsonAsync("/api/v1/auth/register", request);

    // Assert ✅ ПРАВИЛЬНО: ожидаем 400 Bad Request
    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
}