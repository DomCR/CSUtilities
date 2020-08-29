using System;

namespace CSUtilities.Converters
{
	internal class DefaultConverter : IEndianConverter
	{
		public byte[] GetBytes(char _param1) => BitConverter.GetBytes(_param1);

		public byte[] GetBytes(short _param1) => BitConverter.GetBytes(_param1);

		public byte[] GetBytes(ushort _param1) => BitConverter.GetBytes(_param1);

		public byte[] GetBytes(int _param1) => BitConverter.GetBytes(_param1);

		public byte[] GetBytes(uint _param1) => BitConverter.GetBytes(_param1);

		public byte[] GetBytes(long _param1) => BitConverter.GetBytes(_param1);

		public byte[] GetBytes(ulong _param1) => BitConverter.GetBytes(_param1);

		public byte[] GetBytes(double _param1) => BitConverter.GetBytes(_param1);

		public byte[] GetBytes(float _param1) => BitConverter.GetBytes(_param1);

		public char ToChar(byte[] _param1) => BitConverter.ToChar(_param1, 0);

		public short ToInt16(byte[] _param1) => BitConverter.ToInt16(_param1, 0);

		public ushort ToUInt16(byte[] _param1) => BitConverter.ToUInt16(_param1, 0);

		public int ToInt32(byte[] _param1) => BitConverter.ToInt32(_param1, 0);

		public uint ToUInt32(byte[] _param1) => BitConverter.ToUInt32(_param1, 0);

		public long ToInt64(byte[] _param1) => BitConverter.ToInt64(_param1, 0);

		public ulong ToUInt64(byte[] _param1) => BitConverter.ToUInt64(_param1, 0);

		public double ToDouble(byte[] _param1) => BitConverter.ToDouble(_param1, 0);

		public float ToSingle(byte[] _param1) => BitConverter.ToSingle(_param1, 0);

		public char ToChar(byte[] _param1, int _param2) => BitConverter.ToChar(_param1, _param2);

		public short ToInt16(byte[] _param1, int _param2) => BitConverter.ToInt16(_param1, _param2);

		public ushort ToUInt16(byte[] _param1, int _param2) => BitConverter.ToUInt16(_param1, _param2);

		public int ToInt32(byte[] _param1, int _param2) => BitConverter.ToInt32(_param1, _param2);

		public uint ToUInt32(byte[] _param1, int _param2) => BitConverter.ToUInt32(_param1, _param2);

		public long ToInt64(byte[] _param1, int _param2) => BitConverter.ToInt64(_param1, _param2);

		public ulong ToUInt64(byte[] _param1, int _param2) => BitConverter.ToUInt64(_param1, _param2);

		public double ToDouble(byte[] _param1, int _param2) => BitConverter.ToDouble(_param1, _param2);

		public float ToSingle(byte[] _param1, int _param2) => BitConverter.ToSingle(_param1, _param2);
	} 
}
