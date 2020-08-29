using System;

namespace CSUtilities.Converters
{
	/// <summary>Represents a little endian bit converter.</summary>
	public static class LittleEndianBitConverter
	{
		private static readonly IEndianConverter Converter;

		static LittleEndianBitConverter()
		{
			if (BitConverter.IsLittleEndian)
				LittleEndianBitConverter.Converter = (IEndianConverter)new DefaultConverter();
			else
				LittleEndianBitConverter.Converter = (IEndianConverter)new InverseConverter();
		}

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(char value) => LittleEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(short value) => LittleEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(ushort value) => LittleEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(int value) => LittleEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(uint value) => LittleEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(long value) => LittleEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(ulong value) => LittleEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(double value) => LittleEndianBitConverter.Converter.GetBytes(value);

		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(float value) => LittleEndianBitConverter.Converter.GetBytes(value);

		/// <summary>
		/// Converts the specified bytes to a <see cref="System.Char" />.
		/// </summary>
		public static char ToChar(byte[] bytes) => LittleEndianBitConverter.Converter.ToChar(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int16" />.
		/// </summary>
		public static short ToInt16(byte[] bytes) => LittleEndianBitConverter.Converter.ToInt16(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt16" />.
		/// </summary>
		public static ushort ToUInt16(byte[] bytes) => LittleEndianBitConverter.Converter.ToUInt16(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int32" />.
		/// </summary>
		public static int ToInt32(byte[] bytes) => LittleEndianBitConverter.Converter.ToInt32(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt32" />.
		/// </summary>
		public static uint ToUInt32(byte[] bytes) => LittleEndianBitConverter.Converter.ToUInt32(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int64" />.
		/// </summary>
		public static long ToInt64(byte[] bytes) => LittleEndianBitConverter.Converter.ToInt64(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt64" />.
		/// </summary>
		public static ulong ToUInt64(byte[] bytes) => LittleEndianBitConverter.Converter.ToUInt64(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Double" />.
		/// </summary>
		public static double ToDouble(byte[] bytes) => LittleEndianBitConverter.Converter.ToDouble(bytes);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Single" />.
		/// </summary>
		public static float ToSingle(byte[] bytes) => LittleEndianBitConverter.Converter.ToSingle(bytes);

		/// <summary>
		/// Converts the specified bytes to a <see cref="System.Char" />.
		/// </summary>
		public static char ToChar(byte[] bytes, int index) => LittleEndianBitConverter.Converter.ToChar(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int16" />.
		/// </summary>
		public static short ToInt16(byte[] bytes, int index) => LittleEndianBitConverter.Converter.ToInt16(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt16" />.
		/// </summary>
		public static ushort ToUInt16(byte[] bytes, int index) => LittleEndianBitConverter.Converter.ToUInt16(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int32" />.
		/// </summary>
		public static int ToInt32(byte[] bytes, int index) => LittleEndianBitConverter.Converter.ToInt32(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt32" />.
		/// </summary>
		public static uint ToUInt32(byte[] bytes, int index) => LittleEndianBitConverter.Converter.ToUInt32(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int64" />.
		/// </summary>
		public static long ToInt64(byte[] bytes, int index) => LittleEndianBitConverter.Converter.ToInt64(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt64" />.
		/// </summary>
		public static ulong ToUInt64(byte[] bytes, int index) => LittleEndianBitConverter.Converter.ToUInt64(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Double" />.
		/// </summary>
		public static double ToDouble(byte[] bytes, int index) => LittleEndianBitConverter.Converter.ToDouble(bytes, index);

		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Single" />.
		/// </summary>
		public static float ToSingle(byte[] bytes, int index) => LittleEndianBitConverter.Converter.ToSingle(bytes, index);
	}
}
