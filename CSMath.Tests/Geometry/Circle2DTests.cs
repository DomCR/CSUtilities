using CSMath.Geometry;
using System.Linq;
using Xunit;

namespace CSMath.Tests.Geometry;

public class Circle2DTests
{
	[Fact]
	public void Constructor_WithCenterAndRadius_SetsPropertiesCorrectly()
	{
		// Arrange
		XY center = new XY(5.0, 10.0);
		double radius = 7.5;

		// Act
		Circle2D circle = new Circle2D(center, radius);

		// Assert
		Assert.Equal(center, circle.Center);
		Assert.Equal(radius, circle.Radius);
	}

	[Fact]
	public void Constructor_WithCoordinatesAndRadius_SetsPropertiesCorrectly()
	{
		// Arrange
		double centerX = 3.0;
		double centerY = 4.0;
		double radius = 5.0;

		// Act
		Circle2D circle = new Circle2D(centerX, centerY, radius);

		// Assert
		Assert.Equal(centerX, circle.Center.X);
		Assert.Equal(centerY, circle.Center.Y);
		Assert.Equal(radius, circle.Radius);
	}

	[Theory]
	[InlineData(0.0, 0.0, 1.0)]
	[InlineData(5.0, 5.0, 10.0)]
	[InlineData(-3.0, 4.0, 2.5)]
	public void Constructor_WithVariousValues_CreatesCorrectCircle(double x, double y, double radius)
	{
		// Act
		Circle2D circle1 = new Circle2D(x, y, radius);
		Circle2D circle2 = new Circle2D(new XY(x, y), radius);

		// Assert
		Assert.Equal(x, circle1.Center.X);
		Assert.Equal(y, circle1.Center.Y);
		Assert.Equal(radius, circle1.Radius);
		Assert.Equal(circle1.Center, circle2.Center);
		Assert.Equal(circle1.Radius, circle2.Radius);
	}

	[Fact]
	public void FindIntersections_WithDiagonalLine_ReturnsTwoIntersections()
	{
		// Arrange
		Circle2D circle = new Circle2D(0.0, 0.0, 5.0);
		Line2D line = new Line2D(new XY(-10.0, -10.0), new XY(1.0, 1.0)); // Diagonal line y=x

		// Act
		var intersections = circle.FindIntersections(line).ToArray();

		// Assert
		Assert.Equal(2, intersections.Length);

		// Both points should be on the circle (distance from center = radius)
		double distance1 = (intersections[0] - circle.Center).GetLength();
		double distance2 = (intersections[1] - circle.Center).GetLength();
		Assert.True(MathHelper.IsEqual(circle.Radius, distance1, 1e-10));
		Assert.True(MathHelper.IsEqual(circle.Radius, distance2, 1e-10));

		// Both points should satisfy y=x
		Assert.True(MathHelper.IsEqual(intersections[0].X, intersections[0].Y, 1e-10));
		Assert.True(MathHelper.IsEqual(intersections[1].X, intersections[1].Y, 1e-10));
	}

	[Fact]
	public void FindIntersections_WithLargeCircle_ReturnsPreciseIntersections()
	{
		// Arrange
		Circle2D circle = new Circle2D(0.0, 0.0, 1000.0);
		Line2D line = new Line2D(new XY(-2000.0, 0.0), new XY(1.0, 0.0)); // Horizontal line through center

		// Act
		var intersections = circle.FindIntersections(line).ToArray();

		// Assert
		Assert.Equal(2, intersections.Length);

		// Verify distances are equal to radius
		double distance1 = intersections[0].GetLength();
		double distance2 = intersections[1].GetLength();
		Assert.True(MathHelper.IsEqual(circle.Radius, distance1, 1e-8));
		Assert.True(MathHelper.IsEqual(circle.Radius, distance2, 1e-8));
	}

	[Fact]
	public void FindIntersections_WithLineNegativeDirection_ReturnsTwoIntersections()
	{
		// Arrange
		Circle2D circle = new Circle2D(0.0, 0.0, 5.0);
		Line2D line = new Line2D(new XY(0.0, 10.0), new XY(0.0, -1.0)); // Vertical line with negative direction

		// Act
		var intersections = circle.FindIntersections(line).ToArray();

		// Assert
		Assert.Equal(2, intersections.Length);

		// Both points should be at x=0
		Assert.True(MathHelper.IsZero(intersections[0].X, 1e-10));
		Assert.True(MathHelper.IsZero(intersections[1].X, 1e-10));

		// Both points should be on the circle
		double distance1 = intersections[0].GetLength();
		double distance2 = intersections[1].GetLength();
		Assert.True(MathHelper.IsEqual(circle.Radius, distance1, 1e-10));
		Assert.True(MathHelper.IsEqual(circle.Radius, distance2, 1e-10));
	}

