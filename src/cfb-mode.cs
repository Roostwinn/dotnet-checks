using System;
using System.Security.Cryptography;

public class Encryption
{
	public void EncryptWithAesCfb() {
		Aes key = Aes.Create();
		// ruleid: cfb-mode
		key.Mode = CipherMode.CFB;
		using var encryptor = key.CreateEncryptor();
		byte[] msg = new byte[32];
		var cipherText = encryptor.TransformFinalBlock(msg, 0, msg.Length);
	}

	public void EncryptWithAesCfb2() {
		Aes key = Aes.Create();
		byte[] msg = new byte[32];
		// ruleid: cfb-mode
		var cipherText = key.EncryptCfb(msg, PaddingMode.PKCS7);
	}

	public void DecryptWithAesCfb(byte[] cipherText) {
		Aes key = Aes.Create();
		// ruleid: cfb-mode
		key.Mode = CipherMode.CFB;
		using var decryptor = key.CreateDecryptor();
		var msg = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
	}

	public void DecryptWithAesCfb2(byte[] cipherText) {
		Aes key = Aes.Create();
		// ruleid: cfb-mode
		var msgText = key.DecryptCfb(cipherText, PaddingMode.PKCS7);
	}

	public void EncryptWith3DESCfb() {
		TripleDES key = TripleDES.Create();
		// ruleid: cfb-mode
		key.Mode = CipherMode.CFB;
		using var encryptor = key.CreateEncryptor();
		byte[] msg = new byte[32];
		var cipherText = encryptor.TransformFinalBlock(msg, 0, msg.Length);
	}

	public void EncryptWith3DESCfb2() {
		TripleDES key = TripleDES.Create();
		byte[] msg = new byte[32];
		// ruleid: cfb-mode
		var cipherText = key.EncryptCfb(msg, PaddingMode.PKCS7);
	}

	public void DecryptWith3DESCfb(byte[] cipherText) {
		TripleDES key = TripleDES.Create();
		// ruleid: cfb-mode
		key.Mode = CipherMode.CFB;
		using var decryptor = key.CreateDecryptor();
		var msg = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
	}

	public void DecryptWith3DESCfb2(byte[] cipherText) {
		TripleDES key = TripleDES.Create();
		// ruleid: cfb-mode
		var msgText = key.DecryptCfb(cipherText, PaddingMode.PKCS7);
	}

	public void EncryptWithCfb(SymmetricAlgorithm key) {
		// ruleid: cfb-mode
		key.Mode = CipherMode.CFB;
		using var encryptor = key.CreateEncryptor();
		byte[] msg = new byte[32];
		var cipherText = encryptor.TransformFinalBlock(msg, 0, msg.Length);
	}

	public void EncryptWithCfb2(SymmetricAlgorithm key) {
		byte[] msg = new byte[32];
		// ruleid: cfb-mode
		var cipherText = key.EncryptCfb(msg, PaddingMode.PKCS7);
	}

	public void DecryptWithCfb(SymmetricAlgorithm key, byte[] cipherText) {
		// ruleid: cfb-mode
		key.Mode = CipherMode.CFB;
		using var decryptor = key.CreateDecryptor();
		var msg = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
	}

	public void DecryptWithCfb2(SymmetricAlgorithm key, byte[] cipherText) {
		// ruleid: cfb-mode
		var msgText = key.DecryptCfb(cipherText, PaddingMode.PKCS7);
	}

  public void ProvidersAndFieldAssignments() {
    // ruleid: cfb-mode
    AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider
    {
        Mode = CipherMode.CFB
    };

    AesCryptoServiceProvider aesCryptoServiceProvider2 = new AesCryptoServiceProvider();
    // ruleid: cfb-mode
    aesCryptoServiceProvider2.Mode = CipherMode.CFB;

    // ruleid: cfb-mode
    AesManaged aesManaged = new AesManaged
    {
        Mode = CipherMode.CFB
    };

    AesManaged aesManaged2 = new AesManaged();
    // ruleid: cfb-mode
    aesManaged2.Mode = CipherMode.CFB;

    // ruleid: cfb-mode
    RijndaelManaged rm = new RijndaelManaged
    {
        Mode = CipherMode.CFB
    };

    RijndaelManaged rm2 = new RijndaelManaged();
    // ruleid: cfb-mode
    rm2.Mode = CipherMode.CFB;

    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
    // ruleid: cfb-mode
    tdes.Mode = CipherMode.CFB;

    // ruleid: cfb-mode
    TripleDESCryptoServiceProvider tdes2 = new TripleDESCryptoServiceProvider
    {
        Mode = CipherMode.CFB
    };
  }
}
