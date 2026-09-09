using CSMath.Extensions;
using CSMath.Geometry;
using Xunit;

namespace CSMath.Tests.Geometry;

public class Segment2DTests
{
	[Fact]
	public void Constructor_WithValidPoints_CreatesSegment()
	{
		var origin = new XY(0, 0);
		var end = new XY(1, 1);

		var segment = new Segment2D(origin, end);

		Assert.Equal(origin, segment.Origin);
		Assert.Equal(end, segment.End);
	}

	[Fact]
	public void Constructor_WithZeroLengthSegment_CreatesSegment()
	{
		var point = new XY(5, 5);

		var segment = new Segment2D(point, point);

		Assert.Equal(point, segment.Origin);
		Assert.Equal(point, segment.End);
	}

	[Fact]
	public void Direction_ForZeroLengthSegment_ReturnsZeroVector()
	{
		var point = new XY(5, 5);
		var segment = new Segment2D(point, point);

		var direction = segment.Direction;

		Assert.Equal(XY.Zero, direction);
	}

	[Fact]
	public void Direction_ReturnsVectorFromOriginToEnd()
	{
		var origin = new XY(1, 2);
		var end = new XY(4, 6);
		var segment = new Segment2D(origin, end);

		var direction = segment.Direction;

		Assert.Equal(new XY(3, 4), direction);
	}

	[Fact]
	public void Direction_UpdatesWhenPropertiesChange()
	{
		var segment = new Segment2D(new XY(0, 0), new XY(1, 1));

		segment.End = new XY(2, 2);

		Assert.Equal(new XY(2, 2), segment.Direction);
	}

	[Fact]
	public void Direction_WithNegativeCoordinates_ReturnsCorrectVector()
	{
		var origin = new XY(-1, -2);
		var end = new XY(-4, -6);
		var segment = new Segment2D(origin, end);

		var direction = segment.Direction;

		Assert.Equal(new XY(-3, -4), direction);
	}

	[Fact]
	public void End_CanBeModified()
	{
		var segment = new Segment2D(new XY(0, 0), new XY(1, 1));
		var newEnd = new XY(3, 3);

		segment.End = newEnd;

		Assert.Equal(newEnd, segment.End);
	}

	[Fact]
	public void Equals_WithDifferentEnd_ReturnsFalse()
	{
		var segment1 = new Segment2D(new XY(0, 0), new XY(1, 1));
		var segment2 = new Segment2D(new XY(0, 0), new XY(2, 2));

		Assert.False(segment1.Equals(segment2));
	}

	[Fact]
	public void Equals_WithDifferentOrigin_ReturnsFalse()
	{
		var segment1 = new Segment2D(new XY(0, 0), new XY(1, 1));
		var segment2 = new Segment2D(new XY(1, 0), new XY(1, 1));

		Assert.False(segment1.Equals(segment2));
	}

	[Fact]
	public void Equals_WithSameOriginAndEnd_ReturnsTrue()
	{
		var segment1 = new Segment2D(new XY(0, 0), new XY(1, 1));
		var segment2 = new Segment2D(new XY(0, 0), new XY(1, 1));

		Assert.True(segment1.Equals(segment2));
	}

	[Fact]
	public void FindIntersection_WithCollinearSegments_ReturnsNaN()
	{
		// Two collinear segments on the same line
		var segment1 = new Segment2D(new XY(0, 0), new XY(2, 0));
		var segment2 = new Segment2D(new XY(1, 0), new XY(3, 0));

		var intersection = segment1.FindIntersection(segment2);

		Assert.True(intersection.IsNaN());
	}

	[Fact]
	public void FindIntersection_WithDiagonalSegments_ReturnsIntersectionPoint()
	{
		// Segment from (0, 0) to (2, 2)
		var segment1 = new Segment2D(new XY(0, 0), new XY(2, 2));
		// Segment from (0, 2) to (2, 0)
		var segment2 = new Segment2D(new XY(0, 2), new XY(2, 0));

		var intersection = segment1.FindIntersection(segment2);

		Assert.Equal(new XY(1, 1), intersection);
	}

