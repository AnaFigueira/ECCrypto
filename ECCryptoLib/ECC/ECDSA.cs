using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;

namespace ECCryptoLib.ECC
{
    public static class ECDSA
    {
        public static Signature GenerateSignature(ECAsymmetricKeyPair keyPair, byte[] hash)
        {
            BigInteger k;

            for (int i = 0; i < 100; i++)
            {
                int size = keyPair.Curve.FieldSize / 8;
                byte[] kBytes = new byte[size];
                int seed = (int)CryptographicBuffer.GenerateRandomNumber();
                Random rand = new Random(seed);
                rand.NextBytes(kBytes);
                k = new BigInteger(kBytes);
                kBytes[size - 1] = 0;
                //k = new BigInteger(kBytes);
                //for (int j = 0; j < 8; j++)
                //{
                //    //ECPoint.GetBit(curve.n,j)
                //    if (ECPoint.GetBit(keyPair.Curve.n, j) == 0)
                //        kBytes[0] = (byte)(kBytes[0] ^ (1 << (7 - j)));
                //    else
                //        break;
                //}
                
                var z = new BigInteger(hash);

                if (k <= 0 || k >= keyPair.Curve.n)
                    continue;

                var r = ECPoint.MultiplyPointByScalar(k, keyPair.Curve.G).X % keyPair.Curve.n;

                if (r == 0)
                    continue;

                var ss = (z + r * keyPair.PrivateKey);
                var s = (ss * (BigIntegerExtensions.ModInverse(k, keyPair.Curve.n))) % keyPair.Curve.n;

                if (s <= 0)
                    continue;

                Debug.WriteLine("r:" + r.ToString());
                Debug.WriteLine("s:" + s.ToString());

                var w = BigIntegerExtensions.ModInverse(s, keyPair.Curve.n);
                var u1 = (z * w) % keyPair.Curve.n;
                var u2 = (r * w) % keyPair.Curve.n;
                var pt = ECPoint.MultiplyPointByScalar(u1, keyPair.Curve.G) + ECPoint.MultiplyPointByScalar(u2, keyPair.PublicKey);
                BigInteger pmod = pt.X % keyPair.Curve.n;
                if(pmod == r)
                {

                }

                return new Signature(r, s);
            }

            // Could not generate a signature
            // Could not generate a number
            return null;
        }

        public static bool VerifySignature(ECAsymmetricKeyPair keyPair, byte[] hash, Signature signature)
        {
            BigInteger r = signature.R;
            BigInteger s = signature.S;

            if (r >= keyPair.Curve.n || r == 0 || s >= keyPair.Curve.n || s == 0)
                return false;

            var z = new BigInteger(hash);
            var w = BigIntegerExtensions.ModInverse(s, keyPair.Curve.n);
            Debug.WriteLine(s.ToString());
            var u1 = (z * w) % keyPair.Curve.n;
            var u2 = (r * w) % keyPair.Curve.n;
            var pt = ECPoint.MultiplyPointByScalar(u1, keyPair.Curve.G) + ECPoint.MultiplyPointByScalar(u2, keyPair.PublicKey);
            BigInteger pmod = pt.X % keyPair.Curve.n;

            var teste = (w * s) % keyPair.Curve.n;
            if(teste == r)
            {

            }
            return pmod == r;
        }
    }
}
