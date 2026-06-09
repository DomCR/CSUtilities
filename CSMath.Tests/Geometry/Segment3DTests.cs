using CSMath.Geometry;
using Xunit;

namespace CSMath.Tests.Geometry;

public class Segment3DTests
{
	[Fact]
	public void Constructor_WithValidPoints_CreatesSegment()
	{
		var origin = new XYZ(0, 0, 0);
		var end = new XYZ(1, 1, 1);

		var segment = new Segment3D(origin, end);

		Assert.Equal(origin, segment.Origin);
		Assert.Equal(end, segment.End);
	}

	[Fact]
	public void Constructor_WithZeroLengthSegment_CreatesSegment()
	{
		var point = new XYZ(5, 5, 5);

		var segment = new Segment3D(point, point);

		Assert.Equal(point, segment.Origin);
		Assert.Equal(point, segment.End);
	}

	[Fact]
	public void Direction_ForZeroLengthSegment_ReturnsZeroVector()
	{
		var point = new XYZ(5, 5, 5);
		var segment = new Segment3D(point, point);

		var direction = segment.Direction;

		Assert.Equal(XYZ.Zero, direction);
	}

	[Fact]
	public void Direction_ReturnsVectorFromOriginToEnd()
	{
		var origin = new XYZ(1, 2, 3);
		var end = new XYZ(4, 6, 9);
		var segment = new Segment3D(origin, end);

		var direction = segment.Direction;

		Assert.Equal(new XYZ(3, 4, 6), direction);
	}

	[Fact]
	public void Direction_UpdatesWhenOriginChanges()
	{
		var segment = new Segment3D(new XYZ(0, 0, 0), new XYZ(2, 2, 2));

		segment.Origin = new XYZ(1, 1, 1);

		Assert.Equal(new XYZ(1, 1, 1), segment.Direction);
	}

