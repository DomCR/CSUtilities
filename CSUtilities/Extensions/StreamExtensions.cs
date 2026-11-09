namespace CSUtilities.Extensions;

/// <summary>
/// Stream utility extensions.
/// </summary>
#if PUBLIC
public
#else
internal
#endif
static class StreamExtensions
{
#if NETFRAMEWORK
	/// <summary>
	/// When overridden in a derived class, writes a sequence of bytes to the current
	/// stream and advances the current position within this stream by the number of
	/// bytes written.
	/// </summary>
	/// <param name="stream"></param>
	/// <param name="buffer">A region of memory. This method copies the contents of this region to the current stream. </param>
	public static void Write(this System.IO.Stream stream, byte[] buffer)
	{
		stream.Write(buffer, 0, buffer.Length);
	}
#endif

#if !NET7_0_OR_GREATER
	/// <summary>
	/// Reads exactly <paramref name="count"/> bytes from the stream into <paramref name="buffer"/>,
	/// calling <see cref="System.IO.Stream.Read(byte[], int, int)"/> until they are all read.
	/// </summary>
	/// <remarks>
	/// Stands in for <c>Stream.ReadExactly</c>, which .NET 7 and later provide, so one call site
	/// compiles on every target. A single <c>Read</c> may return fewer bytes than asked without the
	/// stream being at its end.
	/// </remarks>
	/// <exception cref="System.IO.EndOfStreamException">The stream ended before <paramref name="count"/> bytes were read.</exception>
	public static void ReadExactly(this System.IO.Stream stream, byte[] buffer, int offset, int count)
	{
		while (count > 0)
		{
			int read = stream.Read(buffer, offset, count);
			if (read <= 0)
				throw new System.IO.EndOfStreamException();

			offset += read;
			count -= read;
		}
	}

	/// <summary>
	/// Asynchronously reads exactly <paramref name="count"/> bytes from the stream into <paramref name="buffer"/>.
	/// </summary>
	/// <remarks>Stands in for <c>Stream.ReadExactlyAsync</c>, which .NET 7 and later provide.</remarks>
	/// <exception cref="System.IO.EndOfStreamException">The stream ended before <paramref name="count"/> bytes were read.</exception>
	public static async System.Threading.Tasks.Task ReadExactlyAsync(this System.IO.Stream stream, byte[] buffer, int offset, int count,
		System.Threading.CancellationToken cancellationToken = default)
	{
		while (count > 0)
		{
			int read = await stream.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
			if (read <= 0)
				throw new System.IO.EndOfStreamException();

			offset += read;
			count -= read;
		}
	}
#endif
}
