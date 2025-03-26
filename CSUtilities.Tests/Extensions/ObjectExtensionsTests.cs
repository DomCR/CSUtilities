using CSUtilities.Extensions;
using System;
using Xunit;

namespace CSUtilities.Tests.Extensions
{
	public class ObjectExtensionsTests
	{
		[Fact]
		public void ThrowIfTest()
		{
			int zero = 0;

			Assert.Throws<ArgumentOutOfRangeException>(() => zero.ThrowIf<int, ArgumentOutOfRangeException>((value) =>
				{
					return value == 0;
				}));
		}
	}
}
