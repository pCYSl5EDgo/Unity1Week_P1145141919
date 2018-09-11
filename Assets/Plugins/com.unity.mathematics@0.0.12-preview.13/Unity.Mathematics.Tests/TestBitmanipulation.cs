// GENERATED CODE
using NUnit.Framework;
using static Unity.Mathematics.math;
namespace Unity.Mathematics.Tests
{
    [TestFixture]
    public class TestBitmanipulation
    {
        [Test]
        public void countbits_int()
        {
            Assert.AreEqual(countbits(0), 0);
            Assert.AreEqual(countbits( 0x01234567), 12);
            Assert.AreEqual(countbits( 0x456789AB), 16);
            Assert.AreEqual(countbits(-0x01234567), 21);
            Assert.AreEqual(countbits(-0x456789AB), 17);
            Assert.AreEqual(countbits(-1), 32);
        }

        [Test]
        public void countbits_int2()
        {
            Assert.AreEqual(countbits(int2(0, 0x01234567)), int2(0, 12));
            Assert.AreEqual(countbits(int2(0x456789AB, -0x01234567)), int2(16, 21));
            Assert.AreEqual(countbits(int2(-0x456789AB, -1)), int2(17, 32));
        }

        [Test]
        public void count_bits_int3()
        {
            Assert.AreEqual(countbits(int3(0, 0x01234567, 0x456789AB)), int3(0, 12, 16));
            Assert.AreEqual(countbits(int3(-0x01234567, -0x456789AB, -1)), int3(21, 17, 32));
        }

        [Test]
        public void count_bits_int4()
        {
            Assert.AreEqual(countbits(int4(0, 0x01234567, 0x456789AB, -0x01234567)), int4(0, 12, 16, 21));
            Assert.AreEqual(countbits(int4(-0x456789AB, -1, -7, -15)), int4(17, 32, 30, 29));
        }

        [Test]
        public void count_bits_uint()
        {
            Assert.AreEqual(countbits(0u), 0);
            Assert.AreEqual(countbits(0x01234567u), 12);
            Assert.AreEqual(countbits(0x456789ABu), 16);
            Assert.AreEqual(countbits(0x89ABCDEFu), 20);
            Assert.AreEqual(countbits(0xCDEF0123u), 16);
            Assert.AreEqual(countbits(0xFFFFFFFFu), 32);
        }

        [Test]
        public void count_bits_uint2()
        {
            Assert.AreEqual(countbits(uint2(0, 0x01234567)), int2(0, 12));
            Assert.AreEqual(countbits(uint2(0x456789AB, 0x89ABCDEFu)), int2(16, 20));
            Assert.AreEqual(countbits(uint2(0xCDEF0123u, 0xFFFFFFFFu)), int2(16, 32));
        }

        [Test]
        public void count_bits_uint3()
        {
            Assert.AreEqual(countbits(uint3(0, 0x01234567, 0x456789AB)), int3(0, 12, 16));
            Assert.AreEqual(countbits(uint3(0x89ABCDEFu, 0xCDEF0123u, 0xFFFFFFFFu)), int3(20, 16, 32));
        }

        [Test]
        public void count_bits_uint4()
        {
            Assert.AreEqual(countbits(uint4(0, 0x01234567, 0x456789AB, 0x89ABCDEFu)), int4(0, 12, 16, 20));
            Assert.AreEqual(countbits(uint4(0xCDEF0123u, 0xFFFFFFFFu, 0xFFFFFFF5u, 0xFFFFFFF1u)), int4(16, 32, 30, 29));
        }

        [Test]
        public void count_bits_long()
        {
            Assert.AreEqual(countbits(0L), 0);
            Assert.AreEqual(countbits(0x0123456789ABCDEFL), 32);
            Assert.AreEqual(countbits(-0x0123456789ABCDEFL), 33);
            Assert.AreEqual(countbits(-1L), 64);
        }

        [Test]
        public void count_bits_ulong()
        {
            Assert.AreEqual(countbits(0UL), 0);
            Assert.AreEqual(countbits(0x0123456789ABCDEFUL), 32);
            Assert.AreEqual(countbits(0x89ABCDEF01234567UL), 32);
            Assert.AreEqual(countbits(0xFFFFFFFFFFFFFFFFUL), 64);
        }

