using System;
using Xunit;
using Xunit.Abstractions;

namespace CSMath.Tests
{
	public abstract class VectorTests<T>
		where T : IVector<T>, new()
	{
		public VectorTestCaseFactory Factory { get; set; }

		protected readonly ITestOutputHelper output;

		public VectorTests(ITestOutputHelper output)
		{
			Random random = new Random();
			Factory = new VectorTestCaseFactory(random.Next());

			this.output = output;
		}

		[Fact]
		public void AdditionTest()
		{
			var test = Factory.CreateOperationCase<T>((o, x) => o + x);
			writeTest(test);

			Assert.Equal(test.Item3, test.Item1.Add(test.Item2));
		}

		[Fact]
		public void DistanceTest()
		{
			var pt1 = Factory.CreatePoint<T>(0);
			var pt2 = Factory.CreatePoint<T>(1);

			double dist = pt1.DistanceFrom(pt2);

			Assert.Equal(Math.Sqrt(pt1.GetComponents().Length), dist);
		}

		[Fact]
		public void SubsctractTest()
		{
			var test = Factory.CreateOperationCase<T>((o, x) => o - x);
			writeTest(test);

			Assert.Equal(test.Item3, test.Item1.Substract(test.Item2));
		}

		[Fact]
		public void MultiplyTest()
		{
			var test = Factory.CreateOperationCase<T>((o, x) => o * x);
			writeTest(test);

			Assert.Equal(test.Item3, test.Item1.Multiply(test.Item2));
		}

		[Fact]
		public void DivideTest()
		{
			(T, T, T) test = Factory.CreateOperationCase<T>((o, x) => o / x);
			writeTest(test);

			Assert.Equal(test.Item3, test.Item1.Divide(test.Item2));
		}

		[Fact]
		public void IsEqualTest()
		{
			Random random = new Random();
			double def = random.NextDouble();

			var pt1 = Factory.CreatePoint<T>(def);
			var pt2 = Factory.CreatePoint<T>(def);

			Assert.True(pt1.IsEqual(pt2));
		}

		[Fact]
		public void IsEqualDigitsTest()
		{
			Random random = new Random();
			double def = random.NextDouble();

			var pt1 = Factory.CreatePoint<T>(def);
			var pt2 = Factory.CreatePoint<T>(def);

			Assert.True(pt1.IsEqual(pt2, 2));
		}

		protected void writeTest((T, T, T) test)
		{
			output.WriteLine($"Item 1 : {test.Item1}");
			output.WriteLine($"Item 2 : {test.Item2}");
			output.WriteLine($"Result : {test.Item3}");
		}
	}
}
