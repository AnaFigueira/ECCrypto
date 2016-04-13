using ECCryptoLib.ECC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ECCryptoLib
{
    public class EncryptedMessage
    {
        public EncryptedMessage(ECPoint y, string encrypted, string mac, string myPublicKeyX)
        {
            this.Y = y;
            this.Message = encrypted;
            this.MAC = mac;
            this.PublicKeyX = myPublicKeyX;
        }

        public EncryptedMessage()
        {
        }

        public ECPoint Y { get; set; }

        public string Message { get; set; }

        public string MAC { get; set; }

        public string PublicKeyX { get; set; }

        public string MessageToString()
        {
            return "$" + Y.X + "#" + Y.X + "#" + Y.Curve.FieldSize + "#" + Message + "#" + MAC + "#" + PublicKeyX + "$";
        }

  
        public EncryptedMessage StringToMessage(string message)
        {
            if (message.StartsWith("$") && message.EndsWith("$"))
            {
                message = message.Remove(0, 1);
                message = message.Remove(message.Count() - 2, 1);
                string[] split = message.Split('#');

                switch (split[2])
                {
                    case "192":
                        this.Y = new ECPoint(new ECurve(Curves.P192r1Curve), new BigInteger(Encoding.UTF8.GetBytes(split[0])), new BigInteger(Encoding.UTF8.GetBytes(split[1])));
                        break;

                    case "224":
                        this.Y = new ECPoint(new ECurve(Curves.P224r1Curve), new BigInteger(Encoding.UTF8.GetBytes(split[0])), new BigInteger(Encoding.UTF8.GetBytes(split[1])));
                        break;

                    case "256":
                        this.Y = new ECPoint(new ECurve(Curves.P256r1Curve), new BigInteger(Encoding.UTF8.GetBytes(split[0])), new BigInteger(Encoding.UTF8.GetBytes(split[1])));
                        break;

                    case "384":
                        this.Y = new ECPoint(new ECurve(Curves.P384r1Curve), new BigInteger(Encoding.UTF8.GetBytes(split[0])), new BigInteger(Encoding.UTF8.GetBytes(split[1])));
                        break;

                    case "521":
                        this.Y = new ECPoint( new ECurve(Curves.P521r1Curve), new BigInteger(Encoding.UTF8.GetBytes(split[0])), new BigInteger(Encoding.UTF8.GetBytes(split[1])));
                        break;
                }

                this.Message = split[3];
                this.MAC = split[4];
                this.PublicKeyX = split[5];

                return this;
            }

            return null;
        }
    }
}
