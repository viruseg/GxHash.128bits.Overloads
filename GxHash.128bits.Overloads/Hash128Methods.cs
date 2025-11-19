using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GxHash.Overloads;

/// <summary>
/// Methods for calculating 128-bit hashes.
/// </summary>
public static class GxHash128
{
    private static FileStream OpenFileStreamWhenAvailable(string filePath, FileShare share)
    {
        const int RETRY_DELAY_MS = 100;
        const int MAX_RETRIES = 50;
        var retryCount = 0;

        while (true)
        {
            try
            {
                return new FileStream(filePath, FileMode.Open, FileAccess.Read, share);
            }
            catch (IOException e)
            {
                retryCount++;
                if (retryCount >= MAX_RETRIES) throw new IOException(e.Message, null);
                Thread.Sleep(RETRY_DELAY_MS);
            }
        }
    }

    /// <summary>
    /// Computes a 128-bit unsigned integer hash for the content of a file.
    /// </summary>
    /// <param name="filePath">The path to the file to be hashed.</param>
    /// <param name="bufferSize">The size of the buffer to use when reading the file. Default is 4096 bytes.</param>
    /// <param name="share">The file sharing mode. Default is <see cref="FileShare.Read"/>.</param>
    /// <returns>A 128-bit hash.</returns>
    public static UInt128 FileContentHash128(string filePath, int bufferSize = 4096, FileShare share = FileShare.Read)
    {
        if (!File.Exists(filePath)) return default;

        using var stream = OpenFileStreamWhenAvailable(filePath, share);
        return Hash128(stream, default, bufferSize);
    }

    /// <summary>
    /// Computes a 128-bit unsigned integer hash for the content of a file using a specified seed.
    /// </summary>
    /// <param name="filePath">The path to the file to be hashed.</param>
    /// <param name="seed">A 128-bit seed.</param>
    /// <param name="bufferSize">The size of the buffer to use when reading the file. Default is 4096 bytes.</param>
    /// <param name="share">The file sharing mode. Default is <see cref="FileShare.Read"/>.</param>
    /// <returns>A 128-bit hash.</returns>
    public static UInt128 FileContentHash128(string filePath, UInt128 seed, int bufferSize = 4096, FileShare share = FileShare.Read)
    {
        if (!File.Exists(filePath)) return seed;

        using var stream = OpenFileStreamWhenAvailable(filePath, share);
        return Hash128(stream, seed, bufferSize);
    }

    /// <summary>
    /// Hash a given string into an 128-bit unsigned integer.
    /// </summary>
    /// <param name="str">The string to calculate the hash for.</param>
    /// <returns>A 128-bit hash.</returns>
    public static UInt128 Hash128(string? str)
    {
        return string.IsNullOrEmpty(str)
            ? default
            : Hash128(str.AsSpan());
    }

    /// <summary>
    /// Hash a given string into an 128-bit unsigned integer, using the given seed.
    /// </summary>
    /// <param name="str">The string to calculate the hash for.</param>
    /// <param name="seed">A 128-bit seed.</param>
    /// <returns>A 128-bit hash.</returns>
    public static UInt128 Hash128(string? str, UInt128 seed)
    {
        return string.IsNullOrEmpty(str)
            ? seed
            : Hash128(str.AsSpan(), seed);
    }

    /// <summary>
    /// Hash a span of bytes into an 128-bit unsigned integer.
    /// </summary>
    /// <param name="buffer">The span of bytes to calculate the hash for.</param>
    /// <returns>A 128-bit hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt128 Hash128(ReadOnlySpan<byte> buffer)
    {
        return Hash128(buffer, default);
    }

    /// <summary>
    /// Hash a span of bytes into an 128-bit unsigned integer, using the given seed.
    /// </summary>
    /// <param name="buffer">The span of bytes to calculate the hash for.</param>
    /// <param name="seed">A 128-bit seed.</param>
    /// <returns>A 128-bit hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt128 Hash128(ReadOnlySpan<byte> buffer, UInt128 seed)
    {
        if (buffer.Length == 0) return seed;

        return GxHash.Hash128(MemoryMarshal.AsBytes(buffer), seed);
    }

