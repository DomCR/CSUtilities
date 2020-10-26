using System;

namespace CSUtilities.Converters
{
	/// <summary>Represents a big endian bit converter.</summary>
	[Obsolete("Use BigEndianConverter instead")]
	internal static class BigEndianBitConverter
	{
		internal static readonly IEndianConverter m_converter;
		static BigEndianBitConverter()
		{
			if (BitConverter.IsLittleEndian)
				BigEndianBitConverter.m_converter = (IEndianConverter)new InverseConverter();
			else
				BigEndianBitConverter.m_converter = (IEndianConverter)new DefaultEndianConverter();
		}
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(char value) => BigEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(short value) => BigEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(ushort value) => BigEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(int value) => BigEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(uint value) => BigEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(long value) => BigEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(ulong value) => BigEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(double value) => BigEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(float value) => BigEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>
		/// Converts the specified bytes to a <see cref="System.Char" />.
		/// </summary>
		public static char ToChar(byte[] bytes) => BigEndianBitConverter.m_converter.ToChar(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int16" />.
		/// </summary>
		public static short ToInt16(byte[] bytes) => BigEndianBitConverter.m_converter.ToInt16(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt16" />.
		/// </summary>
		public static ushort ToUInt16(byte[] bytes) => BigEndianBitConverter.m_converter.ToUInt16(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int32" />.
		/// </summary>
		public static int ToInt32(byte[] bytes) => BigEndianBitConverter.m_converter.ToInt32(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt32" />.
		/// </summary>
		public static uint ToUInt32(byte[] bytes) => BigEndianBitConverter.m_converter.ToUInt32(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int64" />.
		/// </summary>
		public static long ToInt64(byte[] bytes) => BigEndianBitConverter.m_converter.ToInt64(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt64" />.
		/// </summary>
		public static ulong ToUInt64(byte[] bytes) => BigEndianBitConverter.m_converter.ToUInt64(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Double" />.
		/// </summary>
		public static double ToDouble(byte[] bytes) => BigEndianBitConverter.m_converter.ToDouble(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Single" />.
		/// </summary>
		public static float ToSingle(byte[] bytes) => BigEndianBitConverter.m_converter.ToSingle(bytes);
		/// <summary>
		/// Converts the specified bytes to a <see cref="System.Char" />.
		/// </summary>
		public static char ToChar(byte[] bytes, int index) => BigEndianBitConverter.m_converter.ToChar(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int16" />.
		/// </summary>
		public static short ToInt16(byte[] bytes, int index) => BigEndianBitConverter.m_converter.ToInt16(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt16" />.
		/// </summary>
		public static ushort ToUInt16(byte[] bytes, int index) => BigEndianBitConverter.m_converter.ToUInt16(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int32" />.
		/// </summary>
		public static int ToInt32(byte[] bytes, int index) => BigEndianBitConverter.m_converter.ToInt32(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt32" />.
		/// </summary>
		public static uint ToUInt32(byte[] bytes, int index) => BigEndianBitConverter.m_converter.ToUInt32(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int64" />.
		/// </summary>
		public static long ToInt64(byte[] bytes, int index) => BigEndianBitConverter.m_converter.ToInt64(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt64" />.
		/// </summary>
		public static ulong ToUInt64(byte[] bytes, int index) => BigEndianBitConverter.m_converter.ToUInt64(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Double" />.
		/// </summary>
		public static double ToDouble(byte[] bytes, int index) => BigEndianBitConverter.m_converter.ToDouble(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Single" />.
		/// </summary>
		public static float ToSingle(byte[] bytes, int index) => BigEndianBitConverter.m_converter.ToSingle(bytes, index);
	}
}
