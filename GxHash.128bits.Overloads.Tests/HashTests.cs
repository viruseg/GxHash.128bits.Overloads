using System.Runtime.CompilerServices;
using GxHash.Overloads;

namespace GxHash._128bits.Overloads.Tests;

public class Tests
{
    [InlineArray(16)]
    private struct Buffer
    {
        public byte element0;
    }

    private struct TestStruct
    {
        public Buffer bytes;
    }

    [Test]
    public void Test()
    {
        byte[] arr = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];
        var arrHash = GxHash128.Hash128(arr);

        List<byte> list = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];
        var listHash = GxHash128.Hash128(list);

        var testStruct = new TestStruct();
        testStruct.bytes[0] = 1;
        testStruct.bytes[1] = 2;
        testStruct.bytes[2] = 3;
        testStruct.bytes[3] = 4;
        testStruct.bytes[4] = 5;
        testStruct.bytes[5] = 6;
        testStruct.bytes[6] = 7;
        testStruct.bytes[7] = 8;
        testStruct.bytes[8] = 9;
        testStruct.bytes[9] = 10;
        testStruct.bytes[10] = 11;
        testStruct.bytes[11] = 12;
        testStruct.bytes[12] = 13;
        testStruct.bytes[13] = 14;
        testStruct.bytes[14] = 15;
        testStruct.bytes[15] = 16;
        var structHash = GxHash128.Hash128(testStruct);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(arrHash, Is.EqualTo(structHash));
            Assert.That(listHash, Is.EqualTo(structHash));
        }
    }
}