using System;

namespace CSUtilities.Converters
{
	internal class InverseConverter : IEndianConverter
	{
		public byte[] GetBytes(char _param1)
		{
			byte[] bytes = BitConverter.GetBytes(_param1);
			InverseConverter.luWWE000(bytes);
			return bytes;
		}

		public byte[] GetBytes(short _param1)
		{
			byte[] bytes = BitConverter.GetBytes(_param1);
			InverseConverter.luWWE000(bytes);
			return bytes;
		}

		public byte[] GetBytes(ushort _param1)
		{
			byte[] bytes = BitConverter.GetBytes(_param1);
			InverseConverter.luWWE000(bytes);
			return bytes;
		}

		public byte[] GetBytes(int _param1)
		{
			byte[] bytes = BitConverter.GetBytes(_param1);
			InverseConverter.luWWE001(bytes);
			return bytes;
		}

		public byte[] GetBytes(uint _param1)
		{
			byte[] bytes = BitConverter.GetBytes(_param1);
			InverseConverter.luWWE001(bytes);
			return bytes;
		}

		public byte[] GetBytes(long _param1)
		{
			byte[] bytes = BitConverter.GetBytes(_param1);
			InverseConverter.luWWE002(bytes);
			return bytes;
		}

		public byte[] GetBytes(ulong _param1)
		{
			byte[] bytes = BitConverter.GetBytes(_param1);
			InverseConverter.luWWE002(bytes);
			return bytes;
		}

		public byte[] GetBytes(double _param1)
		{
			byte[] bytes = BitConverter.GetBytes(_param1);
			InverseConverter.luWWE002(bytes);
			return bytes;
		}

		public byte[] GetBytes(float _param1)
		{
			byte[] bytes = BitConverter.GetBytes(_param1);
			InverseConverter.luWWE002(bytes);
			return bytes;
		}

		public char ToChar(byte[] _param1) => BitConverter.ToChar(InverseConverter.full2BitInverse(_param1), 0);

		public short ToInt16(byte[] _param1) => BitConverter.ToInt16(InverseConverter.full2BitInverse(_param1), 0);

		public ushort ToUInt16(byte[] _param1) => BitConverter.ToUInt16(InverseConverter.full2BitInverse(_param1), 0);

		public int ToInt32(byte[] _param1) => BitConverter.ToInt32(InverseConverter.full4BitInverse(_param1), 0);

		public uint ToUInt32(byte[] _param1) => BitConverter.ToUInt32(InverseConverter.full4BitInverse(_param1), 0);

		public long ToInt64(byte[] _param1) => BitConverter.ToInt64(InverseConverter.full8BitInverse(_param1), 0);

		public ulong ToUInt64(byte[] _param1) => BitConverter.ToUInt64(InverseConverter.full8BitInverse(_param1), 0);

		public double ToDouble(byte[] _param1) => BitConverter.ToDouble(InverseConverter.full8BitInverse(_param1), 0);

		public float ToSingle(byte[] _param1) => BitConverter.ToSingle(InverseConverter.full4BitInverse(_param1), 0);

		public char ToChar(byte[] _param1, int _param2) => BitConverter.ToChar(InverseConverter.full2BitInverse(_param1), _param2);

		public short ToInt16(byte[] _param1, int _param2) => BitConverter.ToInt16(InverseConverter.luWWE006(_param1, _param2), 0);

		public ushort ToUInt16(byte[] _param1, int _param2) => BitConverter.ToUInt16(InverseConverter.luWWE006(_param1, _param2), 0);

		public int ToInt32(byte[] _param1, int _param2) => BitConverter.ToInt32(InverseConverter.luWWE007(_param1, _param2), 0);

		public uint ToUInt32(byte[] _param1, int _param2) => BitConverter.ToUInt32(InverseConverter.luWWE007(_param1, _param2), 0);

		public long ToInt64(byte[] _param1, int _param2) => BitConverter.ToInt64(InverseConverter.luWWE008(_param1, _param2), 0);

		public ulong ToUInt64(byte[] _param1, int _param2) => BitConverter.ToUInt64(InverseConverter.luWWE008(_param1, _param2), 0);

		public double ToDouble(byte[] _param1, int _param2) => BitConverter.ToDouble(InverseConverter.luWWE008(_param1, _param2), 0);

		public float ToSingle(byte[] _param1, int _param2) => BitConverter.ToSingle(InverseConverter.luWWE007(_param1, _param2), 0);

		private static void luWWE000(byte[] _param0)
		{
			byte num = _param0[0];
			_param0[0] = _param0[1];
			_param0[1] = num;
		}

		private static void luWWE001(byte[] _param0)
		{
			byte num1 = _param0[0];
			_param0[0] = _param0[3];
			_param0[3] = num1;

			byte num2 = _param0[1];
			_param0[1] = _param0[2];
			_param0[2] = num2;
		}

		private static void luWWE002(byte[] _param0)
		{
			byte num1 = _param0[0];
			_param0[0] = _param0[7];
			_param0[7] = num1;

			byte num2 = _param0[1];
			_param0[1] = _param0[6];
			_param0[6] = num2;

			byte num3 = _param0[2];
			_param0[2] = _param0[5];
			_param0[5] = num3;

			byte num4 = _param0[3];
			_param0[3] = _param0[4];
			_param0[4] = num4;
		}

		private static byte[] fullInverse(byte[] arr)
		{
			byte[] inverse = new byte[arr.Length];

			for (int i = arr.Length - 1, j = 0; j < arr.Length; i--, j++)
			{
				inverse[i] = arr[j];
			}

			return inverse;
		}
		[Obsolete("Replace for fullInverse")]
		private static byte[] full2BitInverse(byte[] _param0) => new byte[2]
		{
	_param0[1],
	_param0[0]
		};
		[Obsolete("Replace for fullInverse")]
		private static byte[] full4BitInverse(byte[] _param0) => new byte[4]
		{
	_param0[3],
	_param0[2],
	_param0[1],
	_param0[0]
		};
		[Obsolete("Replace for fullInverse")]
		private static byte[] full8BitInverse(byte[] arr) => new byte[8]
		{
	arr[7],
	arr[6],
	arr[5],
	arr[4],
	arr[3],
	arr[2],
	arr[1],
	arr[0]
		};

		private static byte[] luWWE006(byte[] _param0, int _param1) => new byte[2]
		{
	_param0[1 + _param1],
	_param0[_param1]
		};

		private static byte[] luWWE007(byte[] _param0, int _param1) => new byte[4]
		{
	_param0[3 + _param1],
	_param0[2 + _param1],
	_param0[1 + _param1],
	_param0[_param1]
		};

		private static byte[] luWWE008(byte[] _param0, int _param1) => new byte[8]
		{
	_param0[7 + _param1],
	_param0[6 + _param1],
	_param0[5 + _param1],
	_param0[4 + _param1],
	_param0[3 + _param1],
	_param0[2 + _param1],
	_param0[1 + _param1],
	_param0[_param1]
		};
	} 
}