	[Fact]
	public void FindIntersections_WithLineThroughCenter_ReturnsTwoIntersections()
	{
		// Arrange
		Circle2D circle = new Circle2D(0.0, 0.0, 5.0);
		Line2D line = new Line2D(new XY(-10.0, 0.0), new XY(1.0, 0.0)); // Horizontal line through center

		// Act
		var intersections = circle.FindIntersections(line).ToArray();

		// Assert
		Assert.Equal(2, intersections.Length);
		Assert.True(MathHelper.IsEqual(5.0, intersections[0].X, 1e-10) || MathHelper.IsEqual(-5.0, intersections[0].X, 1e-10));
		Assert.True(MathHelper.IsZero(intersections[0].Y, 1e-10));
		Assert.True(MathHelper.IsEqual(5.0, intersections[1].X, 1e-10) || MathHelper.IsEqual(-5.0, intersections[1].X, 1e-10));
		Assert.True(MathHelper.IsZero(intersections[1].Y, 1e-10));
	}

	[Fact]
	public void FindIntersections_WithNonIntersectingLine_ReturnsEmpty()
	{
		// Arrange
		Circle2D circle = new Circle2D(0.0, 0.0, 5.0);
		Line2D line = new Line2D(new XY(10.0, 0.0), new XY(0.0, 1.0)); // Vertical line at x=10, outside circle

		// Act
		var intersections = circle.FindIntersections(line);

		// Assert
		Assert.Empty(intersections);
	}

	[Fact]
	public void FindIntersections_WithOffsetCircle_ReturnsTwoIntersections()
	{
		// Arrange
		Circle2D circle = new Circle2D(3.0, 4.0, 5.0);
		Line2D line = new Line2D(new XY(3.0, -10.0), new XY(0.0, 1.0)); // Vertical line through center x=3

		// Act
		var intersections = circle.FindIntersections(line).ToArray();

		// Assert
		Assert.Equal(2, intersections.Length);

		// Both points should be on the vertical line x=3
		Assert.True(MathHelper.IsEqual(3.0, intersections[0].X, 1e-10));
		Assert.True(MathHelper.IsEqual(3.0, intersections[1].X, 1e-10));

		// Both points should be on the circle
		double distance1 = (intersections[0] - circle.Center).GetLength();
		double distance2 = (intersections[1] - circle.Center).GetLength();
		Assert.True(MathHelper.IsEqual(circle.Radius, distance1, 1e-10));
		Assert.True(MathHelper.IsEqual(circle.Radius, distance2, 1e-10));
	}

	[Fact]
	public void FindIntersections_WithSmallCircle_ReturnsPreciseIntersections()
	{
		// Arrange
		Circle2D circle = new Circle2D(0.0, 0.0, 0.1);
		Line2D line = new Line2D(new XY(-1.0, 0.0), new XY(1.0, 0.0)); // Horizontal line through center

		// Act
		var intersections = circle.FindIntersections(line).ToArray();

		// Assert
		Assert.Equal(2, intersections.Length);

		// Verify distances are equal to radius
		double distance1 = intersections[0].GetLength();
		double distance2 = intersections[1].GetLength();
		Assert.True(MathHelper.IsEqual(circle.Radius, distance1, 1e-10));
		Assert.True(MathHelper.IsEqual(circle.Radius, distance2, 1e-10));
	}

	[Fact]
	public void FindIntersections_WithTangentLine_ReturnsOneIntersection()
	{
		// Arrange
		Circle2D circle = new Circle2D(0.0, 0.0, 5.0);
		Line2D line = new Line2D(new XY(5.0, -10.0), new XY(0.0, 1.0)); // Vertical tangent line at x=5

		// Act
		var intersections = circle.FindIntersections(line).ToArray();

		// Assert
		Assert.Single(intersections);
		Assert.True(MathHelper.IsEqual(5.0, intersections[0].X, 1e-10));
		Assert.True(MathHelper.IsZero(intersections[0].Y, 1e-10));
	}
}