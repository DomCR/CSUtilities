using System;
using CSUtilities;
using Xunit;

namespace CSUtilities.Tests;

public class TryTests
{
	[Fact]
	public void Do_ActionSucceeds_ReturnsTrue()
	{
		// Arrange
		bool executed = false;
		Action action = () => executed = true;

		// Act
		bool result = Try.Do(action);

		// Assert
		Assert.True(result);
		Assert.True(executed);
	}

	[Fact]
	public void Do_ActionThrows_ReturnsFalse()
	{
		// Arrange
		Action action = () => throw new InvalidOperationException();

		// Act
		bool result = Try.Do(action);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Do_WithExceptionOut_ActionSucceeds_ReturnsTrueAndNullException()
	{
		// Arrange
		Action action = () => { };

		// Act
		bool result = Try.Do(action, out Exception ex);

		// Assert
		Assert.True(result);
		Assert.Null(ex);
	}

	[Fact]
	public void Do_WithExceptionOut_ActionThrows_ReturnsFalseAndException()
	{
		// Arrange
		var expected = new InvalidOperationException("fail");
		Action action = () => throw expected;

		// Act
		bool result = Try.Do(action, out Exception ex);

		// Assert
		Assert.False(result);
		Assert.Same(expected, ex);
	}
}