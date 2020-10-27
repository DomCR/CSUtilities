using System;

namespace CSUtilities.Converters
{
	internal class InverseConverter : IEndianConverter
	{
		public byte[] GetBytes(char value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			InverseConverter.luWWE000(bytes);
			return bytes;
		}

		public byte[] GetBytes(short value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			InverseConverter.luWWE000(bytes);
			return bytes;
		}

		public byte[] GetBytes(ushort value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			InverseConverter.luWWE000(bytes);
			return bytes;
		}

		public byte[] GetBytes(int value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			InverseConverter.luWWE001(bytes);
			return bytes;
		}

		public byte[] GetBytes(uint value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			InverseConverter.luWWE001(bytes);
			return bytes;
		}

		public byte[] GetBytes(long value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			InverseConverter.luWWE002(bytes);
			return bytes;
		}

		public byte[] GetBytes(ulong value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			InverseConverter.luWWE002(bytes);
			return bytes;
		}

		public byte[] GetBytes(double value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			InverseConverter.luWWE002(bytes);
			return bytes;
		}

		public byte[] GetBytes(float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			InverseConverter.luWWE002(bytes);
			return bytes;
		}

		public char ToChar(byte[] arr) => BitConverter.ToChar(InverseConverter.fullInverse(arr), 0);

		public short ToInt16(byte[] arr) => BitConverter.ToInt16(InverseConverter.fullInverse(arr), 0);

		public ushort ToUInt16(byte[] arr) => BitConverter.ToUInt16(InverseConverter.fullInverse(arr), 0);

		public int ToInt32(byte[] arr) => BitConverter.ToInt32(InverseConverter.fullInverse(arr), 0);

		public uint ToUInt32(byte[] arr) => BitConverter.ToUInt32(InverseConverter.fullInverse(arr), 0);

		public long ToInt64(byte[] arr) => BitConverter.ToInt64(InverseConverter.fullInverse(arr), 0);

		public ulong ToUInt64(byte[] arr) => BitConverter.ToUInt64(InverseConverter.fullInverse(arr), 0);

		public double ToDouble(byte[] arr) => BitConverter.ToDouble(InverseConverter.fullInverse(arr), 0);

		public float ToSingle(byte[] arr) => BitConverter.ToSingle(InverseConverter.fullInverse(arr), 0);

		public char ToChar(byte[] arr, int length) => BitConverter.ToChar(InverseConverter.fullInverse(arr), length);

		public short ToInt16(byte[] arr, int length) => BitConverter.ToInt16(InverseConverter.luWWE006(arr, length), 0);

		public ushort ToUInt16(byte[] arr, int length) => BitConverter.ToUInt16(InverseConverter.luWWE006(arr, length), 0);

		public int ToInt32(byte[] arr, int length) => BitConverter.ToInt32(InverseConverter.luWWE007(arr, length), 0);

		public uint ToUInt32(byte[] arr, int length) => BitConverter.ToUInt32(InverseConverter.luWWE007(arr, length), 0);

		public long ToInt64(byte[] arr, int length) => BitConverter.ToInt64(InverseConverter.luWWE008(arr, length), 0);

		public ulong ToUInt64(byte[] arr, int length) => BitConverter.ToUInt64(InverseConverter.luWWE008(arr, length), 0);

		public double ToDouble(byte[] arr, int length) => BitConverter.ToDouble(InverseConverter.luWWE008(arr, length), 0);

		public float ToSingle(byte[] arr, int length) => BitConverter.ToSingle(InverseConverter.luWWE007(arr, length), 0);

		private static void luWWE000(byte[] arr)
		{
			byte num = arr[0];
			arr[0] = arr[1];
			arr[1] = num;
		}

		private static void luWWE001(byte[] arr)
		{
			byte num1 = arr[0];
			arr[0] = arr[3];
			arr[3] = num1;

			byte num2 = arr[1];
			arr[1] = arr[2];
			arr[2] = num2;
		}

		private static void luWWE002(byte[] arr)
		{
			byte num1 = arr[0];
			arr[0] = arr[7];
			arr[7] = num1;

			byte num2 = arr[1];
			arr[1] = arr[6];
			arr[6] = num2;

			byte num3 = arr[2];
			arr[2] = arr[5];
			arr[5] = num3;

			byte num4 = arr[3];
			arr[3] = arr[4];
			arr[4] = num4;
		}

		private static void inversePairs(byte[] arr)
		{
			for (int i = arr.Length - 1, j = 0; (i - j) > 0; i--, j++)
			{
				byte num1 = arr[j];
				arr[j] = arr[i];
				arr[i] = num1;
			}
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

		private static byte[] luWWE006(byte[] arr, int length) => new byte[2]
		{
	arr[1 + length],
	arr[length]
		};

		private static byte[] luWWE007(byte[] arr, int length) => new byte[4]
		{
	arr[3 + length],
	arr[2 + length],
	arr[1 + length],
	arr[length]
		};

		private static byte[] luWWE008(byte[] arr, int length) => new byte[8]
		{
	arr[7 + length],
	arr[6 + length],
	arr[5 + length],
	arr[4 + length],
	arr[3 + length],
	arr[2 + length],
	arr[1 + length],
	arr[length]
		};
	}
}
