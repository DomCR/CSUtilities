using System;

namespace CSUtilities.Converters;

#if PUBLIC

public
#else
internal
#endif
	class BigEndianConverter : EndianConverter
{
	public static BigEndianConverter Instance = new BigEndianConverter();

	public BigEndianConverter() : base(init())
	{
	}

	private static IEndianConverter init()
	{
		if (BitConverter.IsLittleEndian)
			return (IEndianConverter)new InverseConverter();
		else
			return (IEndianConverter)new DefaultEndianConverter();
	}
}