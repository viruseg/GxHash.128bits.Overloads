# GxHash.128bits.Overloads

**GxHash.128bits.Overloads** is an extension library for [GxHash](https://www.nuget.org/packages/GxHash) that provides a comprehensive set of overloads to compute 128-bit hashes on various data types: files, string, ReadOnlySpan(T), Span(T), T[], Stream.

## Features

- Compute 128-bit hash (`UInt128`) for:
    - File contents (`string filePath`)
    - `string` values
    - `Span<byte>` and `ReadOnlySpan<byte>`
    - Generic `Span<T>` and `ReadOnlySpan<T>` where `T : unmanaged`
    - Arrays `T[]` where `T : unmanaged`
    - `Stream` instances
- Optional seeding for all methods

## Installation

Install via NuGet:

|Package|Download|
|-|-|
|GxHash.128bits.Overloads|[![NuGet](https://img.shields.io/nuget/v/GxHash.128bits.Overloads.svg)](https://www.nuget.org/packages/GxHash.128bits.Overloads)

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
        byte[] data = { 1, 2, 3, 4 };
        UInt128 spanHash = GxHash128.Hash128((ReadOnlySpan<byte>)data);
        Console.WriteLine($"Span hash: {spanHash}");

        // Hash a stream
        using var stream = File.OpenRead(path);
        UInt128 streamHash = GxHash128.Hash128(stream, seed);
        Console.WriteLine($"Stream hash: {streamHash}");
    }
}
```

## API Reference

```csharp
namespace GxHash.Overloads;

public static class GxHash128
{
    // File overloads
    public static UInt128 FileContentHash128(string filePath);
    public static UInt128 FileContentHash128(string filePath, UInt128 seed);
    
    // String overloads
    public static UInt128 Hash128(string? str);
    public static UInt128 Hash128(string? str, UInt128 seed);
    
    // Byte ReadOnlySpan overloads
    public static UInt128 Hash128(ReadOnlySpan<byte> buffer);
    public static UInt128 Hash128(ReadOnlySpan<byte> buffer, UInt128 seed);
    public static UInt128 Hash128<T>(ReadOnlySpan<T> buffer) where T : unmanaged;
    public static UInt128 Hash128<T>(ReadOnlySpan<T> buffer, UInt128 seed) where T : unmanaged;

    // Byte Span overloads
    public static UInt128 Hash128(Span<byte> buffer);
    public static UInt128 Hash128(Span<byte> buffer, UInt128 seed);
    public static UInt128 Hash128<T>(Span<T> buffer) where T : unmanaged;
    public static UInt128 Hash128<T>(Span<T> buffer, UInt128 seed) where T : unmanaged;
    
    // Array overloads
    public static UInt128 Hash128<T>(T[]? array) where T : unmanaged;
    public static UInt128 Hash128<T>(T[]? array, UInt128 seed) where T : unmanaged;
    
    // Stream overload
    public static UInt128 Hash128(Stream stream, UInt128 seed);
}

```