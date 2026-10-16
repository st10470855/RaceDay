
using Xunit;

namespace RaceDay.API.Tests;

public class UnitTest1
{
    [Theory]
    [InlineData("Run")]
    [InlineData("Walk")]
    [InlineData("Cycle")]
    public void AllowedEventTypes_AreValid(string eventType)
    {
        // Arrange
        var allowedTypes = new[] { "Run", "Walk", "Cycle" };

        // Act
        var isValid = allowedTypes.Contains(eventType);

        // Assert
        Assert.True(isValid);
    }

    [Theory]
    [InlineData("Swim")]
    [InlineData("Drive")]
    [InlineData("")]
    public void UnsupportedEventTypes_AreInvalid(string eventType)
    {
        // Arrange
        var allowedTypes = new[] { "Run", "Walk", "Cycle" };

        // Act
        var isValid = allowedTypes.Contains(eventType);

        // Assert
        Assert.False(isValid);
    }
}
