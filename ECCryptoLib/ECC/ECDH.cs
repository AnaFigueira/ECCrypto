using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ECCryptoLib.ECC
{
    public class ECDH
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="privateKeyA">Private Key</param>
        /// <param name="publicKeyB">Public Key</param>
        /// <returns></returns>
        public static string ECDHAgreement(BigInteger privateKeyA, ECPoint publicKeyB)
        {
            ECPoint k = ECPoint.MultiplyPointByScalar(privateKeyA, publicKeyB);

            string xHex = k.X.ToString();
            string yHex = k.Y.ToString();

            // Calculate SHA256 of key
            return HashFunctions.Sha256(xHex);
        }
    }
}