        [Test]
        public void lzcnt_int()
        {
            Assert.AreEqual(lzcnt(0), 32);
            Assert.AreEqual(lzcnt(1), 31);
            Assert.AreEqual(lzcnt(2), 30);
            Assert.AreEqual(lzcnt(3), 30);
            Assert.AreEqual(lzcnt(0x5321), 17);
            Assert.AreEqual(lzcnt(0x04435321), 5);
            Assert.AreEqual(lzcnt(-1), 0);
            Assert.AreEqual(lzcnt(-2147483648), 0);
        }

        [Test]
        public void lzcnt_int2()
        {
            Assert.AreEqual(lzcnt(int2(0, 1)), int2(32, 31));
            Assert.AreEqual(lzcnt(int2(2, 3)), int2(30, 30));
            Assert.AreEqual(lzcnt(int2(0x5321, 0x04435321)), int2(17, 5));
            Assert.AreEqual(lzcnt(int2(-1, -2147483648)), int2(0, 0));
        }

        [Test]
        public void lzcnt_int3()
        {
            Assert.AreEqual(lzcnt(int3(0, 1, 2)), int3(32, 31, 30));
            Assert.AreEqual(lzcnt(int3(3, 0x5321, 0x04435321)), int3(30, 17, 5));
            Assert.AreEqual(lzcnt(int3(-1, -2147483648, 17)), int3(0, 0, 27));
        }

        [Test]
        public void lzcnt_int4()
        {
            Assert.AreEqual(lzcnt(int4(0, 1, 2, 3)), int4(32, 31, 30, 30));
            Assert.AreEqual(lzcnt(int4(0x5321, 0x04435321, -1, -2147483648)), int4(17, 5, 0, 0));
        }

        [Test]
        public void lzcnt_uint()
        {
            Assert.AreEqual(lzcnt(0u), 32);
            Assert.AreEqual(lzcnt(1u), 31);
            Assert.AreEqual(lzcnt(2u), 30);
            Assert.AreEqual(lzcnt(3u), 30);
            Assert.AreEqual(lzcnt(0x5321u), 17);
            Assert.AreEqual(lzcnt(0x04435321u), 5);
            Assert.AreEqual(lzcnt(0x84435320u), 0);
            Assert.AreEqual(lzcnt(0xFFFFFFFFu), 0);
        }

        [Test]
        public void lzcnt_uint2()
        {
            Assert.AreEqual(lzcnt(uint2(0u, 1u)), int2(32, 31));
            Assert.AreEqual(lzcnt(uint2(2u, 3u)), int2(30, 30));
            Assert.AreEqual(lzcnt(uint2(0x5321u, 0x04435321u)), int2(17, 5));
            Assert.AreEqual(lzcnt(uint2(0x84435320u, 0xFFFFFFFFu)), int2(0, 0));
        }

        [Test]
        public void lzcnt_uint3()
        {
            Assert.AreEqual(lzcnt(uint3(0u, 1u, 2u)), int3(32, 31, 30));
            Assert.AreEqual(lzcnt(uint3(3u, 0x5321u, 0x04435321u)), int3(30, 17, 5));
            Assert.AreEqual(lzcnt(uint3(0x84435320u, 0xFFFFFFFFu, 17)), int3(0, 0, 27));
        }

        [Test]
        public void lzcnt_uint4()
        {
            Assert.AreEqual(lzcnt(uint2(0u, 1u)), int2(32, 31));
            Assert.AreEqual(lzcnt(uint2(2u, 3u)), int2(30, 30));
            Assert.AreEqual(lzcnt(uint2(0x5321u, 0x04435321u)), int2(17, 5));
            Assert.AreEqual(lzcnt(uint2(0x84435320u, 0xFFFFFFFFu)), int2(0, 0));
        }

        [Test]
        public void lzcnt_long()
        {
            Assert.AreEqual(lzcnt(0L), 64);
            Assert.AreEqual(lzcnt(1L), 63);
            Assert.AreEqual(lzcnt(0x1FFF1234L), 35);
            Assert.AreEqual(lzcnt(0x1FFFF1234L), 31);
            Assert.AreEqual(lzcnt(0x1FFFFFFF1234L), 19);
            Assert.AreEqual(lzcnt(-1L), 0);
            Assert.AreEqual(lzcnt(-9223372036854775808L), 0);
        }

        [Test]
        public void lzcnt_ulong()
        {
            Assert.AreEqual(lzcnt(0UL), 64);
            Assert.AreEqual(lzcnt(1UL), 63);
            Assert.AreEqual(lzcnt(0x1FFF1234UL), 35);
            Assert.AreEqual(lzcnt(0x1FFFF1234UL), 31);
            Assert.AreEqual(lzcnt(0x1FFFFFFF1234UL), 19);
            Assert.AreEqual(lzcnt(0xFFFFFFFFFFFFFFFFUL), 0);
            Assert.AreEqual(lzcnt(0x8000000000000000UL), 0);
        }

