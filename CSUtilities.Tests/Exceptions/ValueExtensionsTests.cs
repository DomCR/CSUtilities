using CSUtilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CSUtilities.Tests.Exceptions
{
	public class ValueExtensionsTests
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

		[Theory]
		[InlineData(-10, 0, 10, true)]
		[InlineData(5, 0, 10, false)]
		[InlineData(20, 0, 10, true)]
		[InlineData(0, 0, 10, false)]
		[InlineData(10, 0, 10, false)]
		public void InRangeTest(double value, double min, double max, bool shouldThrow)
		{
			Action action = () =>
			{
				value.InRange(min, max);
			};

			if (shouldThrow)
			{
				Assert.Throws<ArgumentOutOfRangeException>(action);
			}
			else
			{
				action();
			}
		}
	}
}
