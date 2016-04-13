using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;

namespace ECCryptoLib.ECC
{
    public class ECAsymmetricKeyPair
    {
        private ECurve _curve;
        private BigInteger _privateKey;
        private ECPoint _publicKey;

        public ECurve Curve
        {
            get { return _curve; }
            set { this._curve = value; }
        }

        public BigInteger PrivateKey
        {
            get { return _privateKey; }
            set { this._privateKey = value; }
        }

        public ECPoint PublicKey
        {
            get { return _publicKey; }
            set { this._publicKey = value; }
        }

        public ECAsymmetricKeyPair()
        {
        }

        public ECAsymmetricKeyPair(string publicKeyX, string publicKeyY, string privateKey, string curve)
        {
            // TODO: Complete member initialization
            this.PublicKey.X = new BigInteger(Encoding.UTF8.GetBytes(publicKeyX));
            this.PublicKey.Y = new BigInteger(Encoding.UTF8.GetBytes(publicKeyY));
            switch (curve)
            {
                case "192":
                    this.Curve = new ECurve(Curves.P192r1Curve);
                    break;

                case "224":
                    this.Curve = new ECurve(Curves.P224r1Curve);
                    break;

                case "256":
                    this.Curve = new ECurve(Curves.P256r1Curve);
                    break;

                case "384":
                    this.Curve = new ECurve(Curves.P384r1Curve);
                    break;

                case "521":
                    this.Curve = new ECurve(Curves.P521r1Curve);
                    break;

                default:
                    this.Curve = new ECurve(Curves.P521r1Curve);
                    break;
            }
        }

        /// <summary>
        /// Generates a new ECAsymmetric Key Pair
        /// </summary>
        /// <returns>Returns true if generated successfully</returns>
        public bool GenerateKeyPair(ECurve curve)
        {
            _curve = curve;
            GeneratePrivateKey(curve);
            GeneratePublicKey(curve);
            return true;
        }

        /// <summary>
        /// Generates a Private Key
        /// </summary>
        /// <param name="curve">Elliptic Curve used to generate the private key</param>
        /// <returns>Generated ECPrivateKey</returns>
        private void GeneratePrivateKey(ECurve curve)
        {
            BigInteger privateKey = new BigInteger();
            do
            {
                privateKey = GenerateFromRandomBits(curve.FieldSize);
            } while (privateKey < 2 && privateKey >= curve.n);

            PrivateKey = privateKey; ;
        }

        private static BigInteger GenerateFromRandomBits(int nBits)
        {
            // TODO: Check this
            Random rand = new Random();
            int seed = (int)CryptographicBuffer.GenerateRandomNumber();
            byte[] bytes = new byte[nBits / 8];
            rand.NextBytes(bytes);
            return new BigInteger(bytes);
        }

        /// <summary>
        /// Generates a Public Key
        /// </summary>
        /// <param name="curve">Elliptic Curve used to generate the public key</param>
        /// <returns>Generated ECPublicKey</returns>
        private void GeneratePublicKey(ECurve curve)
        {
            PublicKey = ECPoint.MultiplyPointByScalar(PrivateKey, curve.G);
        }

        public string GetEncryptionKey(ECPoint contactPubKey)
        {
            ECPoint k = ECPoint.MultiplyPointByScalar(this._privateKey, contactPubKey);

            string xHex = k.X.ToString();
            string yHex = k.Y.ToString();

            // Calculate SHA256 of key
            return HashFunctions.Sha256(xHex + yHex); // TODO: Check if its in hex
        }
    }
}
