using AreaCalculator;
using FluentAssertions;
using NUnit.Framework;

namespace Tests;

internal class TriangleTests
{
    [Test]
    public void CalculateArea_RightTriangle_ReturnsCorrectArea()
    {
        var sideA = 3;
        var sideB = 4;
        var sideC = 5;
        var expectedArea = 6;

        var triangle = new Triangle(sideA, sideB, sideC);
        
        var actualArea = triangle.CalculateArea();
        
        actualArea.Should().Be(expectedArea);
    }

    [Test]
    public void CalculateArea_NonRightTriangle_ReturnsCorrectArea()
    {
        var sideA = 5;
        var sideB = 6;
        var sideC = 7;
        var expectedArea = 14.696938456699069;

        var triangle = new Triangle(sideA, sideB, sideC);
        
        var actualArea = triangle.CalculateArea();
        
        actualArea.Should().BeApproximately(expectedArea, 1E-10);
    }

    [TestCase(2, 2, 5, @"
Validation failed: \r\n -- : Invalid triangle sides! Severity: Error")]
    [TestCase(-2, 2, 5, @"
Validation failed: \r\n -- SideA: 
'Side A' должно быть больше '0'. Severity: Error\r\n -- : 
Invalid triangle sides! Severity: Error")]
    [TestCase(-2, 0, 5, @"
Validation failed: \r\n -- SideA: 
'Side A' должно быть больше '0'. Severity: Error\r\n -- SideB: 
'Side B' должно быть больше '0'. Severity: Error\r\n -- : 
Invalid triangle sides! Severity: Error")]
    [TestCase(0, 0, 0, @"
Validation failed: 
 -- SideA: 'Side A' должно быть больше '0'. Severity: Error
 -- SideB: 'Side B' должно быть больше '0'. Severity: Error
 -- SideC: 'Side C' должно быть больше '0'. Severity: Error
 -- : Invalid triangle sides! Severity: Error")]
    public void CalculateArea_InvalidTriangle_ThrowsArgumentException(int sideA, 
        int sideB, 
        int sideC, 
        string errorMessage)
    {
        var action = () => new Triangle(sideA, sideB, sideC);
        
        action.Should().Throw<FluentValidation.ValidationException>()
            .WithMessage(errorMessage);
    }

    [Test]
    public void CalculateArea_RealyBigSides_ReturnsInfinity()
    {
        var sideA = Double.MaxValue;
        var sideB = Double.MaxValue;
        var sideC = Double.MaxValue;

        var triangle = new Triangle(sideA, sideB, sideC);

        var actualArea = triangle.CalculateArea();

        actualArea.Should().Be(double.PositiveInfinity);
    }

    [TestCase(3, 4, 5)]
    [TestCase(3, 5, 4)]
    [TestCase(5, 4, 3)]
    public void IsRight_RightTriangle_ReturnsTrue(int sideA, int sideB, int sideC)
    {
        var triangle = new Triangle(sideA, sideB, sideC);

        triangle.IsRight.Should().BeTrue();
    }

    [TestCase(4, 4, 5)]
    [TestCase(2, 3, 4)]
    [TestCase(1, 1, 1)]
    public void IsRight_NotRightTriangle_ReturnsFalse(int sideA, int sideB, int sideC)
    {
        var triangle = new Triangle(sideA, sideB, sideC);

        triangle.IsRight.Should().BeFalse();
    }
}