using CSMath.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSMath.Tests.Geometry;

public class Arc2DTests
{
	private const double Tolerance = 1e-10;

	[Fact]
	public void Arc2D_DefaultConstructor_CreatesZeroArc()
	{
		// Arrange & Act
		Arc2D arc = new Arc2D();

		// Assert
		Assert.Equal(0, arc.StartAngle);
		Assert.Equal(0, arc.EndAngle);
		Assert.Equal(0, arc.Radius);
		Assert.Equal(XY.Zero, arc.Center);
	}

	[Fact]
	public void Arc2D_Properties_CanBeSetAndRetrieved()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			StartAngle = MathHelper.DegToRad(45),
			EndAngle = MathHelper.DegToRad(135),
			Radius = 10.0,
			Center = new XY(5, 5)
		};

		// Assert
		Assert.Equal(MathHelper.DegToRad(45), arc.StartAngle, Tolerance);
		Assert.Equal(MathHelper.DegToRad(135), arc.EndAngle, Tolerance);
		Assert.Equal(10.0, arc.Radius, Tolerance);
		Assert.Equal(new XY(5, 5), arc.Center);
	}

	[Fact]
	public void FindIntersections_ArcWithOffsetCenter_ReturnsCorrectPoints()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = new XY(10, 10),
			Radius = 5.0,
			StartAngle = 0,
			EndAngle = MathHelper.TwoPI
		};

		// Horizontal line through the center
		Line2D line = new Line2D(new XY(0, 10), new XY(20, 0));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance).ToList();

		// Assert
		Assert.Equal(2, intersections.Count);
		Assert.Contains(intersections, p => Math.Abs(p.X - 15) < Tolerance && Math.Abs(p.Y - 10) < Tolerance);
		Assert.Contains(intersections, p => Math.Abs(p.X - 5) < Tolerance && Math.Abs(p.Y - 10) < Tolerance);
	}

	[Fact]
	public void FindIntersections_DiagonalLineThroughArc_ReturnsTwoPoints()
	{
		// Arrange - Full circle
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = 0,
			EndAngle = MathHelper.TwoPI
		};

		// Diagonal line passing through center (45 degree angle)
		Line2D line = new Line2D(new XY(-10, -10), new XY(20, 20));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance).ToList();

		// Assert
		Assert.Equal(2, intersections.Count);

		// Points should be at 45° and 225°
		double expectedCoord = 5 / Math.Sqrt(2);
		Assert.Contains(intersections, p =>
			Math.Abs(p.X - expectedCoord) < Tolerance &&
			Math.Abs(p.Y - expectedCoord) < Tolerance);
		Assert.Contains(intersections, p =>
			Math.Abs(p.X + expectedCoord) < Tolerance &&
			Math.Abs(p.Y + expectedCoord) < Tolerance);
	}

	[Fact]
	public void FindIntersections_FullCircle_LinePassesThrough_ReturnsTwoPoints()
	{
		// Arrange - Full circle (0° to 360°)
		Arc2D arc = new Arc2D
		{
			Center = new XY(5, 5),
			Radius = 3.0,
			StartAngle = 0,
			EndAngle = MathHelper.TwoPI
		};

		// Line passing through circle
		Line2D line = new Line2D(new XY(5, 0), new XY(0, 10));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance).ToList();

		// Assert
		Assert.Equal(2, intersections.Count);
		// Verify both points are at the correct distance from center
		foreach (var point in intersections)
		{
			double distance = Math.Sqrt(Math.Pow(point.X - 5, 2) + Math.Pow(point.Y - 5, 2));
			Assert.True(Math.Abs(distance - 3.0) < Tolerance);
		}
	}

	[Fact]
	public void FindIntersections_LargeRadius_ReturnsCorrectPoints()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 1000.0,
			StartAngle = 0,
			EndAngle = MathHelper.TwoPI
		};

		// Horizontal line through center
		Line2D line = new Line2D(new XY(-2000, 0), new XY(4000, 0));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance).ToList();

		// Assert
		Assert.Equal(2, intersections.Count);
		Assert.Contains(intersections, p => Math.Abs(p.X - 1000) < Tolerance && Math.Abs(p.Y) < Tolerance);
		Assert.Contains(intersections, p => Math.Abs(p.X + 1000) < Tolerance && Math.Abs(p.Y) < Tolerance);
	}

	[Fact]
	public void FindIntersections_LineIntersectsArcAtOnePoint_ReturnsOnePoint()
	{
		// Arrange - Arc from 45° to 135° (upper half, centered)
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = MathHelper.DegToRad(45),
			EndAngle = MathHelper.DegToRad(135)
		};

		// Vertical line through center at x = 0
		Line2D line = new Line2D(new XY(0, -10), new XY(0, 20));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance).ToList();

		// Assert
		// Should intersect at 90° (0, 5)
		Assert.Single(intersections);
		Assert.True(Math.Abs(intersections[0].X) < Tolerance);
		Assert.True(Math.Abs(intersections[0].Y - 5) < Tolerance);
	}

	[Fact]
	public void FindIntersections_LineIntersectsCircleButNotArc_ReturnsEmpty()
	{
		// Arrange - Arc from 0° to 90° (first quadrant only)
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = 0,
			EndAngle = MathHelper.HalfPI
		};

		// Horizontal line through center (would intersect at 180° and 0°)
		// But 180° is outside the arc range
		Line2D line = new Line2D(new XY(-10, 0), new XY(20, 0));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance).ToList();

		// Assert
		// Should only find the point at 0° (5, 0)
		Assert.Single(intersections);
		Assert.True(Math.Abs(intersections[0].X - 5) < Tolerance);
		Assert.True(Math.Abs(intersections[0].Y) < Tolerance);
	}

	[Fact]
	public void FindIntersections_LineMissesArc_ReturnsEmpty()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = 0,
			EndAngle = MathHelper.TwoPI
		};

		// Horizontal line far above the arc
		Line2D line = new Line2D(new XY(-10, 10), new XY(20, 0));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance);

		// Assert
		Assert.Empty(intersections);
	}

	[Fact]
	public void FindIntersections_LinePassesThroughArc_ReturnsTwoPoints()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = 0,
			EndAngle = Math.PI // Half circle (0° to 180°)
		};

		// Horizontal line passing through center
		Line2D line = new Line2D(new XY(-10, 0), new XY(20, 0));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance).ToList();

		// Assert
		Assert.Equal(2, intersections.Count);
		Assert.Contains(intersections, p => Math.Abs(p.X - 5) < Tolerance && Math.Abs(p.Y) < Tolerance);
		Assert.Contains(intersections, p => Math.Abs(p.X + 5) < Tolerance && Math.Abs(p.Y) < Tolerance);
	}

	[Fact]
	public void FindIntersections_LineTangentToArc_ReturnsOnePoint()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = 0,
			EndAngle = MathHelper.TwoPI // Full circle
		};

		// Horizontal line tangent at the top
		Line2D line = new Line2D(new XY(-10, 5), new XY(20, 0));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance).ToList();

		// Assert
		Assert.Single(intersections);
		Assert.True(Math.Abs(intersections[0].X) < Tolerance);
		Assert.True(Math.Abs(intersections[0].Y - 5) < Tolerance);
	}

	[Fact]
	public void FindIntersections_SmallArc_LineIntersectsOnce_ReturnsOnePoint()
	{
		// Arrange - Small arc from 80° to 100°
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 10.0,
			StartAngle = MathHelper.DegToRad(80),
			EndAngle = MathHelper.DegToRad(100)
		};

		// Vertical line at x = 0 (intersects at 90°)
		Line2D line = new Line2D(new XY(0, 0), new XY(0, 20));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance).ToList();

		// Assert
		Assert.Single(intersections);
		Assert.True(Math.Abs(intersections[0].X) < Tolerance);
		Assert.True(Math.Abs(intersections[0].Y - 10) < Tolerance);
	}

	[Fact]
	public void FindIntersections_VerticalLine_ReturnsCorrectPoints()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = 0,
			EndAngle = MathHelper.TwoPI
		};

		// Vertical line at x = 3
		Line2D line = new Line2D(new XY(3, -10), new XY(0, 20));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance).ToList();

		// Assert
		Assert.Equal(2, intersections.Count);

		// Calculate expected Y coordinates: sqrt(r^2 - x^2) = sqrt(25 - 9) = 4
		double expectedY = 4.0;
		Assert.Contains(intersections, p => Math.Abs(p.X - 3) < Tolerance && Math.Abs(p.Y - expectedY) < Tolerance);
		Assert.Contains(intersections, p => Math.Abs(p.X - 3) < Tolerance && Math.Abs(p.Y + expectedY) < Tolerance);
	}

	[Fact]
	public void FindIntersections_VerySmallRadius_ReturnsCorrectPoints()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 0.001,
			StartAngle = 0,
			EndAngle = MathHelper.TwoPI
		};

		// Horizontal line through center
		Line2D line = new Line2D(new XY(-1, 0), new XY(2, 0));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance).ToList();

		// Assert
		Assert.Equal(2, intersections.Count);
		Assert.Contains(intersections, p => Math.Abs(p.X - 0.001) < Tolerance && Math.Abs(p.Y) < Tolerance);
		Assert.Contains(intersections, p => Math.Abs(p.X + 0.001) < Tolerance && Math.Abs(p.Y) < Tolerance);
	}

	[Fact]
	public void FindIntersections_WrapAroundArc_ReturnsCorrectPoints()
	{
		// Arrange - Arc from 315° to 45° (wraps around 0°)
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = MathHelper.DegToRad(315),
			EndAngle = MathHelper.DegToRad(45)
		};

		// Horizontal line through center
		Line2D line = new Line2D(new XY(-10, 0), new XY(20, 0));

		// Act
		var intersections = arc.FindIntersections(line, Tolerance).ToList();

		// Assert
		// Should only intersect at 0° (5, 0), not at 180° (-5, 0)
		Assert.Single(intersections);
		Assert.True(Math.Abs(intersections[0].X - 5) < Tolerance);
		Assert.True(Math.Abs(intersections[0].Y) < Tolerance);
	}

	[Fact]
	public void FindIntersections_ZeroLengthLine_ReturnsEmpty()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = 0,
			EndAngle = MathHelper.TwoPI
		};

		// Zero-length line (point)
		Line2D line = new Line2D(new XY(5, 0), XY.Zero);

		// Act
		var intersections = arc.FindIntersections(line, Tolerance);

		// Assert
		Assert.Empty(intersections);
	}

	[Fact]
	public void InAngularRange_CenterOffsetArc_PointInRange_ReturnsTrue()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = new XY(10, 10),
			Radius = 5.0,
			StartAngle = 0,
			EndAngle = MathHelper.HalfPI
		};

		// Point at 45 degrees from center
		double angle = MathHelper.DegToRad(45);
		XY point = new XY(10 + 5 * Math.Cos(angle), 10 + 5 * Math.Sin(angle));

		// Act
		bool result = arc.InAngularRange(point);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void InAngularRange_PointAtEndAngle_ReturnsTrue()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = MathHelper.DegToRad(30),
			EndAngle = MathHelper.DegToRad(120)
		};

		double endAngle = MathHelper.DegToRad(120);
		XY point = new XY(5 * Math.Cos(endAngle), 5 * Math.Sin(endAngle));

		// Act
		bool result = arc.InAngularRange(point);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void InAngularRange_PointAtStartAngle_ReturnsTrue()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = MathHelper.DegToRad(30),
			EndAngle = MathHelper.DegToRad(120)
		};

		double startAngle = MathHelper.DegToRad(30);
		XY point = new XY(5 * Math.Cos(startAngle), 5 * Math.Sin(startAngle));

		// Act
		bool result = arc.InAngularRange(point);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void InAngularRange_PointOutsideRange_ReturnsFalse()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = 0,
			EndAngle = MathHelper.HalfPI // 90 degrees
		};

		// Point at 180 degrees
		XY point = new XY(-5, 0);

		// Act
		bool result = arc.InAngularRange(point);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void InAngularRange_PointWithinRange_ReturnsTrue()
	{
		// Arrange
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = 0,
			EndAngle = MathHelper.HalfPI // 90 degrees
		};

		// Point at 45 degrees
		XY point = new XY(5 * Math.Cos(MathHelper.DegToRad(45)), 5 * Math.Sin(MathHelper.DegToRad(45)));

		// Act
		bool result = arc.InAngularRange(point);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void InAngularRange_WrapAroundArc_PointInRange_ReturnsTrue()
	{
		// Arrange - Arc from 315° to 45° (wraps around 0°)
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = MathHelper.DegToRad(315),
			EndAngle = MathHelper.DegToRad(45)
		};

		// Point at 10 degrees (should be in range)
		double angle = MathHelper.DegToRad(10);
		XY point = new XY(5 * Math.Cos(angle), 5 * Math.Sin(angle));

		// Act
		bool result = arc.InAngularRange(point);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void InAngularRange_WrapAroundArc_PointOutsideRange_ReturnsFalse()
	{
		// Arrange - Arc from 315° to 45° (wraps around 0°)
		Arc2D arc = new Arc2D
		{
			Center = XY.Zero,
			Radius = 5.0,
			StartAngle = MathHelper.DegToRad(315),
			EndAngle = MathHelper.DegToRad(45)
		};

		// Point at 180 degrees (should be outside range)
		XY point = new XY(-5, 0);

		// Act
		bool result = arc.InAngularRange(point);

		// Assert
		Assert.False(result);
	}
}