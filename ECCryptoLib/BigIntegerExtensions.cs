using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ECCryptoLib
{
    public static class BigIntegerExtensions
    {
        public static BigInteger ModInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n;
            BigInteger v = 0;
            BigInteger d = 1;
            while (a > 0)
            {
                BigInteger t = i / a;
                BigInteger x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - (t * x);
                v = x;
            }
            v %= n;
            if (v < 0)
                v = (v + n) % n;

            return v;
        }
    }
}
