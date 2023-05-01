﻿using System;

namespace CSMath
{
	public static class VectorExtensions
	{
		/// <summary>
		/// Distance between two points
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="u"></param>
		/// <param name="v"></param>
		/// <returns></returns>
		public static double DistanceFrom<T>(this T u, T v)
			where T : IVector<T>
		{
			double value = 0;
			var c1 = u.GetComponents();
			var c2 = v.GetComponents();

			for (int i = 0; i < c1.Length; i++)
			{
				value += Math.Pow(c1[i] - c2[i], 2);
			}

			return Math.Sqrt(value);
		}

		/// <summary>
		/// Returns the length of the vector.
		/// </summary>
		/// <returns>The vector's length.</returns>
		public static double GetLength<T>(this T vector)
			where T : IVector<T>
		{
			double length = 0;

			foreach (var item in vector.GetComponents())
			{
				length += item * item;
			}

			return Math.Sqrt(length);
		}

		/// <summary>
		/// Returns a vector with the same direction as the given vector, but with a length of 1.
		/// </summary>
		/// <param name="vector">The vector to normalize.</param>
		/// <returns>The normalized vector.</returns>
		public static T Normalize<T>(this T vector)
			where T : IVector<T>, new()
		{
			double length = vector.GetLength();
			double[] components = vector.GetComponents();

			for (int i = 0; i < components.Length; i++)
			{
				components[i] /= length;
			}

			return new T().SetComponents(components);
		}

		/// <summary>
		/// Returns the dot product of two vectors.
		/// </summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The dot product.</returns>
		public static double Dot<T>(this T left, T right)
			where T : IVector<T>
		{
			var components1 = left.GetComponents();
			var components2 = right.GetComponents();
			double result = 0;

			for (int i = 0; i < components1.Length; i++)
			{
				result += components1[i] * components2[i];
			}

			return result;
		}

		/// <summary>
		/// Returns a boolean indicating whether the two given vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>True if the vectors are equal; False otherwise.</returns>
		public static bool IsEqual<T>(this T left, T right)
			where T : IVector<T>
		{
			var components1 = left.GetComponents();
			var components2 = right.GetComponents();

			for (int i = 0; i < components1.Length; i++)
			{
				if (components1[i] != components2[i])
					return false;
			}

			return true;
		}

		/// <summary>
		/// Returns a boolean indicating whether the two given vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <param name="ndecimals">Number of decimals digits to be set as precision.</param>
		/// <returns>True if the vectors are equal; False otherwise.</returns>
		public static bool IsEqual<T>(this T left, T right, int ndecimals)
			where T : IVector<T>
		{
			var components1 = left.GetComponents();
			var components2 = right.GetComponents();

			for (int i = 0; i < components1.Length; i++)
			{
				if (Math.Round(components1[i], ndecimals) != Math.Round(components2[i], ndecimals))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Adds two vectors together.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The summed vector.</returns>
		public static T Add<T>(this T left, T right)
			where T : IVector<T>, new()
		{
			return applyFunctionByComponentIndex(left, right, (o, x) => o + x);
		}

		/// <summary>
		/// Subtracts the second vector from the first.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The difference vector.</returns>
		public static T Substract<T>(this T left, T right)
			where T : IVector<T>, new()
		{
			return applyFunctionByComponentIndex(left, right, (o, x) => o - x);
		}

		/// <summary>
		/// Multiplies two vectors together.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The product vector.</returns>
		public static T Multiply<T>(this T left, T right)
			where T : IVector<T>, new()
		{
			return applyFunctionByComponentIndex(left, right, (o, x) => o * x);
		}

		/// <summary>
		/// Multiplies a vector with an scalar.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The product vector.</returns>
		public static T Multiply<T>(this T left, double scalar)
			where T : IVector<T>, new()
		{
			return applyFunctionByScalar(left, scalar, (o, x) => o * x);
		}

		/// <summary>
		/// Divides the first vector by the second.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The vector resulting from the division.</returns>
		public static T Divide<T>(this T left, T right)
			where T : IVector<T>, new()
		{
			return applyFunctionByComponentIndex(left, right, (o, x) => o / x);
		}

		/// <summary>
		/// Divides a vector with an scalar.
		/// </summary>
		public static T Divide<T>(this T left, double scalar)
			where T : IVector<T>, new()
		{
			return applyFunctionByScalar(left, scalar, (o, x) => o / x);
		}

		public static T Round<T>(this T vector)
			where T : IVector<T>, new()
		{
			double[] components1 = vector.GetComponents();

			for (int i = 0; i < components1.Length; i++)
			{
				components1[i] = Math.Round(components1[i]);
			}

			return new T().SetComponents(components1);
		}

		/// <summary>
		/// Applies a function in all the components of a vector by order
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="op"></param>
		/// <returns></returns>
		private static T applyFunctionByComponentIndex<T>(this T left, T right, Func<double, double, double> op)
			where T : IVector<T>, new()
		{
			double[] c1 = left.GetComponents();
			double[] c2 = right.GetComponents();
			double[] result = new double[c1.Length];

			for (int i = 0; i < c1.Length; i++)
			{
				result[i] = op(c1[i], c2[i]);
			}

			return new T().SetComponents(result);
		}

		private static T applyFunctionByScalar<T>(this T v, double scalar, Func<double, double, double> op)
			where T : IVector<T>, new()
		{
			double[] c1 = v.GetComponents();
			double[] result = new double[c1.Length];

			for (int i = 0; i < c1.Length; i++)
			{
				result[i] = op(c1[i], scalar);
			}

			return new T().SetComponents(result);
		}
	}
}
