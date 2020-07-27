using System;
using System.Collections.Generic;
using System.Text;

namespace CSUtilities
{
	/// <summary>
	/// Class to store a type value directly in it.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TypeValue<T> : IEquatable<T>
	{
		/// <summary>
		/// Value stored in the class.
		/// </summary>
		public T Value { get; set; }
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TypeValue()
		{
			Value = default;
		}
		/// <summary>
		/// Assigned value to the current class
		/// </summary>
		/// <param name="value"></param>
		public TypeValue(T value)
		{
			Value = value;
		}
		/// <summary>
		/// Allow the assignment of this element to it's value.
		/// </summary>
		/// <param name="obj"></param>
		public static implicit operator T(TypeValue<T> obj)
		{
			return obj.Value;
		}
		/// <summary>
		/// Create a type value class by assigning a value.
		/// </summary>
		/// <param name="value"></param>
		public static explicit operator TypeValue<T>(T value)
		{
			return new TypeValue<T>(value);
		}
		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return Value != null ? Value.GetHashCode() : base.GetHashCode();
		}
		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (GetType() != obj.GetType())
				return false;

			return (obj as TypeValue<T>).Value.Equals(Value);
		}
		/// <inheritdoc/>
		public bool Equals(T other)
		{
			return Equals(other);
		}
	}
}
