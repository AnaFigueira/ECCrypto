using System.Globalization;
using System.Numerics;

namespace ECCryptoLib.ECC
{
    public static class ECurves
    {
        //public static class SEC2
        //{
            public static class P192r1Curve
            //TODO: Change names to follow the standard. SecP192r1
            //TODO: Include brainpool curves present in the RFC http://tools.ietf.org/html/rfc5639#page-14
            {
                public static readonly BigInteger p = BigInteger.Parse("0FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFF", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger a = BigInteger.Parse("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFC", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger b = BigInteger.Parse("64210519E59C80E70FA7E9AB72243049FEB8DEECC146B9B1", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger G = BigInteger.Parse("004188DA80EB03090F67CBF20EB43A18800F4FF0AFD82FF101207192B95FFC8DA78631011ED6B24CDD573F977A11E794811", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger n = BigInteger.Parse("0FFFFFFFFFFFFFFFFFFFFFFFF99DEF836146BC9B1B4D22831", NumberStyles.AllowHexSpecifier);

                // n * G = infinito
                public static readonly BigInteger h = BigInteger.Parse("01", NumberStyles.AllowHexSpecifier);

                public static readonly int fieldSize = 192;
            }

            public static class P224r1Curve
            {
                public static readonly BigInteger p = BigInteger.Parse("0FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000000000000000000000001", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger a = BigInteger.Parse("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFE", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger b = BigInteger.Parse("B4050A850C04B3ABF54132565044B0B7D7BFD8BA270B39432355FFB4", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger G = BigInteger.Parse("004B70E0CBD6BB4BF7F321390B94A03C1D356C21122343280D6115C1D21BD376388B5F723FB4C22DFE6CD4375A05A07476444D5819985007E34", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger n = BigInteger.Parse("0FFFFFFFFFFFFFFFFFFFFFFFFFFFF16A2E0B8F03E13DD29455C5C2A3D", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger h = BigInteger.Parse("01", NumberStyles.AllowHexSpecifier);
                public static readonly int fieldSize = 224;
            }

            public static class P256r1Curve
            {
                public static readonly BigInteger p = BigInteger.Parse("0FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFF", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger a = BigInteger.Parse("FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFC", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger b = BigInteger.Parse("5AC635D8AA3A93E7B3EBBD55769886BC651D06B0CC53B0F63BCE3C3E27D2604B", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger G = BigInteger.Parse("0046B17D1F2E12C4247F8BCE6E563A440F277037D812DEB33A0F4A13945D898C2964FE342E2FE1A7F9B8EE7EB4A7C0F9E162BCE33576B315ECECBB6406837BF51F5", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger n = BigInteger.Parse("0FFFFFFFF00000000FFFFFFFFFFFFFFFFBCE6FAADA7179E84F3B9CAC2FC632551", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger h = BigInteger.Parse("01", NumberStyles.AllowHexSpecifier);
                public static readonly int fieldSize = 256;
            }

            public static class P384r1Curve
            {
                public static readonly BigInteger p = BigInteger.Parse("0FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFF", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger a = BigInteger.Parse("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFC", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger b = BigInteger.Parse("0B3312FA7E23EE7E4988E056BE3F82D19181D9C6EFE8141120314088F5013875AC656398D8A2ED19D2A85C8EDD3EC2AEF", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger G = BigInteger.Parse("04AA87CA22BE8B05378EB1C71EF320AD746E1D3B628BA79B9859F741E082542A385502F25DBF55296C3A545E3872760AB73617DE4A96262C6F5D9E98BF9292DC29F8F41DBD289A147CE9DA3113B5F0B8C00A60B1CE1D7E819D7A431D7C90EA0E5F", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger n = BigInteger.Parse("0FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC7634D81F4372DDF581A0DB248B0A77AECEC196ACCC52973", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger h = BigInteger.Parse("01", NumberStyles.AllowHexSpecifier);
                public static readonly int fieldSize = 384;
            }

            public static class P521r1Curve
            {
                public static readonly BigInteger p = BigInteger.Parse("001FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger a = BigInteger.Parse("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger b = BigInteger.Parse("0051953EB9618E1C9A1F929A21A0B68540EEA2DA725B99B315F3B8B489918EF109E156193951EC7E937B1652C0BD3BB1BF073573DF883D2C34F1EF451FD46B503F00", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger G = BigInteger.Parse("00400C6858E06B70404E9CD9E3ECB662395B4429C648139053FB521F828AF606B4D3DBAA14B5E77EFE75928FE1DC127A2FFA8DE3348B3C1856A429BF97E7E31C2E5BD66011839296A789A3BC0045C8A5FB42C7D1BD998F54449579B446817AFBD17273E662C97EE72995EF42640C550B9013FAD0761353C7086A272C24088BE94769FD16650", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger n = BigInteger.Parse("001FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFA51868783BF2F966B7FCC0148F709A5D03BB5C9B8899C47AEBB6FB71E91386409", NumberStyles.AllowHexSpecifier);
                public static readonly BigInteger h = BigInteger.Parse("01", NumberStyles.AllowHexSpecifier);
                public static readonly int fieldSize = 521;
            }
        //}

        //public static class Brainpool
        //{
        //    // TODO: To implement in the future
        //}

        //public static class BADA55
        //{
        //    // TODO: To implement in the future
        //}
    }
}