	[Fact]
	public void FindIntersection_WithIntersectingSegment_ReturnsIntersectionPoint()
	{
		// Horizontal segment from (0, 1) to (2, 1)
		var segment1 = new Segment2D(new XY(0, 1), new XY(2, 1));
		// Vertical segment from (1, 0) to (1, 2)
		var segment2 = new Segment2D(new XY(1, 0), new XY(1, 2));

		var intersection = segment1.FindIntersection(segment2);

		Assert.Equal(new XY(1, 1), intersection);
	}

	[Fact]
	public void FindIntersection_WithIntersectionAtEndpoint_ReturnsIntersectionPoint()
	{
		// Segment from (0, 0) to (2, 0)
		var segment1 = new Segment2D(new XY(0, 0), new XY(2, 0));
		// Segment from (2, -1) to (2, 1)
		var segment2 = new Segment2D(new XY(2, -1), new XY(2, 1));

		var intersection = segment1.FindIntersection(segment2);

		Assert.Equal(new XY(2, 0), intersection);
	}

	[Fact]
	public void FindIntersection_WithIntersectionJustOutsideBounds_ReturnsNaN()
	{
		// Segment from (0, 0) to (1, 0)
		var segment1 = new Segment2D(new XY(0, 0), new XY(1, 0));
		// Segment from (2, -1) to (2, 1)
		var segment2 = new Segment2D(new XY(2, -1), new XY(2, 1));

		var intersection = segment1.FindIntersection(segment2);

		Assert.True(intersection.IsNaN());
	}

	[Fact]
	public void FindIntersection_WithIntersectionOutsideSegmentBounds_ReturnsNaN()
	{
		// Segment from (0, 1) to (1, 1)
		var segment1 = new Segment2D(new XY(0, 1), new XY(1, 1));
		// Vertical segment from (2, 0) to (2, 2)
		var segment2 = new Segment2D(new XY(2, 0), new XY(2, 2));

		var intersection = segment1.FindIntersection(segment2);

		Assert.True(intersection.IsNaN());
	}

	[Fact]
	public void FindIntersection_WithParallelSegments_ReturnsNaN()
	{
		// Two parallel horizontal segments
		var segment1 = new Segment2D(new XY(0, 1), new XY(2, 1));
		var segment2 = new Segment2D(new XY(0, 2), new XY(2, 2));

		var intersection = segment1.FindIntersection(segment2);

		Assert.True(intersection.IsNaN());
	}

	[Fact]
	public void GetHashCode_DifferentSegments_MayHaveDifferentHashCode()
	{
		var segment1 = new Segment2D(new XY(0, 0), new XY(1, 1));
		var segment2 = new Segment2D(new XY(2, 1), new XY(2, 2));

		// Not guaranteed to be different, but highly likely
		Assert.NotEqual(segment1.GetHashCode(), segment2.GetHashCode());
	}

	[Fact]
	public void GetHashCode_EqualSegments_HaveSameHashCode()
	{
		var segment1 = new Segment2D(new XY(0, 0), new XY(1, 1));
		var segment2 = new Segment2D(new XY(0, 0), new XY(1, 1));

		Assert.Equal(segment1.GetHashCode(), segment2.GetHashCode());
	}

	[Fact]
	public void ObjectEquals_WithDifferentType_ReturnsFalse()
	{
		var segment = new Segment2D(new XY(0, 0), new XY(1, 1));
		object other = "not a segment";

		Assert.False(segment.Equals(other));
	}

	[Fact]
	public void ObjectEquals_WithNull_ReturnsFalse()
	{
		var segment = new Segment2D(new XY(0, 0), new XY(1, 1));

		Assert.False(segment.Equals(null));
	}

	[Fact]
	public void ObjectEquals_WithSameSegment_ReturnsTrue()
	{
		var segment1 = new Segment2D(new XY(0, 0), new XY(1, 1));
		object segment2 = new Segment2D(new XY(0, 0), new XY(1, 1));

		Assert.True(segment1.Equals(segment2));
	}

	[Fact]
	public void Origin_CanBeModified()
	{
		var segment = new Segment2D(new XY(0, 0), new XY(1, 1));
		var newOrigin = new XY(2, 2);

		segment.Origin = newOrigin;

		Assert.Equal(newOrigin, segment.Origin);
	}
}