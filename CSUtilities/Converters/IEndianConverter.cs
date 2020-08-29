namespace CSUtilities.Converters
{
	internal interface IEndianConverter
	{
		byte[] GetBytes(char _param1);

		byte[] GetBytes(short _param1);

		byte[] GetBytes(ushort _param1);

		byte[] GetBytes(int _param1);

		byte[] GetBytes(uint _param1);

		byte[] GetBytes(long _param1);

		byte[] GetBytes(ulong _param1);

		byte[] GetBytes(double _param1);

		byte[] GetBytes(float _param1);

		char ToChar(byte[] _param1);

		short ToInt16(byte[] _param1);

		ushort ToUInt16(byte[] _param1);

		int ToInt32(byte[] _param1);

		uint ToUInt32(byte[] _param1);

		long ToInt64(byte[] _param1);

		ulong ToUInt64(byte[] _param1);

		double ToDouble(byte[] _param1);

		float ToSingle(byte[] _param1);

		char ToChar(byte[] _param1, int _param2);

		short ToInt16(byte[] _param1, int _param2);

		ushort ToUInt16(byte[] _param1, int _param2);

		int ToInt32(byte[] _param1, int _param2);

		uint ToUInt32(byte[] _param1, int _param2);

		long ToInt64(byte[] _param1, int _param2);

		ulong ToUInt64(byte[] _param1, int _param2);

		double ToDouble(byte[] _param1, int _param2);

		float ToSingle(byte[] _param1, int _param2);
	}
}