    /// <summary>
    /// Hash a span of <see cref="T"/> into an 128-bit unsigned integer.
    /// </summary>
    /// <param name="buffer">The span of <see cref="T"/> to calculate the hash for.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    /// <returns>A 128-bit hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt128 Hash128<T>(ReadOnlySpan<T> buffer) where T : unmanaged
    {
        if (buffer.Length == 0) return default;

        return Hash128(MemoryMarshal.AsBytes(buffer), default);
    }

    /// <summary>
    /// Hash a span of <see cref="T"/> into an 128-bit unsigned integer, using the given seed.
    /// </summary>
    /// <param name="buffer">The span of <see cref="T"/> to calculate the hash for.</param>
    /// <param name="seed">A 128-bit seed.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    /// <returns>A 128-bit hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt128 Hash128<T>(ReadOnlySpan<T> buffer, UInt128 seed) where T : unmanaged
    {
        if (buffer.Length == 0) return seed;

        return GxHash.Hash128(MemoryMarshal.AsBytes(buffer), seed);
    }

    /// <summary>
    /// Hash a span of bytes into an 128-bit unsigned integer.
    /// </summary>
    /// <param name="buffer">The span of bytes to calculate the hash for.</param>
    /// <returns>A 128-bit hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt128 Hash128(Span<byte> buffer)
    {
        return Hash128(buffer, default);
    }

    /// <summary>
    /// Hash a span of bytes into an 128-bit unsigned integer, using the given seed.
    /// </summary>
    /// <param name="buffer">The span of bytes to calculate the hash for.</param>
    /// <param name="seed">A 128-bit seed.</param>
    /// <returns>A 128-bit hash.</returns>
    public static UInt128 Hash128(Span<byte> buffer, UInt128 seed)
    {
        if (buffer.Length == 0) return seed;

        return Hash128(MemoryMarshal.CreateReadOnlySpan(ref MemoryMarshal.GetReference(buffer), buffer.Length), seed);
    }

    /// <summary>
    /// Hash a span of <see cref="T"/> into an 128-bit unsigned integer.
    /// </summary>
    /// <param name="buffer">The span of <see cref="T"/> to calculate the hash for.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    /// <returns>A 128-bit hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt128 Hash128<T>(Span<T> buffer) where T : unmanaged
    {
        return Hash128(MemoryMarshal.AsBytes(buffer), default);
    }

    /// <summary>
    /// Hash a span of <see cref="T"/> into an 128-bit unsigned integer, using the given seed.
    /// </summary>
    /// <param name="buffer">The span of <see cref="T"/> to calculate the hash for.</param>
    /// <param name="seed">A 128-bit seed.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    /// <returns>A 128-bit hash.</returns>
    public static UInt128 Hash128<T>(Span<T> buffer, UInt128 seed) where T : unmanaged
    {
        if (buffer.Length == 0) return seed;

        var asBytes = MemoryMarshal.AsBytes(buffer);
        return Hash128(MemoryMarshal.CreateReadOnlySpan(ref MemoryMarshal.GetReference(asBytes), buffer.Length), seed);
    }

    /// <summary>
    /// Hash a array into an 128-bit unsigned integer.
    /// </summary>
    /// <param name="array">The array to calculate the hash for.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    /// <returns>A 128-bit hash.</returns>
    public static UInt128 Hash128<T>(T[]? array) where T : unmanaged
    {
        if (array == null || array.Length == 0) return default;

        return Hash128(array.AsSpan());
    }

    /// <summary>
    /// Hash a array into an 128-bit unsigned integer, using the given seed.
    /// </summary>
    /// <param name="array">The array to calculate the hash for.</param>
    /// <param name="seed">A 128-bit seed.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    /// <returns>A 128-bit hash.</returns>
    public static UInt128 Hash128<T>(T[]? array, UInt128 seed) where T : unmanaged
    {
        if (array == null || array.Length == 0) return seed;

        return Hash128(MemoryMarshal.AsBytes(array.AsSpan()), seed);
    }

