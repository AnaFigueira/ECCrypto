using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ECCryptoLib.ECC
{
    public class ECPoint
    {
        internal ECurve _curve;
        internal BigInteger _x;
        internal BigInteger _y;

        public ECurve Curve
        {
            get { return _curve; }
        }

        public BigInteger X
        {
            get { return _x; }
            set
            {
                if(_x != value)
                {
                    _x = value;
                }
            }
        }

        public BigInteger Y
        {
            get { return _y; }
            set
            {
                if (_y != value)
                {
                    _y = value;
                }
            }
        }
        public ECPoint()
        {

        }

        public ECPoint(ECurve curve, BigInteger x, BigInteger y)
        {
            if (curve == null)
                throw new ArgumentNullException("ECurve");

            this._curve = curve;
            this._x = x;
            this._y = y;
        }

        public bool IsInfinity
        {
            get { return _x == null && _y == null; }
        }

        public static ECPoint operator +(ECPoint p1, ECPoint p2)
        {
            if (p1.IsInfinity)
                return p2;

            if (p2.IsInfinity)
                return p1;

            if (p1.X == p2.X) // a == b
            {
                if (p1.Y == p2.Y)
                {
                    // a == b so the point must be doubled
                    return p1.DoublePoint();
                }

                // else a == -b
                return p1.Curve.Infinity;
            }

            BigInteger s = ((p2.Y - p1.Y) * BigIntegerExtensions.ModInverse((p2.X - p1.X + p1.Curve.P),(p1._curve.P))) % p1._curve.P;
            //BigInteger x3 = BigInteger.Remainder((s * s) - p1.X - p2.X, p1._curve.P);
            BigInteger x3 = ((s * s) - p1.X - p2.X) % p1._curve.P;
            //if (x3 < 0)
            //{
            //    x3 = x3 + p1._curve.P;
            //    //Debug.WriteLine(x3.ToString());
            //}
            BigInteger y3 = ((s * (p1.X - x3)) - p1.Y) % p1._curve.P;
            if (y3 < 0)
                y3 = y3 + p1._curve.P;

            return new ECPoint(p1._curve, x3, y3);
        }

        public ECPoint DoublePoint()
        {
            
            BigInteger s = (((3 * (this.X * this.X)) + _curve.a) * (BigIntegerExtensions.ModInverse((2 * this.Y),(this._curve.P)))) % _curve.P;
            BigInteger x3 = ((s * s) - this.X - this.X) % _curve.P;
            //BigInteger x3 = BigInteger.Remainder((s * s) - this.X - this.X, _curve.P);
            //if (x3 < 0)
            //{
            //    x3 = x3 + _curve.P;
            //    //Debug.WriteLine(x3.ToString());
            //}
            BigInteger y3 = ((s * (this.X - x3)) - this.Y) % _curve.P;
            if (y3 < 0)
                y3 = y3 + this._curve.P;

            return new ECPoint(this._curve, x3, y3);
        }

        public static ECPoint MultiplyPointByScalar(BigInteger n, ECPoint point)
        {
            // Double and Add
            int numberOfBits = GetNumberOfBits(n);
            ECPoint result = point;

            //for (int i = numberOfBits - 1; i > 0; i--)
            for (int i = 1; i < numberOfBits; i++)
            {
                result = result.DoublePoint();

                int bit = GetBit(n, i);
                if (bit == 1)
                    result = result + point;
            }

            return result;
        }


        public static int GetBit(BigInteger n, int pos)
        {
            return GetBitArray(n)[pos];
        }

        /// <summary>
        /// Function that returns an array with the bits of 
        /// the BigInteger in the little endian format
        /// </summary>
        /// <returns>Array with the bits of the BigInteger</returns>
        public static int[] GetBitArray(BigInteger n)
        {
            List<BitArray> bitArray = new List<BitArray>();
            byte[] bytes = n.ToByteArray();
            Array.Reverse(bytes); // Convert to little endian

            int numberOfBits = GetNumberOfBits(n);

            BitArray bits = new BitArray(bytes);

            if (bits.Length == 0) // Number zero
                return new int[] { 0 };

            int[] bitsArray = new int[numberOfBits];

            for (int i = 0; i < numberOfBits; i++)
            {
                bitsArray[i] = bits[numberOfBits - i - 1] ? 1 : 0;
            }

            return bitsArray;
        }

        // TODO: To test
        private static int GetNumberOfBits(BigInteger n)
        {
            List<BitArray> bitArray = new List<BitArray>();
            byte[] bytes = n.ToByteArray();

            Array.Reverse(bytes);

            BitArray bits = new BitArray(bytes);
            bool _inNumber = false;
            int initialPosition = -1;

            for (int i = bits.Length - 1; i >= 0; i--)
            {
                if (!_inNumber && bits[i])
                {
                    _inNumber = true;
                    initialPosition = i;
                    break;
                }
            }

            if (!_inNumber) // If no 1 was found, then it's a zero
                return 1;

            return initialPosition + 1;
        }


        public static bool operator ==(ECPoint point1, ECPoint point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(ECPoint point1, ECPoint point2)
        {
            return !(point1.Equals(point2));
        }

        public override bool Equals(object o)
        {
            if (o == null)
                return false;

            ECPoint point = (ECPoint)o;

            if (this.X == point.X && this.Y == point.Y && this.Curve == point.Curve)
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

        /// <summary>
        /// Converts a string with the representation of a ECPoint to a ECPoint.
        /// String format:
        /// CurveType + ";" + xCoordinate + ";" + yCoordinate
        /// </summary>
        /// <param name="strPoint">String representation of the ECPoint</param>
        /// <returns>ECPoint</returns>
        public static ECPoint ConvertStringToECPoint(string strPoint)
        {
            string[] parse = strPoint.Split(';');
            if (parse.Length != 3)
            {
                return null;
            }
            else
            {
                string strCurve = parse[0];
                strCurve = Base64.DecodeString(strCurve);

                string strX = parse[1];
                strX = Base64.DecodeString(strX);

                string strY = parse[2];
                strY = Base64.DecodeString(strY);

                BigInteger X = BigInteger.Parse(strX, NumberStyles.AllowHexSpecifier);
                BigInteger Y = BigInteger.Parse(strY, NumberStyles.AllowHexSpecifier);

                ECurve curve = null;
                switch (strCurve)
                {
                    case "192":
                        curve = new ECurve(Curves.P192r1Curve);
                        break;

                    case "224":
                        curve = new ECurve(Curves.P224r1Curve);
                        break;

                    case "256":
                        curve = new ECurve(Curves.P256r1Curve);
                        break;

                    case "384":
                        curve = new ECurve(Curves.P384r1Curve);
                        break;

                    case "521":
                        curve = new ECurve(Curves.P521r1Curve);
                        break;

                    default:
                        return null;
                }

                return new ECPoint(curve, X, Y);
            }
        }

        


        public static string ConvertECPointToString(ECPoint point)
        {
            if (point == null)
            {
                return string.Empty;
            }
            else
            {
                // Encode to Base64
                string strX = point.X.ToString("X");
                strX = Base64.EncodeString(strX);

                string strY = point.Y.ToString("X");
                strY = Base64.EncodeString(strY);

                string strCurve = Base64.EncodeString(point.Curve.FieldSize.ToString());

                return strCurve + ";" + strX + ";" + strY;
            }
        }

       
    }
}