        [Test]
        public void tzcnt_int()
        {
            Assert.AreEqual(tzcnt(0), 32);
            Assert.AreEqual(tzcnt(1), 0);
            Assert.AreEqual(tzcnt(2), 1);
            Assert.AreEqual(tzcnt(3), 0);
            Assert.AreEqual(tzcnt(0x53210), 4);
            Assert.AreEqual(tzcnt(0x44420000), 17);
            Assert.AreEqual(tzcnt(-2), 1);
            Assert.AreEqual(tzcnt(-2147483647), 0);
            Assert.AreEqual(tzcnt(-2147483648), 31);
        }

        [Test]
        public void tzcnt_int2()
        {
            Assert.AreEqual(tzcnt(int2(0, 1)), int2(32, 0));
            Assert.AreEqual(tzcnt(int2(2, 3)), int2(1, 0));
            Assert.AreEqual(tzcnt(int2(0x53210, 0x44420000)), int2(4, 17));
            Assert.AreEqual(tzcnt(int2(-2, -2147483647)), int2(1, 0));
            Assert.AreEqual(tzcnt(int2(-2147483648, 20)), int2(31, 2));
        }

        [Test]
        public void tzcnt_int3()
        {
            Assert.AreEqual(tzcnt(int3(0, 1, 2)), int3(32, 0, 1));
            Assert.AreEqual(tzcnt(int3(3, 0x53210, 0x44420000)), int3(0, 4, 17));
            Assert.AreEqual(tzcnt(int3(-2, -2147483647, -2147483648)), int3(1, 0, 31));
        }

        [Test]
        public void tzcnt_int4()
        {
            Assert.AreEqual(tzcnt(int4(0, 1, 2, 3)), int4(32, 0, 1, 0));
            Assert.AreEqual(tzcnt(int4(0x53210, 0x44420000, -2, -2147483647)), int4(4, 17, 1, 0));
            Assert.AreEqual(tzcnt(int4(-2147483648, 20, 4132, -8)), int4(31, 2, 2, 3));
        }

        [Test]
        public void tzcnt_uint()
        {
            Assert.AreEqual(tzcnt(0u), 32);
            Assert.AreEqual(tzcnt(1u), 0);
            Assert.AreEqual(tzcnt(2u), 1);
            Assert.AreEqual(tzcnt(3u), 0);
            Assert.AreEqual(tzcnt(0x53210u), 4);
            Assert.AreEqual(tzcnt(0x44420000u), 17);
            Assert.AreEqual(tzcnt(0xFFFFFFFEu), 1);
            Assert.AreEqual(tzcnt(0x80000001u), 0);
            Assert.AreEqual(tzcnt(0x80000000u), 31);
        }

        [Test]
        public void tzcnt_uint2()
        {
            Assert.AreEqual(tzcnt(uint2(0u, 1u)), int2(32, 0));
            Assert.AreEqual(tzcnt(uint2(2u, 3u)), int2(1, 0));
            Assert.AreEqual(tzcnt(uint2(0x53210u, 0x44420000u)), int2(4, 17));
            Assert.AreEqual(tzcnt(uint2(0xFFFFFFFEu, 0x80000001u)), int2(1, 0));
            Assert.AreEqual(tzcnt(uint2(0x80000000u, 20u)), int2(31, 2));
        }

        [Test]
        public void tzcnt_uint3()
        {
            Assert.AreEqual(tzcnt(uint3(0u, 1u, 2u)), int3(32, 0, 1));
            Assert.AreEqual(tzcnt(uint3(3u, 0x53210u, 0x44420000u)), int3(0, 4, 17));
            Assert.AreEqual(tzcnt(uint3(0xFFFFFFFEu, 0x80000001u, 0x80000000u)), int3(1, 0, 31));
        }

        [Test]
        public void tzcnt_uint4()
        {
            Assert.AreEqual(tzcnt(uint4(0u, 1u, 2u, 3u)), int4(32, 0, 1, 0));
            Assert.AreEqual(tzcnt(uint4(0x53210u, 0x44420000u, 0xFFFFFFFE, 0x80000001u)), int4(4, 17, 1, 0));
            Assert.AreEqual(tzcnt(uint4(0x80000000u, 20u, 4132u, 0xFFFFFFF8u)), int4(31, 2, 2, 3));
        }

