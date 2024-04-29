using CSUtilities.Extensions;
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
	}
}