	[Fact]
	public void Direction_UpdatesWhenPropertiesChange()
	{
		var segment = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));

		segment.End = new XYZ(2, 2, 2);

		Assert.Equal(new XYZ(2, 2, 2), segment.Direction);
	}

	[Fact]
	public void Direction_WithMixedCoordinates_ReturnsCorrectVector()
	{
		var origin = new XYZ(-1, 2, -3);
		var end = new XYZ(4, -6, 9);
		var segment = new Segment3D(origin, end);

		var direction = segment.Direction;

		Assert.Equal(new XYZ(5, -8, 12), direction);
	}

	[Fact]
	public void Direction_WithNegativeCoordinates_ReturnsCorrectVector()
	{
		var origin = new XYZ(-1, -2, -3);
		var end = new XYZ(-4, -6, -9);
		var segment = new Segment3D(origin, end);

		var direction = segment.Direction;

		Assert.Equal(new XYZ(-3, -4, -6), direction);
	}

	[Fact]
	public void End_CanBeModified()
	{
		var segment = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));
		var newEnd = new XYZ(3, 3, 3);

		segment.End = newEnd;

		Assert.Equal(newEnd, segment.End);
	}

	[Fact]
	public void Equals_WithDifferentEnd_ReturnsFalse()
	{
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));
		var segment2 = new Segment3D(new XYZ(0, 0, 0), new XYZ(2, 2, 2));

		Assert.False(segment1.Equals(segment2));
	}

	[Fact]
	public void Equals_WithDifferentOrigin_ReturnsFalse()
	{
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));
		var segment2 = new Segment3D(new XYZ(1, 0, 0), new XYZ(1, 1, 1));

		Assert.False(segment1.Equals(segment2));
	}

	[Fact]
	public void Equals_WithDifferentZ_ReturnsFalse()
	{
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));
		var segment2 = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 2));

		Assert.False(segment1.Equals(segment2));
	}

	[Fact]
	public void Equals_WithSameOriginAndEnd_ReturnsTrue()
	{
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));
		var segment2 = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));

		Assert.True(segment1.Equals(segment2));
	}

	[Fact]
	public void FindIntersection_WithCollinearSegments_ReturnsNaN()
	{
		// Two collinear segments on the same line
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(2, 0, 0));
		var segment2 = new Segment3D(new XYZ(1, 0, 0), new XYZ(3, 0, 0));

		var intersection = segment1.FindIntersection(segment2);

		Assert.True(intersection.IsNaN());
	}

	[Fact]
	public void FindIntersection_WithDiagonalSegments_ReturnsIntersectionPoint()
	{
		// Segment from (0, 0, 0) to (2, 2, 0)
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(2, 2, 0));
		// Segment from (0, 2, 0) to (2, 0, 0)
		var segment2 = new Segment3D(new XYZ(0, 2, 0), new XYZ(2, 0, 0));

		var intersection = segment1.FindIntersection(segment2);

		Assert.Equal(new XYZ(1, 1, 0), intersection);
	}

	[Fact]
	public void FindIntersection_WithIntersectingSegmentsIn3D_ReturnsIntersectionPoint()
	{
		// Segment from (0, 0, 0) to (2, 2, 2)
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(2, 2, 2));
		// Segment from (0, 2, 0) to (2, 0, 2)
		var segment2 = new Segment3D(new XYZ(0, 2, 0), new XYZ(2, 0, 2));

		var intersection = segment1.FindIntersection(segment2);

		Assert.Equal(new XYZ(1, 1, 1), intersection);
	}

	[Fact]
	public void FindIntersection_WithIntersectingSegmentsInXYPlane_ReturnsIntersectionPoint()
	{
		// Horizontal segment from (0, 1, 0) to (2, 1, 0)
		var segment1 = new Segment3D(new XYZ(0, 1, 0), new XYZ(2, 1, 0));
		// Vertical segment from (1, 0, 0) to (1, 2, 0)
		var segment2 = new Segment3D(new XYZ(1, 0, 0), new XYZ(1, 2, 0));

		var intersection = segment1.FindIntersection(segment2);

		Assert.Equal(new XYZ(1, 1, 0), intersection);
	}

	[Fact]
	public void FindIntersection_WithIntersectionAtEndpoint_ReturnsIntersectionPoint()
	{
		// Segment from (0, 0, 0) to (2, 0, 0)
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(2, 0, 0));
		// Segment from (2, -1, 0) to (2, 1, 0)
		var segment2 = new Segment3D(new XYZ(2, -1, 0), new XYZ(2, 1, 0));

		var intersection = segment1.FindIntersection(segment2);

		Assert.Equal(new XYZ(2, 0, 0), intersection);
	}

	[Fact]
	public void FindIntersection_WithIntersectionJustOutsideBounds_ReturnsNaN()
	{
		// Segment from (0, 0, 0) to (1, 0, 0)
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 0, 0));
		// Segment from (2, -1, 0) to (2, 1, 0)
		var segment2 = new Segment3D(new XYZ(2, -1, 0), new XYZ(2, 1, 0));

		var intersection = segment1.FindIntersection(segment2);

		Assert.True(intersection.IsNaN());
	}

	[Fact]
	public void FindIntersection_WithIntersectionOutsideSegmentBounds_ReturnsNaN()
	{
		// Segment from (0, 1, 0) to (1, 1, 0)
		var segment1 = new Segment3D(new XYZ(0, 1, 0), new XYZ(1, 1, 0));
		// Vertical segment from (2, 0, 0) to (2, 2, 0)
		var segment2 = new Segment3D(new XYZ(2, 0, 0), new XYZ(2, 2, 0));

		var intersection = segment1.FindIntersection(segment2);

		Assert.True(intersection.IsNaN());
	}

	[Fact]
	public void FindIntersection_WithParallelSegments_ReturnsNaN()
	{
		// Two parallel horizontal segments
		var segment1 = new Segment3D(new XYZ(0, 1, 0), new XYZ(2, 1, 0));
		var segment2 = new Segment3D(new XYZ(0, 2, 0), new XYZ(2, 2, 0));

		var intersection = segment1.FindIntersection(segment2);

		Assert.True(intersection.IsNaN());
	}

	[Fact]
	public void FindIntersection_WithSegmentsInDifferentPlanes_MayReturnNaN()
	{
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(2, 2, 0));
		var segment2 = new Segment3D(new XYZ(3, 3, 3), new XYZ(1, 1, 3));

		var intersection = segment1.FindIntersection(segment2);

		Assert.True(intersection.IsNaN());
	}

	[Fact]
	public void GetHashCode_DifferentSegments_MayHaveDifferentHashCode()
	{
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));
		var segment2 = new Segment3D(new XYZ(1, 1, 1), new XYZ(2, 2, 2));

		// Not guaranteed to be different, but highly likely
		Assert.NotEqual(segment1.GetHashCode(), segment2.GetHashCode());
	}

	[Fact]
	public void GetHashCode_EqualSegments_HaveSameHashCode()
	{
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));
		var segment2 = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));

		Assert.Equal(segment1.GetHashCode(), segment2.GetHashCode());
	}

	[Fact]
	public void ObjectEquals_WithDifferentType_ReturnsFalse()
	{
		var segment = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));
		object other = "not a segment";

		Assert.False(segment.Equals(other));
	}

	[Fact]
	public void ObjectEquals_WithNull_ReturnsFalse()
	{
		var segment = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));

		Assert.False(segment.Equals(null));
	}

	[Fact]
	public void ObjectEquals_WithSameSegment_ReturnsTrue()
	{
		var segment1 = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));
		object segment2 = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));

		Assert.True(segment1.Equals(segment2));
	}

	[Fact]
	public void Origin_CanBeModified()
	{
		var segment = new Segment3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1));
		var newOrigin = new XYZ(2, 2, 2);

		segment.Origin = newOrigin;

		Assert.Equal(newOrigin, segment.Origin);
	}
}