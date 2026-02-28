using System;

namespace CSUtilities.Converters;

#if PUBLIC
public
#else
	internal
#endif
class LittleEndianConverter : EndianConverter
{
	public static LittleEndianConverter Instance = new LittleEndianConverter();

	static IEndianConverter init()
	{
		if (BitConverter.IsLittleEndian)
			return (IEndianConverter)new DefaultEndianConverter();
		else
			return (IEndianConverter)new InverseConverter();
	}

	public LittleEndianConverter() : base(init()) { }
}