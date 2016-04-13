using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace ECCryptoLib.ECC
{
    public static class ECIES
    {

        public static string Encrypt(string message, ECAsymmetricKeyPair keypair, string myPublicKeyX)
        {
            BigInteger db = new BigInteger();
            do
            {
                db = GenerateFromRandomBits(keypair.Curve.FieldSize);
            } while (db < 1 && db >= keypair.Curve.n);

            ECPoint Y = ECPoint.MultiplyPointByScalar(db, keypair.Curve.G);
            ECPoint K = ECPoint.MultiplyPointByScalar(db, keypair.PublicKey);
            BigInteger x3 = K.X;  // Secret

            byte[] salt = GenerateRandomBytes(521);
            byte [] k1;
            byte[] k2;

            k1 = PBKDF2.DeriveKey(x3.ToString(), salt);
            k2 = PBKDF2.DeriveKey(x3.ToString(), salt);

            string encrypted = AES.AES_Encrypt(message, k1);
            string mac = HashFunctions.HmacSha1(k2, encrypted);

            EncryptedMessage encMessage = new EncryptedMessage(Y, encrypted, mac, myPublicKeyX);

            string result = encMessage.MessageToString();

            Debug.WriteLine(result);

            return result;
        }

        public static string Decrypt(EncryptedMessage msg, ECAsymmetricKeyPair myKey)
        {
            
            BigInteger db = new BigInteger();
            do
            {
                db = GenerateFromRandomBits(msg.Y._curve.FieldSize);
            } while (db < 1 && db >= msg.Y._curve.n);

            ECPoint K = ECPoint.MultiplyPointByScalar(myKey.PrivateKey, msg.Y);
            BigInteger x3 = K.X;  // Secret

            byte[] salt = GenerateRandomBytes(521);
            byte[] k1;
            byte[] k2;


            k1 = PBKDF2.DeriveKey(x3.ToString(), salt);
            k2 = PBKDF2.DeriveKey(x3.ToString(), salt);

            string decrypted = AES.AES_Decrypt(msg.Message, k1);
            string mac = HashFunctions.HmacSha1(k2, decrypted);

            if(mac != msg.MAC)
            {

            }
            else
            {

            }


            return decrypted;
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

        private static byte[] GenerateRandomBytes(int nBits)
        {
            // TODO: Check this
            Random rand = new Random();
            int seed = (int)CryptographicBuffer.GenerateRandomNumber();
            byte[] bytes = new byte[nBits / 8];
            rand.NextBytes(bytes);
            return bytes;
        }



        

    }
}
