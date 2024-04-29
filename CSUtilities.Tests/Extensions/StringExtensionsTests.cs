using CSUtilities.Extensions;
using System;
using Xunit;

namespace CSUtilities.Tests.Extensions
{
	public class StringExtensionsTests
	{
		[Fact]
		public void IsNullOrEmptyTest()
		{
			string n = null;
			Assert.True(n.IsNullOrEmpty());
		}

		[Fact]
		public void TrowIfNullOrEmptyTest()
		{
			string n = null;

			Assert.Throws<ArgumentException>(n.TrowIfNullOrEmpty);
			Assert.Throws<ArgumentException>(() => n.TrowIfNullOrEmpty("Message in case of null or empty"));

			string empty = string.Empty;

			Assert.Throws<ArgumentException>(empty.TrowIfNullOrEmpty);
			Assert.Throws<ArgumentException>(() => empty.TrowIfNullOrEmpty("Message in case of null or empty"));
		}
	}
}
