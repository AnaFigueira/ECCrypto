using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECCryptoLib
{
    public class Base64
    {
        #region String

        /// <summary>
        /// Encodes a string to Base64
        /// </summary>
        /// <param name="text">String to be encoded</param>
        /// <returns>Encoded string</returns>
        public static string EncodeString(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Decodes a string from Base64
        /// </summary>
        /// <param name="encodedText">Base64 encoded string</param>
        /// <returns>Decoded string</returns>
        public static string DecodeString(string encodedText)
        {
            byte[] bytes = Convert.FromBase64String(encodedText);
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        #endregion String

        //#region File

        ///// <summary>
        ///// Encodes a Text File to Base64
        ///// </summary>
        ///// <param name="inputFile">File to be encoded to Base64</param>
        ///// <param name="outputFile">Encoded File</param>
        ///// <returns>True if file was encoded successfully</returns>
        //public static bool EncodeTextFile(string inputFile, string outputFile)
        //{
        //    try
        //    {
        //        using (StreamReader streamIn = new StreamReader(inputFile))
        //        {
        //            // Overwrites file if already exists
        //            using (StreamWriter streamOut = new StreamWriter(outputFile, false))
        //            {
        //                string line;
        //                while ((line = streamIn.ReadLine()) != null)
        //                {
        //                    streamOut.WriteLine(EncodeString(line));
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Decodes a Text File from Base64
        ///// </summary>
        ///// <param name="inputFile">File to be decoded</param>
        ///// <param name="outputFile">Decoded File</param>
        ///// <returns>True if file was decoded successfully</returns>
        //public static bool DecodeTextFile(string inputFile, string outputFile)
        //{
        //    try
        //    {
        //        using (StreamReader streamIn = new StreamReader(inputFile))
        //        {
        //            // Overwrites file if already exists
        //            using (StreamWriter streamOut = new StreamWriter(outputFile, false))
        //            {
        //                string line;
        //                while ((line = streamIn.ReadLine()) != null)
        //                {
        //                    streamOut.WriteLine(DecodeString(line));
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //#endregion File
    }
}
