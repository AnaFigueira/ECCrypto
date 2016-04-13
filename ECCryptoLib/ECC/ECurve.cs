using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ECCryptoLib.ECC;

namespace ECCryptoLib.ECC
{
    public enum Curves { P192r1Curve, P224r1Curve, P256r1Curve, P384r1Curve, P521r1Curve }

    public class ECurve
    {
        private Curves _curveType;

        private BigInteger _p; // prime
        private BigInteger _a; // a
        private BigInteger _b; // b
        private ECPoint _G; // 
        private BigInteger _n; // n
        private BigInteger _h; // h
        private int _fieldSize; // Field Size

        public ECPoint Infinity; // x = null, y = null

        public Curves CurveType { get { return _curveType; } }

        public BigInteger P { get { return _p; } }

        public BigInteger a { get { return _a; } }

        public BigInteger b { get { return _b; } }

        public ECPoint G { get { return _G; } }

        public BigInteger n { get { return _n; } }

        public BigInteger h { get { return _h; } }

        public int FieldSize { get { return _fieldSize; } }

        public ECurve(Curves curveType)
        {
            switch (curveType)
            {
                case Curves.P192r1Curve:
                    Populate(Curves.P192r1Curve, ECurves.P192r1Curve.p, ECurves.P192r1Curve.a, ECurves.P192r1Curve.b, ECurves.P192r1Curve.G, ECurves.P192r1Curve.n, ECurves.P192r1Curve.h, ECurves.P192r1Curve.fieldSize);
                    break;

                case Curves.P224r1Curve:
                    Populate(Curves.P224r1Curve, ECurves.P224r1Curve.p, ECurves.P224r1Curve.a, ECurves.P224r1Curve.b, ECurves.P224r1Curve.G, ECurves.P224r1Curve.n, ECurves.P224r1Curve.h, ECurves.P224r1Curve.fieldSize);
                    break;

                case Curves.P256r1Curve:
                    Populate(Curves.P256r1Curve, ECurves.P256r1Curve.p, ECurves.P256r1Curve.a, ECurves.P256r1Curve.b, ECurves.P256r1Curve.G, ECurves.P256r1Curve.n, ECurves.P256r1Curve.h, ECurves.P256r1Curve.fieldSize);
                    break;

                case Curves.P384r1Curve:
                    Populate(Curves.P384r1Curve, ECurves.P384r1Curve.p, ECurves.P384r1Curve.a, ECurves.P384r1Curve.b, ECurves.P384r1Curve.G, ECurves.P384r1Curve.n, ECurves.P384r1Curve.h, ECurves.P384r1Curve.fieldSize);
                    break;

                case Curves.P521r1Curve:
                    Populate(Curves.P521r1Curve, ECurves.P521r1Curve.p, ECurves.P521r1Curve.a, ECurves.P521r1Curve.b, ECurves.P521r1Curve.G, ECurves.P521r1Curve.n, ECurves.P521r1Curve.h, ECurves.P521r1Curve.fieldSize);
                    break;
            }
        }

        private void Populate(Curves curveType, BigInteger p, BigInteger a, BigInteger b, BigInteger G, BigInteger n, BigInteger h, int fieldSize)
        {
            this._curveType = curveType;
            this._p = p;
            this._a = a;
            this._b = b;
            this._G = DecodePoint(G, fieldSize);
            this._n = n;
            this._h = h;
            this._fieldSize = fieldSize;
            // TODO: Fix this
            //this.Infinity = new ECPoint(this, null, null);
        }

        private ECPoint DecodePoint(BigInteger G, int fieldSize)
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

            point = new ECPoint(this, x1, y1);

            return point;
        }

        public static bool operator ==(ECurve curve1, ECurve curve2)
        {
            return curve1.Equals(curve2);
        }

        public static bool operator !=(ECurve curve1, ECurve curve2)
        {
            return !(curve1.Equals(curve2));
        }

        public override bool Equals(object o)
        {
            if (o == null)
                return false;

            ECurve curve = (ECurve)o;

            if (this._curveType == curve._curveType)
                return true;

            return false;
        }

        /// <summary>
        /// Gets Hash Code for BigInteger
        /// </summary>
        /// <returns>Returns the hash code for this BigInteger.</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
