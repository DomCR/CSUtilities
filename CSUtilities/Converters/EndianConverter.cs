using System;
using System.Collections.Generic;
using System.Text;

namespace CSUtilities.Converters
{
	internal abstract class EndianConverter : IEndianConverter
	{
		protected readonly IEndianConverter m_converter;
		public EndianConverter() { }
		protected EndianConverter(IEndianConverter converter)
		{
			m_converter = converter;
		}
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public byte[] GetBytes(char value) => m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public byte[] GetBytes(short value) => m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public byte[] GetBytes(ushort value) => m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public byte[] GetBytes(int value) => m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public byte[] GetBytes(uint value) => m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public byte[] GetBytes(long value) => m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public byte[] GetBytes(ulong value) => m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public byte[] GetBytes(double value) => m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public byte[] GetBytes(float value) => m_converter.GetBytes(value);
		/// <summary>
		/// Converts the specified bytes to a <see cref="System.Char" />.
		/// </summary>
		public char ToChar(byte[] bytes) => m_converter.ToChar(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int16" />.
		/// </summary>
		public short ToInt16(byte[] bytes) => m_converter.ToInt16(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt16" />.
		/// </summary>
		public ushort ToUInt16(byte[] bytes) => m_converter.ToUInt16(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int32" />.
		/// </summary>
		public int ToInt32(byte[] bytes) => m_converter.ToInt32(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt32" />.
		/// </summary>
		public uint ToUInt32(byte[] bytes) => m_converter.ToUInt32(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int64" />.
		/// </summary>
		public long ToInt64(byte[] bytes) => m_converter.ToInt64(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt64" />.
		/// </summary>
		public ulong ToUInt64(byte[] bytes) => m_converter.ToUInt64(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Double" />.
		/// </summary>
		public double ToDouble(byte[] bytes) => m_converter.ToDouble(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Single" />.
		/// </summary>
		public float ToSingle(byte[] bytes) => m_converter.ToSingle(bytes);
		/// <summary>
		/// Converts the specified bytes to a <see cref="System.Char" />.
		/// </summary>
		public char ToChar(byte[] bytes, int index) => m_converter.ToChar(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int16" />.
		/// </summary>
		public short ToInt16(byte[] bytes, int index) => m_converter.ToInt16(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt16" />.
		/// </summary>
		public ushort ToUInt16(byte[] bytes, int index) => m_converter.ToUInt16(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int32" />.
		/// </summary>
		public int ToInt32(byte[] bytes, int index) => m_converter.ToInt32(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt32" />.
		/// </summary>
		public uint ToUInt32(byte[] bytes, int index) => m_converter.ToUInt32(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int64" />.
		/// </summary>
		public long ToInt64(byte[] bytes, int index) => m_converter.ToInt64(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt64" />.
		/// </summary>
		public ulong ToUInt64(byte[] bytes, int index) => m_converter.ToUInt64(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Double" />.
		/// </summary>
		public double ToDouble(byte[] bytes, int index) => m_converter.ToDouble(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Single" />.
		/// </summary>
		public float ToSingle(byte[] bytes, int index) => m_converter.ToSingle(bytes, index);
	}
}
