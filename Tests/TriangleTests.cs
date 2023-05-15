using AreaCalculator;
using FluentAssertions;
using NUnit.Framework;

namespace Tests;

internal class TriangleTests
{
    [Test]
    public void CalculateArea_RightTriangle_ReturnsCorrectArea()
    {
        double sideA = 3;
        double sideB = 4;
        double sideC = 5;
        double expectedArea = 6;

        Triangle triangle = new Triangle(sideA, sideB, sideC);
        
        double actualArea = triangle.CalculateArea();
        
        actualArea.Should().Be(expectedArea);
    }

    [Test]
    public void CalculateArea_NonRightTriangle_ReturnsCorrectArea()
    {
        double sideA = 5;
        double sideB = 6;
        double sideC = 7;
        double expectedArea = 14.696938456699069;

        Triangle triangle = new Triangle(sideA, sideB, sideC);
        
        double actualArea = triangle.CalculateArea();
        
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
        Action action = () => new Triangle(sideA, sideB, sideC);
        
        action.Should().Throw<FluentValidation.ValidationException>()
            .WithMessage(errorMessage);
    }

    [Test]
    public void CalculateArea_RealyBigSides_ReturnsInfinity()
    {
        double sideA = Double.MaxValue;
        double sideB = Double.MaxValue;
        double sideC = Double.MaxValue;

        Triangle triangle = new Triangle(sideA, sideB, sideC);

        double actualArea = triangle.CalculateArea();

        actualArea.Should().Be(double.PositiveInfinity);
    }
}