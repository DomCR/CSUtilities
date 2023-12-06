using System;

namespace CSMath
{
	public interface IVector
	{
		/// <summary>
		/// Get the diferent components of a dimensional vector.
		/// </summary>
		/// <returns>Array with the vector components.</returns>
		double[] GetComponents();

		/// <summary>
		/// Get the dimension of the <see cref="IVector"/>.
		/// </summary>
		uint Dimension { get; }

		/// <summary>
		/// Value of the coordinate at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The value of the coordinate at the specified index.</returns>
		public double this[uint index] { get; set; }
	}

	[Obsolete("Use IVector instead")]
	public interface IVector<T> : IVector
	{
		/// <summary>
		/// Create a new instance of the same type with the given components.
		/// </summary>
		/// <param name="components">Components to create the new IVector</param>
		/// <returns>A new instance of a dimensional vector.</returns>
		[Obsolete("Deprecated, use double this[uint index] keys instead")]
		T SetComponents(double[] components);
	}
}
