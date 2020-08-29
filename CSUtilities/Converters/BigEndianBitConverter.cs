using System;

namespace CSUtilities.Converters
{
	/// <summary>Represents a big endian bit converter.</summary>
	public static class BigEndianBitConverter
	{
		internal static readonly IEndianConverter Converter;

		static BigEndianBitConverter()
		{
			if (BitConverter.IsLittleEndian)
				BigEndianBitConverter.Converter = (IEndianConverter)new InverseConverter();
			else
				BigEndianBitConverter.Converter = (IEndianConverter)new DefaultConverter();
		}
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(char value) => BigEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(short value) => BigEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(ushort value) => BigEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(int value) => BigEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(uint value) => BigEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(long value) => BigEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(ulong value) => BigEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(double value) => BigEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(float value) => BigEndianBitConverter.Converter.GetBytes(value);

		/// <summary>
		/// Converts the specified bytes to a <see cref="T:System.Char" />.
		/// </summary>
		public static char ToChar(byte[] bytes) => BigEndianBitConverter.Converter.ToChar(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.Int16" />.
		/// </summary>
		public static short ToInt16(byte[] bytes) => BigEndianBitConverter.Converter.ToInt16(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.UInt16" />.
		/// </summary>
		public static ushort ToUInt16(byte[] bytes) => BigEndianBitConverter.Converter.ToUInt16(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.Int32" />.
		/// </summary>
		public static int ToInt32(byte[] bytes) => BigEndianBitConverter.Converter.ToInt32(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.UInt32" />.
		/// </summary>
		public static uint ToUInt32(byte[] bytes) => BigEndianBitConverter.Converter.ToUInt32(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.Int64" />.
		/// </summary>
		public static long ToInt64(byte[] bytes) => BigEndianBitConverter.Converter.ToInt64(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.UInt64" />.
		/// </summary>
		public static ulong ToUInt64(byte[] bytes) => BigEndianBitConverter.Converter.ToUInt64(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.Double" />.
		/// </summary>
		public static double ToDouble(byte[] bytes) => BigEndianBitConverter.Converter.ToDouble(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.Single" />.
		/// </summary>
		public static float ToSingle(byte[] bytes) => BigEndianBitConverter.Converter.ToSingle(bytes);

		/// <summary>
		/// Converts the specified bytes to a <see cref="T:System.Char" />.
		/// </summary>
		public static char ToChar(byte[] bytes, int index) => BigEndianBitConverter.Converter.ToChar(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.Int16" />.
		/// </summary>
		public static short ToInt16(byte[] bytes, int index) => BigEndianBitConverter.Converter.ToInt16(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.UInt16" />.
		/// </summary>
		public static ushort ToUInt16(byte[] bytes, int index) => BigEndianBitConverter.Converter.ToUInt16(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.Int32" />.
		/// </summary>
		public static int ToInt32(byte[] bytes, int index) => BigEndianBitConverter.Converter.ToInt32(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.UInt32" />.
		/// </summary>
		public static uint ToUInt32(byte[] bytes, int index) => BigEndianBitConverter.Converter.ToUInt32(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.Int64" />.
		/// </summary>
		public static long ToInt64(byte[] bytes, int index) => BigEndianBitConverter.Converter.ToInt64(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.UInt64" />.
		/// </summary>
		public static ulong ToUInt64(byte[] bytes, int index) => BigEndianBitConverter.Converter.ToUInt64(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.Double" />.
		/// </summary>
		public static double ToDouble(byte[] bytes, int index) => BigEndianBitConverter.Converter.ToDouble(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="T:System.Single" />.
		/// </summary>
		public static float ToSingle(byte[] bytes, int index) => BigEndianBitConverter.Converter.ToSingle(bytes, index);
	}
}
