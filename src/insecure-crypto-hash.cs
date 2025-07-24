using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

class Crypto
{
    public void Hash()
    {
        var md5 = new MD5CryptoServiceProvider();
        // ruleid: insecure-crypto-hash
        var hashValue = md5.ComputeHash(new byte[] { });

        var hmacmd5 = new HMACMD5();
        // ruleid: insecure-crypto-hash
        hashValue = hmacmd5.ComputeHash(new byte[] { });

        var sha1 = new SHA1CryptoServiceProvider();
        // ruleid: insecure-crypto-hash
        hashValue = sha1.ComputeHash(new byte[] { });

        var sha1Managed = new SHA1Managed();
        // ruleid: insecure-crypto-hash
        hashValue = sha1Managed.ComputeHash(new byte[] { });

        var sha2 = new SHA256CryptoServiceProvider();
        // ruleid: insecure-crypto-hash
        hashValue = sha2.ComputeHash(new byte[] { });

        var sha2Managed = new SHA256Managed();
        // ruleid: insecure-crypto-hash
        hashValue = sha2Managed.ComputeHash(new byte[] { });


        var sha2Managedsecond = new SHA256Managed();
        // ruleid: insecure-crypto-hash
        hashValue = sha2Managed.ComputeHash(new byte[] { });

        var sha3Managedsecond = new SHA256Managed();
        // ruleid: insecure-crypto-hash
        hashValue = sha2Managed.ComputeHash(new byte[] { });
    }
}
