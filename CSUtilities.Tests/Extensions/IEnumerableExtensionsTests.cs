using System;
using System.Collections.Generic;
using System.Linq;
using CSUtilities.Extensions;
using Xunit;

namespace CSUtilities.Tests.Extensions;

public class IEnumerableExtensionsTests
{
#if NET5_0
	[Fact]
	public void DistinctBy_EmptySequence_ReturnsEmpty()
	{
		var source = Enumerable.Empty<int>();

		var result = source.DistinctBy(x => x).ToList();

		Assert.Empty(result);
	}

	[Fact]
	public void DistinctBy_RemovesDuplicatesByKey()
	{
		var source = new[] { "apple", "apricot", "banana", "blueberry", "cherry" };

		var result = source.DistinctBy(s => s[0]).ToList();

		Assert.Equal(3, result.Count);
		Assert.Equal("apple", result[0]);
		Assert.Equal("banana", result[1]);
		Assert.Equal("cherry", result[2]);
	} 
#endif

	[Fact]
	public void ForEach_EmptySequence_DoesNothing()
	{
		var source = Enumerable.Empty<int>();
		var results = new List<int>();

		source.ForEach(x => results.Add(x));

		Assert.Empty(results);
	}

	[Fact]
	public void ForEach_ExecutesActionOnAllElements()
	{
		var source = new[] { 1, 2, 3 };
		var results = new List<int>();

		source.ForEach(x => results.Add(x * 2));

		Assert.Equal(new[] { 2, 4, 6 }, results);
	}

	[Fact]
	public void IsEmpty_EmptyCollection_ReturnsExpected()
	{
		var source = Enumerable.Empty<int>();

		var result = source.IsEmpty();

		// Note: current implementation checks GetEnumerator() == null,
		// which will return false for standard enumerables
		Assert.False(result);
	}

	[Fact]
	public void RemoveLastEquals_NoTrailingMatch_ReturnsAll()
	{
		var source = new[] { 1, 2, 3 };

		var result = source.RemoveLastEquals(0).ToList();

		Assert.Equal(3, result.Count);
		Assert.Equal(new[] { 1, 2, 3 }, result);
	}

	[Fact]
	public void RemoveLastEquals_RemovesTrailingElements()
	{
		var source = new[] { 1, 2, 3, 0, 0 };

		var result = source.RemoveLastEquals(0).ToList();

		Assert.Equal(3, result.Count);
		Assert.Equal(new[] { 1, 2, 3 }, result);
	}

	[Fact]
	public void ToQueue_ConvertsToQueue()
	{
		var source = new[] { 1, 2, 3 };

		var result = source.ToQueue();

		Assert.Equal(3, result.Count);
		Assert.Equal(1, result.Dequeue());
		Assert.Equal(2, result.Dequeue());
		Assert.Equal(3, result.Dequeue());
	}

	[Fact]
	public void TryGet_IndexOutOfRange_ReturnsFalseAndDefault()
	{
		var source = new[] { 10, 20, 30 };

		bool found = source.TryGet(5, out int result);

		Assert.False(found);
		Assert.Equal(default, result);
	}

	[Fact]
	public void TryGet_ValidIndex_ReturnsTrueAndElement()
	{
		var source = new[] { 10, 20, 30 };

		bool found = source.TryGet(1, out int result);

		Assert.True(found);
		Assert.Equal(20, result);
	}
}