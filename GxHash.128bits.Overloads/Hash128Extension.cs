using System.Runtime.CompilerServices;

namespace GxHash.Overloads;

/// <summary>
/// Extension methods for calculating 128-bit hashes.
/// </summary>
public static class Hash128Extension
{
    /// <summary>
    /// Extension methods for calculating 128-bit hashes on files.
    /// </summary>
    extension(File)
    {
        /// <summary>
        /// Computes a 128-bit unsigned integer hash for the content of a file using a specified seed.
        /// </summary>
        /// <param name="filePath">The path to the file to be hashed.</param>
        /// <param name="seed">A 128-bit seed.</param>
        /// <param name="bufferSize">The size of the buffer to use when reading the file. Default is 4096 bytes.</param>
        /// <param name="share">The file sharing mode. Default is <see cref="FileShare.Read"/>.</param>
        /// <returns>A 128-bit hash.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 Hash128(string filePath, UInt128 seed = default, int bufferSize = 4096, FileShare share = FileShare.Read)
        {
            return GxHash128.FileContentHash128(filePath, seed, bufferSize, share);
        }
    }

    /// <param name="str">The string to calculate the hash for.</param>
    extension(string? str)
    {
        /// <summary>
        /// Hash a given string into an 128-bit unsigned integer, using the given seed.
        /// </summary>
        /// <param name="seed">A 128-bit seed.</param>
        /// <returns>A 128-bit hash.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt128 Hash128(UInt128 seed = default)
        {
            return GxHash128.Hash128(str, seed);
        }
    }

    /// <param name="buffer">The span of <typeparamref name="T"/> to calculate the hash for.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    extension<T>(ReadOnlySpan<T> buffer) where T : unmanaged
    {
        /// <summary>
        /// Hash a span of <typeparamref name="T"/> into an 128-bit unsigned integer, using the given seed.
        /// </summary>
        /// <param name="seed">A 128-bit seed.</param>
        /// <returns>A 128-bit hash.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt128 Hash128(UInt128 seed = default)
        {
            return GxHash128.Hash128(buffer, seed);
        }
    }

    /// <param name="buffer">The span of <typeparamref name="T"/> to calculate the hash for.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    extension<T>(Span<T> buffer) where T : unmanaged
    {
        /// <summary>
        /// Hash a span of <typeparamref name="T"/> into an 128-bit unsigned integer, using the given seed.
        /// </summary>
        /// <param name="seed">A 128-bit seed.</param>
        /// <returns>A 128-bit hash.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt128 Hash128(UInt128 seed = default)
        {
            return GxHash128.Hash128(buffer, seed);
        }
    }

    /// <param name="array">The array to calculate the hash for.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    extension<T>(T[]? array) where T : unmanaged
    {
        /// <summary>
        /// Hash a array into an 128-bit unsigned integer, using the given seed.
        /// </summary>
        /// <param name="seed">A 128-bit seed.</param>
        /// <returns>A 128-bit hash.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt128 Hash128(UInt128 seed = default)
        {
            return GxHash128.Hash128(array, seed);
        }
    }

    /// <param name="list">The list to calculate the hash for.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    extension<T>(List<T>? list) where T : unmanaged
    {
        /// <summary>
        /// Hash a list into an 128-bit unsigned integer, using the given seed.
        /// </summary>
        /// <param name="seed">A 128-bit seed.</param>
        /// <returns>A 128-bit hash.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt128 Hash128(UInt128 seed = default)
        {
            return GxHash128.Hash128(list, seed);
        }
    }

    /// <param name="stream">The stream to calculate the hash for.</param>
    extension(Stream stream)
    {
        /// <summary>
        /// Hash a stream into an 128-bit unsigned integer, using the given seed.
        /// </summary>
        /// <param name="seed">A 128-bit seed.</param>
        /// <param name="bufferSize">The size of the buffer to use for reading from the stream. Defaults to 4096.</param>
        /// <returns>A 128-bit hash.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt128 Hash128(UInt128 seed = default, int bufferSize = 4096)
        {
            return GxHash128.Hash128(stream, seed, bufferSize);
        }
    }

    /// <param name="value">The struct to calculate the hash for.</param>
    /// <typeparam name="T">The unmanaged type.</typeparam>
    extension<T>(T value) where T : unmanaged
    {
        /// <summary>
        /// Hash a <typeparamref name="T"/> into an 128-bit unsigned integer, using the given seed.
        /// </summary>
        /// <param name="seed">A 128-bit seed.</param>
        /// <returns>A 128-bit hash.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt128 Hash128(UInt128 seed = default)
        {
            return GxHash128.Hash128(value, seed);
        }
    }
}