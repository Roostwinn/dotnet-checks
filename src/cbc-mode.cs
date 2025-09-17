using System;
using System.Security.Cryptography;

public class Encryption
{
	public void EncryptWithAesCbc() {
		Aes key = Aes.Create();
		// ruleid: cbc-mode
		key.Mode = CipherMode.CBC;
		using var encryptor = key.CreateEncryptor();
		byte[] msg = new byte[32];
		var cipherText = encryptor.TransformFinalBlock(msg, 0, msg.Length);
	}

	public void EncryptWithAesCbc2() {
		Aes key = Aes.Create();
		byte[] msg = new byte[32];
		// ruleid: cbc-mode
		var cipherText = key.EncryptCbc(msg, PaddingMode.PKCS7);
	}

	public void DecryptWithAesCbc(byte[] cipherText) {
		Aes key = Aes.Create();
		// ruleid: cbc-mode
		key.Mode = CipherMode.CBC;
		using var decryptor = key.CreateDecryptor();
		var msg = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
	}

	public void DecryptWithAesCbc2(byte[] cipherText) {
		Aes key = Aes.Create();
		// ruleid: cbc-mode
		var msgText = key.DecryptCbc(cipherText, PaddingMode.PKCS7);
	}

	public void EncryptWith3DESCbc() {
		TripleDES key = TripleDES.Create();
		// ruleid: cbc-mode
		key.Mode = CipherMode.CBC;
		using var encryptor = key.CreateEncryptor();
		byte[] msg = new byte[32];
		var cipherText = encryptor.TransformFinalBlock(msg, 0, msg.Length);
	}

	public void EncryptWith3DESCbc2() {
		TripleDES key = TripleDES.Create();
		byte[] msg = new byte[32];
		// ruleid: cbc-mode
		var cipherText = key.EncryptCbc(msg, PaddingMode.PKCS7);
	}

	public void DecryptWith3DESCbc(byte[] cipherText) {
		TripleDES key = TripleDES.Create();
		// ruleid: cbc-mode
		key.Mode = CipherMode.CBC;
		using var decryptor = key.CreateDecryptor();
		var msg = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
	}

	public void DecryptWith3DESCbc2(byte[] cipherText) {
		TripleDES key = TripleDES.Create();
		// ruleid: cbc-mode
		var msgText = key.DecryptCbc(cipherText, PaddingMode.PKCS7);
	}

	public void EncryptWithCbc(SymmetricAlgorithm key) {
		// ruleid: cbc-mode
		key.Mode = CipherMode.CBC;
		using var encryptor = key.CreateEncryptor();
		byte[] msg = new byte[32];
		var cipherText = encryptor.TransformFinalBlock(msg, 0, msg.Length);
	}

	public void EncryptWithCbc2(SymmetricAlgorithm key) {
		byte[] msg = new byte[32];
		// ruleid: cbc-mode
		var cipherText = key.EncryptCbc(msg, PaddingMode.PKCS7);
	}

	public void DecryptWithCbc(SymmetricAlgorithm key, byte[] cipherText) {
		// ruleid: cbc-mode
		key.Mode = CipherMode.CBC;
		using var decryptor = key.CreateDecryptor();
		var msg = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
	}

	public void DecryptWithCbc2(SymmetricAlgorithm key, byte[] cipherText) {
		// ruleid: cbc-mode
		var msgText = key.DecryptCbc(cipherText, PaddingMode.PKCS7);
	}

  public void ProvidersAndFieldAssignments() {
    // ruleid: cbc-mode
    AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider
    {
        Mode = CipherMode.CBC
    };

    AesCryptoServiceProvider aesCryptoServiceProvider2 = new AesCryptoServiceProvider();
    // ruleid: cbc-mode
    aesCryptoServiceProvider2.Mode = CipherMode.CBC;

    // ruleid: cbc-mode
    AesManaged aesManaged = new AesManaged
    {
        Mode = CipherMode.CBC
    };

    AesManaged aesManaged2 = new AesManaged();
    // ruleid: cbc-mode
    aesManaged2.Mode = CipherMode.CBC;

    // ruleid: cbc-mode
    RijndaelManaged rm = new RijndaelManaged
    {
        Mode = CipherMode.CBC
    };

    RijndaelManaged rm2 = new RijndaelManaged();
    // ruleid: cbc-mode
    rm2.Mode = CipherMode.CBC;

    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
    // ruleid: cbc-mode
    tdes.Mode = CipherMode.CBC;

    // ruleid: cbc-mode
    TripleDESCryptoServiceProvider tdes2 = new TripleDESCryptoServiceProvider
    {
        Mode = CipherMode.CBC
    };
  }
}
