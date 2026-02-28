using System;

namespace CSUtilities.Converters;

#if PUBLIC
public
#else
internal
#endif
class DefaultEndianConverter : IEndianConverter
{
	public byte[] GetBytes(char value) => BitConverter.GetBytes(value);
	public byte[] GetBytes(short value) => BitConverter.GetBytes(value);
	public byte[] GetBytes(ushort value) => BitConverter.GetBytes(value);
	public byte[] GetBytes(int value) => BitConverter.GetBytes(value);
	public byte[] GetBytes(uint value) => BitConverter.GetBytes(value);
	public byte[] GetBytes(long value) => BitConverter.GetBytes(value);
	public byte[] GetBytes(ulong value) => BitConverter.GetBytes(value);
	public byte[] GetBytes(double value) => BitConverter.GetBytes(value);
	public byte[] GetBytes(float value) => BitConverter.GetBytes(value);

	public byte[] GetBytes<T>(T value)
		where T : struct
	{
		switch (value)
		{
			case byte b:
				return new byte[] { b };
			case char c:
				return this.GetBytes(c);
			case short s:
				return this.GetBytes(s);
			case ushort us:
				return this.GetBytes(us);
			case int i:
				return this.GetBytes(i);
			case uint ui:
				return this.GetBytes(ui);
			case long l:
				return this.GetBytes(l);
			case ulong ul:
				return this.GetBytes(ul);
			case double d:
				return this.GetBytes(d);
			case float f:
				return this.GetBytes(f);
			default:
				throw new NotSupportedException($"type {typeof(T).FullName} not supported");
		}
	}

	public char ToChar(byte[] arr) => BitConverter.ToChar(arr, 0);
	public short ToInt16(byte[] arr) => BitConverter.ToInt16(arr, 0);
	public ushort ToUInt16(byte[] arr) => BitConverter.ToUInt16(arr, 0);
	public int ToInt32(byte[] arr) => BitConverter.ToInt32(arr, 0);
	public uint ToUInt32(byte[] arr) => BitConverter.ToUInt32(arr, 0);
	public long ToInt64(byte[] arr) => BitConverter.ToInt64(arr, 0);
	public ulong ToUInt64(byte[] arr) => BitConverter.ToUInt64(arr, 0);
	public double ToDouble(byte[] arr) => BitConverter.ToDouble(arr, 0);
	public float ToSingle(byte[] arr) => BitConverter.ToSingle(arr, 0);
	public char ToChar(byte[] arr, int offset) => BitConverter.ToChar(arr, offset);
	public short ToInt16(byte[] arr, int offset) => BitConverter.ToInt16(arr, offset);
	public ushort ToUInt16(byte[] arr, int offset) => BitConverter.ToUInt16(arr, offset);
	public int ToInt32(byte[] arr, int offset) => BitConverter.ToInt32(arr, offset);
	public uint ToUInt32(byte[] arr, int offset) => BitConverter.ToUInt32(arr, offset);
	public long ToInt64(byte[] arr, int offset) => BitConverter.ToInt64(arr, offset);
	public ulong ToUInt64(byte[] arr, int offset) => BitConverter.ToUInt64(arr, offset);
	public double ToDouble(byte[] arr, int offset) => BitConverter.ToDouble(arr, offset);
	public float ToSingle(byte[] arr, int offset) => BitConverter.ToSingle(arr, offset);
}