        [Test]
        public void tzcnt_long()
        {
            Assert.AreEqual(tzcnt(0L), 64);
            Assert.AreEqual(tzcnt(1L), 0);
            Assert.AreEqual(tzcnt(2L), 1);
            Assert.AreEqual(tzcnt(0x44420000L), 17);
            Assert.AreEqual(tzcnt(0x444200000000L), 33);
            Assert.AreEqual(tzcnt(-9223372036854775808L), 63);
            Assert.AreEqual(tzcnt(-9223372036854775807L), 0);
        }

        [Test]
        public void tzcnt_ulong()
        {
            Assert.AreEqual(tzcnt(0UL), 64);
            Assert.AreEqual(tzcnt(1UL), 0);
            Assert.AreEqual(tzcnt(2UL), 1);
            Assert.AreEqual(tzcnt(0x44420000UL), 17);
            Assert.AreEqual(tzcnt(0x444200000000UL), 33);
            Assert.AreEqual(tzcnt(0x8000000000000000UL), 63);
            Assert.AreEqual(tzcnt(0x8000000000000001UL), 0);
        }

        [Test]
        public void reversebits_int()
        {
            Assert.AreEqual(reversebits(-1872213312), 0x03521609);
            Assert.AreEqual(reversebits(0x1260dafa), 0x5f5b0648);
            Assert.AreEqual(reversebits(-1312858670), 0x4bbafd8d);
            Assert.AreEqual(reversebits(0x74239b12), 0x48d9c42e);
        }

        [Test]
        public void reversebits_int2()
        {
            Assert.AreEqual(reversebits(int2(-1872213312, 0x1260dafa)), int2(0x03521609, 0x5f5b0648));
            Assert.AreEqual(reversebits(int2(-1312858670, 0x74239b12)), int2(0x4bbafd8d, 0x48d9c42e));
        }

        [Test]
        public void reversebits_int3()
        {
            Assert.AreEqual(reversebits(int3(-1872213312, 0x1260dafa, -1312858670)), int3(0x03521609, 0x5f5b0648, 0x4bbafd8d));
            Assert.AreEqual(reversebits(int3(0x74239b12, 0, -1)), int3(0x48d9c42e, 0, -1));
        }

        [Test]
        public void reversebits_int4()
        {
            Assert.AreEqual(reversebits(int4(-1872213312, 0x1260dafa, -1312858670, 0x74239b12)), int4(0x03521609, 0x5f5b0648, 0x4bbafd8d, 0x48d9c42e));
        }

        [Test]
        public void reversebits_uint()
        {
            Assert.AreEqual(reversebits(0x90684ac0u), 0x03521609u);
            Assert.AreEqual(reversebits(0x1260dafau), 0x5f5b0648u);
            Assert.AreEqual(reversebits(0xb1bf5dd2u), 0x4bbafd8du);
            Assert.AreEqual(reversebits(0x74239b12u), 0x48d9c42eu);
        }

        [Test]
        public void reversebits_uint2()
        {
            Assert.AreEqual(reversebits(uint2(0x90684ac0u, 0x1260dafau)), uint2(0x03521609u, 0x5f5b0648u));
            Assert.AreEqual(reversebits(uint2(0xb1bf5dd2u, 0x74239b12u)), uint2(0x4bbafd8du, 0x48d9c42eu));
        }

        [Test]
        public void reversebits_uint3()
        {
            Assert.AreEqual(reversebits(uint3(0x90684ac0u, 0x1260dafau, 0xb1bf5dd2u)), uint3(0x03521609u, 0x5f5b0648u, 0x4bbafd8du));
            Assert.AreEqual(reversebits(uint3(0x74239b12u, 0u, 0xFFFFFFFF)), uint3(0x48d9c42eu, 0u, 0xFFFFFFFF));
        }

        [Test]
        public void reversebits_uint4()
        {
            Assert.AreEqual(reversebits(uint4(0x90684ac0u, 0x1260dafau, 0xb1bf5dd2u, 0x74239b12u)), uint4(0x03521609u, 0x5f5b0648u, 0x4bbafd8du, 0x48d9c42eu));
        }

        [Test]
        public void reversebits_long()
        {
            Assert.AreEqual(reversebits(0x1260dafab1bf5dd2L), 0x4bbafd8d5f5b0648L);
        }

        [Test]
        public void reversebits_ulong()
        {
            Assert.AreEqual(reversebits(0x1260dafab1bf5dd2ul), 0x4bbafd8d5f5b0648ul);
        }
    }
}
