using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ECCryptoLib.ECC
{
    public static class Helper
    {
        public static ECPoint DecodePoint(BigInteger G, int fieldSize)
        {
            // TODO: Test Decoder
            ECPoint point = null;
            int expectedLength = (int)Math.Ceiling((double)fieldSize / 8);

            byte[] encoded = G.ToByteArray();
            byte[] encodedX = new byte[expectedLength + 1];
            byte[] encodedY = new byte[expectedLength + 1];

            if (encoded.Length != (2 * expectedLength + 1))
                throw new ArgumentException("Incorrect length for uncompressed encoded point");

            for (int i = 1; i < expectedLength + 1; i++)
            {
                encodedX[i] = encoded[i];
            }
            int j = 0;
            for (int i = expectedLength + 1; i < 2 * expectedLength + 1; i++)
            {
                encodedY[j] = encoded[i];
                j++;
            }

            // TODO: Check this

            //BigInteger x1 = BigInteger.Parse(encodedX, expectedLength + 1);
            //BigInteger y1 = BigInteger.Parse(encodedY, expectedLength);

            BigInteger x1 = new BigInteger(encodedX);
            BigInteger y1 = new BigInteger(encodedY);

            switch (fieldSize)
            {
                case 192:
                    point = new ECPoint(new ECurve(Curves.P192r1Curve), x1, y1);
                    break;


                case 224:
                    point = new ECPoint(new ECurve(Curves.P224r1Curve), x1, y1);
                    break;


                case 256:
                    point = new ECPoint(new ECurve(Curves.P256r1Curve), x1, y1);
                    break;

                case 384:
                    point = new ECPoint(new ECurve(Curves.P384r1Curve), x1, y1);
                    break;

                case 521:
                    point = new ECPoint(new ECurve(Curves.P521r1Curve), x1, y1);
                    break;

                default:
                    point = new ECPoint(new ECurve(Curves.P521r1Curve), x1, y1);
                    break;
            }

            return point;
        }

        public static BigInteger EncodePoint(ECPoint G, int fieldSize)
        {
            // TODO: Test Decoder
            ECPoint point = null;
            BigInteger encodedPoint = 0;
            int expectedLength = (int)Math.Ceiling((double)fieldSize / 8);

            byte[] encoded = null;
            byte[] encodedX = new byte[expectedLength + 1];
            byte[] encodedY = new byte[expectedLength + 1];

            encodedX = G.X.ToByteArray();
            encodedY = G.Y.ToByteArray();

            for (int i = 1; i < expectedLength + 1; i++)
            {
                encoded[i] = encodedX[i];
            }
            int j = 0;
            for (int i = expectedLength + 1; i < 2 * expectedLength + 1; i++)
            {
                encoded[j] = encodedY[i];
                j++;
            }

            encodedPoint = new BigInteger(encoded);
            return encodedPoint;
        }
    }
}
