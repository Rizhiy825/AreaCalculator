using AreaCalculator;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    internal class CircleTests
    {
        [Test]
        public void CalculateArea_ValidRadius_ReturnsCorrectArea()
        {
            var radius = 5;
            var circle = new Circle(radius);
            var expectedArea = Math.PI * radius * radius;
            
            var actualArea = circle.CalculateArea();
            
            actualArea.Should().Be(expectedArea);
        }

        [TestCase(-5)]
        [TestCase(0)]
        public void CalculateArea_NegativeRadius_ThrowsArgumentException(int radius)
        {
            var action = () => new Circle(radius);

            action.Should().Throw<FluentValidation.ValidationException>()
                .WithMessage(@"
Validation failed: 
 -- Radius: 'Radius' должно быть больше '0'. Severity: Error");
        }

        [Test]
        public void CalculateArea_ReallyBigRadius_ReturnsInfinity()
        {
            var radius = double.MaxValue;
            var circle = new Circle(radius);

            var actualArea = circle.CalculateArea();

            actualArea.Should().Be(double.PositiveInfinity);
        }
    }
}