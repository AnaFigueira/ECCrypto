using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ECCryptoLib
{
    public class Signature
    {
        private BigInteger _r;

        public BigInteger R
        {
            get { return _r; }
            set { _r = value; }
        }
        private BigInteger _s;

        public BigInteger S
        {
            get { return _s; }
            set { _s = value; }
        }

        public Signature(BigInteger r, BigInteger s)
        {
            this._r = r;
            this._s = s;
        }
        public Signature(string r, string s)
        {
            this._r = new BigInteger(Encoding.UTF8.GetBytes(r));
            this._s = new BigInteger(Encoding.UTF8.GetBytes(s));
        }
    }
}
