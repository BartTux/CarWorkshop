using Xunit;
using FluentAssertions;

namespace CarWorkshop.Domain.Entities.Tests;

public class CarWorkshopTests
{
    [Fact]
    public void EncodeName_ShouldSetEncodedName()
    {
        // Arrange
        var carWorkshop = new CarWorkshop { Name = "Our Super Car Workshop" };

        // Act
        carWorkshop.EncodeName();

        // Assert
        carWorkshop.EncodedName
            .Should()
            .Be("our-super-car-workshop");
    }

    [Fact]
    public void EncodeName_ShouldThrowException_WhenNameIsNull()
    {
        // Arrange
        var carWorkshop = new CarWorkshop();

        // Act
        Action encodeNameAction = () => carWorkshop.EncodeName();

        // Assert
        encodeNameAction
            .Invoking(a => a.Invoke())
            .Should()
            .Throw<NullReferenceException>();
    }
}