    /// <summary>
    /// Hash a array into an 128-bit unsigned integer.
    /// </summary>
    /// <param name="list">The list to calculate the hash for.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    /// <returns>A 128-bit hash.</returns>
    public static UInt128 Hash128<T>(List<T>? list) where T : unmanaged
    {
        if (list == null || list.Count == 0) return default;

        return Hash128(MemoryMarshal.AsBytes(CollectionsMarshal.AsSpan(list)), default);
    }

    /// <summary>
    /// Hash a list into an 128-bit unsigned integer, using the given seed.
    /// </summary>
    /// <param name="list">The list to calculate the hash for.</param>
    /// <param name="seed">A 128-bit seed.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    /// <returns>A 128-bit hash.</returns>
    public static UInt128 Hash128<T>(List<T>? list, UInt128 seed) where T : unmanaged
    {
        if (list == null || list.Count == 0) return seed;

        return Hash128(MemoryMarshal.AsBytes(CollectionsMarshal.AsSpan(list)), seed);
    }

    /// <summary>
    /// Hash a stream into an 128-bit unsigned integer.
    /// </summary>
    /// <param name="stream">The stream to calculate the hash for.</param>
    /// <param name="bufferSize">The size of the buffer to use for reading from the stream. Defaults to 4096.</param>
    /// <returns>A 128-bit hash.</returns>
    [SkipLocalsInit]
    public static unsafe UInt128 Hash128(Stream stream, int bufferSize = 4096)
    {
        if (bufferSize <= 0) bufferSize = 4096;

        UInt128 hash = default;
        Span<byte> buffer = stackalloc byte[bufferSize];

        int bytesRead;
        while ((bytesRead = stream.Read(buffer)) > 0)
        {
            hash = Hash128(buffer.Slice(0, bytesRead), hash);
        }

        return hash;
    }

    /// <summary>
    /// Hash a stream into an 128-bit unsigned integer, using the given seed.
    /// </summary>
    /// <param name="stream">The stream to calculate the hash for.</param>
    /// <param name="seed">A 128-bit seed.</param>
    /// <param name="bufferSize">The size of the buffer to use for reading from the stream. Defaults to 4096.</param>
    /// <returns>A 128-bit hash.</returns>
    [SkipLocalsInit]
    public static unsafe UInt128 Hash128(Stream stream, UInt128 seed, int bufferSize = 4096)
    {
        if (bufferSize <= 0) bufferSize = 4096;

        var hash = seed;
        Span<byte> buffer = stackalloc byte[bufferSize];

        int bytesRead;
        while ((bytesRead = stream.Read(buffer)) > 0)
        {
            hash = Hash128(buffer.Slice(0, bytesRead), hash);
        }

        return hash;
    }

    /// <summary>
    /// Hash a <see cref="T"/> into an 128-bit unsigned integer.
    /// </summary>
    /// <param name="value">The struct to calculate the hash for.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    /// <returns>A 128-bit hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe UInt128 Hash128<T>(T value) where T : unmanaged
    {
        var structAsSpan = new ReadOnlySpan<byte>(&value, sizeof(T));
        return GxHash.Hash128(structAsSpan, default);
    }

    /// <summary>
    /// Hash a <see cref="T"/> into an 128-bit unsigned integer, using the given seed.
    /// </summary>
    /// <param name="value">The struct to calculate the hash for.</param>
    /// <param name="seed">A 128-bit seed.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    /// <returns>A 128-bit hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe UInt128 Hash128<T>(T value, UInt128 seed) where T : unmanaged
    {
        var structAsSpan = new ReadOnlySpan<byte>(&value, sizeof(T));
        return GxHash.Hash128(structAsSpan, seed);
    }
}