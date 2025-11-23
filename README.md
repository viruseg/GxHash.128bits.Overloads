# GxHash.128bits.Overloads

**GxHash.128bits.Overloads** is an extension library for [GxHash](https://github.com/ogxd/gxhash-csharp) that provides a comprehensive set of overloads to compute 128-bit hashes on various data types: files, string, ReadOnlySpan(T), Span(T), T[], List(T), Stream, T.

## Features

- Compute 128-bit hash (`UInt128`) for:
    - File contents (`string filePath`)
    - `string` values
    - `Span<byte>` and `ReadOnlySpan<byte>`
    - Generic `Span<T>` and `ReadOnlySpan<T>` where `T : unmanaged`
    - Arrays `T[]` where `T : unmanaged`
    - Lists `List<T>` where `T : unmanaged`
    - `Stream` instances
- Optional seeding for all methods

## Installation

Install via NuGet:

|Package|Download|
|-|-|
|GxHash.128bits.Overloads|[![NuGet](https://img.shields.io/nuget/v/GxHash.128bits.Overloads.svg)](https://www.nuget.org/packages/GxHash.128bits.Overloads) [![NuGet](https://img.shields.io/nuget/dt/GxHash.128bits.Overloads.svg)](https://www.nuget.org/packages/GxHash.128bits.Overloads)

```bash
dotnet add package GxHash.128bits.Overloads
```

## Quick Start

```csharp
using GxHash.Overloads;

class Program
{
    static void Main()
    {
        // Hash via extension methods
        {
            // Hash a string
            UInt128 hash1 = "Hello, World!".Hash128();
            Console.WriteLine($"String hash: {hash1}");

            // Hash with custom seed
            UInt128 seed = new UInt128(1234567890, 9876543210);
            UInt128 hash2 = "Hello, World!".Hash128(seed);
            Console.WriteLine($"String hash with seed: {hash2}");

            // Hash a file
            string path = "data.bin";
            UInt128 fileHash = File.Hash128(path); // Only for .NET 10 and higher
            Console.WriteLine($"File hash: {fileHash}");

            // Hash a byte span
            byte[] data = [1, 2, 3, 4];
            UInt128 spanHash = data.Hash128();
            Console.WriteLine($"Span hash: {spanHash}");

            // Hash a stream
            using var stream = File.OpenRead(path);
            UInt128 streamHash = stream.Hash128(seed: seed);
            Console.WriteLine($"Stream hash: {streamHash}");

            // Hash of a simple type
            long longValue = 1234567890;
            Console.WriteLine($"Long hash: {longValue.Hash128()}");

            // Hash of a struct
            ExampleStruct structValue = new ExampleStruct();
            Console.WriteLine($"Long hash: {structValue.Hash128()}");
        }



        // Hash via direct call
        {
            // Hash a string
            UInt128 hash1 = GxHash128.Hash128("Hello, World!");
            Console.WriteLine($"String hash: {hash1}");

            // Hash with custom seed
            UInt128 seed = new UInt128(1234567890, 9876543210);
            UInt128 hash2 = GxHash128.Hash128("Hello, World!", seed);
            Console.WriteLine($"String hash with seed: {hash2}");

            // Hash a file
            string path = "data.bin";
            UInt128 fileHash = GxHash128.FileContentHash128(path);
            Console.WriteLine($"File hash: {fileHash}");

            // Hash a byte span
            byte[] data = [1, 2, 3, 4];
            UInt128 spanHash = GxHash128.Hash128((ReadOnlySpan<byte>) data);
            Console.WriteLine($"Span hash: {spanHash}");

            // Hash a stream
            using var stream = File.OpenRead(path);
            UInt128 streamHash = GxHash128.Hash128(stream, seed);
            Console.WriteLine($"Stream hash: {streamHash}");

            // Hash of a simple type
            long longValue = 1234567890;
            UInt128 longHash = GxHash128.Hash128(longValue);
            Console.WriteLine($"Long hash: {longHash}");

            // Hash of a struct
            ExampleStruct structValue = new ExampleStruct();
            UInt128 structHash = GxHash128.Hash128(structValue);
            Console.WriteLine($"Struct hash: {structHash}");
        }
    }
}

struct ExampleStruct
{
    public int intValue;
    public long longValue;
}
```

## API Reference

```csharp
namespace GxHash.Overloads;

public static class GxHash128
{
    // File overloads
    public static UInt128 FileContentHash128(string filePath, int bufferSize = 4096, UInt128 seed = default, FileShare share = FileShare.Read)
    public static async Task<UInt128> FileContentHash128Async(string filePath, int bufferSize = 4096, UInt128 seed = default, FileShare share = FileShare.Read, CancellationToken cancellationToken = default)

    // String overloads
    public static UInt128 Hash128(string? str, UInt128 seed = default)

    // Byte ReadOnlySpan overloads
    public static UInt128 Hash128(ReadOnlySpan<byte> buffer, UInt128 seed = default)
    public static UInt128 Hash128<T>(ReadOnlySpan<T> buffer, UInt128 seed = default) where T : unmanaged

    // Byte Span overloads
    public static UInt128 Hash128(Span<byte> buffer, UInt128 seed = default)
    public static UInt128 Hash128<T>(Span<T> buffer, UInt128 seed = default) where T : unmanaged

    // Array overloads
    public static UInt128 Hash128<T>(T[]? array, UInt128 seed = default) where T : unmanaged

    // List overloads
    public static UInt128 Hash128<T>(List<T>? list, UInt128 seed = default) where T : unmanaged

    // Stream overload
    public static UInt128 Hash128(Stream stream, int bufferSize = 4096, UInt128 seed = default)
    public static async Task<UInt128> Hash128Async(Stream stream, int bufferSize = 4096, UInt128 seed = default, CancellationToken cancellationToken = default)

    // T overloads
    public static unsafe UInt128 Hash128<T>(T value, UInt128 seed = default) where T : unmanaged
}

public static class Hash128Extension
{
    extension(File)
    {
        public static UInt128 Hash128(string filePath, int bufferSize = 4096, UInt128 seed = default, FileShare share = FileShare.Read)
        public static async Task<UInt128> Hash128Async(string filePath, int bufferSize = 4096, UInt128 seed = default, FileShare share = FileShare.Read, CancellationToken cancellationToken = default)
    }
    
    extension(string? str)
    {
        public UInt128 Hash128(UInt128 seed = default)
    }
    
    extension<T>(ReadOnlySpan<T> buffer) where T : unmanaged
    {
        public UInt128 Hash128(UInt128 seed = default)
    }
    
    extension<T>(Span<T> buffer) where T : unmanaged
    {
        public UInt128 Hash128(UInt128 seed = default)
    }
    
    extension<T>(T[]? array) where T : unmanaged
    {
        public UInt128 Hash128(UInt128 seed = default)
    }
    
    extension<T>(List<T>? list) where T : unmanaged
    {
        public UInt128 Hash128(UInt128 seed = default)
    }
    
    extension(Stream stream)
    {
        public UInt128 Hash128(int bufferSize = 4096, UInt128 seed = default)
        
        public async Task<UInt128> Hash128Async(int bufferSize = 4096, UInt128 seed = default, CancellationToken cancellationToken = default)
    }
    
    extension<T>(T value) where T : unmanaged
    {
        public UInt128 Hash128(UInt128 seed = default)
    }
}

```

## Memory Padding and Hash Consistency
Unmanaged structures often contain padding bytes inserted by the compiler for memory alignment. These padding bytes are uninitialized and can contain arbitrary values from previous memory operations, causing identical logical data to produce different hash values. To ensure consistent hashing across different data sources and environments, it's essential to zero out padding bytes before hashing. For a detailed explanation of this issue and an automated solution, see the [StructPadding](https://github.com/viruseg/StructPadding), which provides high-performance clearing of padding bytes in unmanaged structures.