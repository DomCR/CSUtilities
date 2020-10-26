using System;

namespace CSUtilities.Converters
{
	/// <summary>Represents a little endian bit converter.</summary>
	[Obsolete("Use LittleEndianConverter instead")]
	internal static class LittleEndianBitConverter
	{
		private static readonly IEndianConverter m_converter;
		static LittleEndianBitConverter()
		{
			if (BitConverter.IsLittleEndian)
				LittleEndianBitConverter.m_converter = (IEndianConverter)new DefaultEndianConverter();
			else
				LittleEndianBitConverter.m_converter = (IEndianConverter)new InverseConverter();
		}
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(char value) => LittleEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(short value) => LittleEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(ushort value) => LittleEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(int value) => LittleEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(uint value) => LittleEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(long value) => LittleEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(ulong value) => LittleEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(double value) => LittleEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>Returns the specified value as an array of bytes.</summary>
		public static byte[] GetBytes(float value) => LittleEndianBitConverter.m_converter.GetBytes(value);
		/// <summary>
		/// Converts the specified bytes to a <see cref="System.Char" />.
		/// </summary>
		public static char ToChar(byte[] bytes) => LittleEndianBitConverter.m_converter.ToChar(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int16" />.
		/// </summary>
		public static short ToInt16(byte[] bytes) => LittleEndianBitConverter.m_converter.ToInt16(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt16" />.
		/// </summary>
		public static ushort ToUInt16(byte[] bytes) => LittleEndianBitConverter.m_converter.ToUInt16(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int32" />.
		/// </summary>
		public static int ToInt32(byte[] bytes) => LittleEndianBitConverter.m_converter.ToInt32(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt32" />.
		/// </summary>
		public static uint ToUInt32(byte[] bytes) => LittleEndianBitConverter.m_converter.ToUInt32(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int64" />.
		/// </summary>
		public static long ToInt64(byte[] bytes) => LittleEndianBitConverter.m_converter.ToInt64(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt64" />.
		/// </summary>
		public static ulong ToUInt64(byte[] bytes) => LittleEndianBitConverter.m_converter.ToUInt64(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Double" />.
		/// </summary>
		public static double ToDouble(byte[] bytes) => LittleEndianBitConverter.m_converter.ToDouble(bytes);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Single" />.
		/// </summary>
		public static float ToSingle(byte[] bytes) => LittleEndianBitConverter.m_converter.ToSingle(bytes);
		/// <summary>
		/// Converts the specified bytes to a <see cref="System.Char" />.
		/// </summary>
		public static char ToChar(byte[] bytes, int index) => LittleEndianBitConverter.m_converter.ToChar(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int16" />.
		/// </summary>
		public static short ToInt16(byte[] bytes, int index) => LittleEndianBitConverter.m_converter.ToInt16(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt16" />.
		/// </summary>
		public static ushort ToUInt16(byte[] bytes, int index) => LittleEndianBitConverter.m_converter.ToUInt16(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int32" />.
		/// </summary>
		public static int ToInt32(byte[] bytes, int index) => LittleEndianBitConverter.m_converter.ToInt32(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt32" />.
		/// </summary>
		public static uint ToUInt32(byte[] bytes, int index) => LittleEndianBitConverter.m_converter.ToUInt32(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Int64" />.
		/// </summary>
		public static long ToInt64(byte[] bytes, int index) => LittleEndianBitConverter.m_converter.ToInt64(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.UInt64" />.
		/// </summary>
		public static ulong ToUInt64(byte[] bytes, int index) => LittleEndianBitConverter.m_converter.ToUInt64(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Double" />.
		/// </summary>
		public static double ToDouble(byte[] bytes, int index) => LittleEndianBitConverter.m_converter.ToDouble(bytes, index);
		/// <summary>
		/// Converts the specified bytes to an <see cref="System.Single" />.
		/// </summary>
		public static float ToSingle(byte[] bytes, int index) => LittleEndianBitConverter.m_converter.ToSingle(bytes, index);
	}
}
