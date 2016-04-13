using System;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace ECCryptoLib
{
    public class HashFunctions
    {
        /// <summary>
        /// Calculates the SHA1 of a string
        /// </summary>
        /// <param name="text">String</param>
        /// <returns>Byte array with the hash</returns>
        public static string Sha1(string text)
        {
            HashAlgorithmProvider hashProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha1);
            IBuffer hash = hashProvider.HashData(CryptographicBuffer.ConvertStringToBinary(text, BinaryStringEncoding.Utf8));
            string hashValue = CryptographicBuffer.EncodeToHexString(hash);
            return hashValue;
        }

        /// <summary>
        /// Calculates the SHA256 of a string
        /// </summary>
        /// <param name="text">String</param>
        /// <returns>Byte array with the hash</returns>
        public static string Sha256(string text)
        {
            HashAlgorithmProvider hashProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            IBuffer hash = hashProvider.HashData(CryptographicBuffer.ConvertStringToBinary(text, BinaryStringEncoding.Utf8));
            string hashValue = CryptographicBuffer.EncodeToHexString(hash);
            return hashValue;
        }

        /// <summary>
        /// Calculates the SHA384 of a string
        /// </summary>
        /// <param name="text">String</param>
        /// <returns>Byte array with the hash</returns>
        public static string Sha384(string text)
        {
            HashAlgorithmProvider hashProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha384);
            IBuffer hash = hashProvider.HashData(CryptographicBuffer.ConvertStringToBinary(text, BinaryStringEncoding.Utf8));
            string hashValue = CryptographicBuffer.EncodeToHexString(hash);
            return hashValue;
        }

        /// <summary>
        /// Calculates the SHA512 of a string
        /// </summary>
        /// <param name="text">String</param>
        /// <returns>Byte array with the hash</returns>
        public static string Sha512(string text)
        {
            HashAlgorithmProvider hashProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha512);
            IBuffer hash = hashProvider.HashData(CryptographicBuffer.ConvertStringToBinary(text, BinaryStringEncoding.Utf8));
            string hashValue = CryptographicBuffer.EncodeToHexString(hash);
            return hashValue;
        }

        /// <summary>
        /// Calculates the Md5 of a string
        /// </summary>
        /// <param name="text">String</param>
        /// <returns>Byte array with the hash</returns>
        public static string Md5(string text)
        {
            HashAlgorithmProvider hashProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer hash = hashProvider.HashData(CryptographicBuffer.ConvertStringToBinary(text, BinaryStringEncoding.Utf8));
            string hashValue = CryptographicBuffer.EncodeToHexString(hash);
            return hashValue;
        }

        public static string HmacSha1(byte[] keyBytes, string message)
        {
            MacAlgorithmProvider objMacProv = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1);

            BinaryStringEncoding encoding = BinaryStringEncoding.Utf8;
            IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(message, encoding);
            
            IBuffer buffKeyMaterial = CryptographicBuffer.GenerateRandom(objMacProv.MacLength);
            CryptographicKey hmacKey = objMacProv.CreateKey(buffKeyMaterial);

            IBuffer buffHMAC = CryptographicEngine.Sign(hmacKey, buffMsg);

            // Verify that the HMAC length is correct for the selected algorithm
            if (buffHMAC.Length != objMacProv.MacLength)
            {
                throw new Exception("Error computing digest");
            }

            return buffHMAC.ToString();
        }
    }
}

