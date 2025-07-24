using System;
using System.Security.Cryptography;

public class Encryption
{
  public void ProvidersAndFieldAssignments() {
    // ruleid: ofb-mode
    AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider
    {
        Mode = CipherMode.OFB
    };

    AesCryptoServiceProvider aesCryptoServiceProvider2 = new AesCryptoServiceProvider();
    // ruleid: ofb-mode
    aesCryptoServiceProvider2.Mode = CipherMode.OFB;

    // ruleid: ofb-mode
    AesManaged aesManaged = new AesManaged
    {
        Mode = CipherMode.OFB
    };

    AesManaged aesManaged2 = new AesManaged();
    // ruleid: ofb-mode
    aesManaged2.Mode = CipherMode.OFB;

    // ruleid: ofb-mode
    RijndaelManaged rm = new RijndaelManaged
    {
        Mode = CipherMode.OFB
    };

    RijndaelManaged rm2 = new RijndaelManaged();
    // ruleid: ofb-mode
    rm2.Mode = CipherMode.OFB;

    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
    // ruleid: ofb-mode
    tdes.Mode = CipherMode.OFB;

    // ruleid: ofb-mode
    TripleDESCryptoServiceProvider tdes2 = new TripleDESCryptoServiceProvider
    {
        Mode = CipherMode.OFB
    };
  }
}
