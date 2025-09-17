using System;
using System.Security.Cryptography;

public class Encryption
{
	public void EncryptWithAesEcb() {
		Aes key = Aes.Create();
		// ruleid: ecb-mode
		key.Mode = CipherMode.ECB;
		using var encryptor = key.CreateEncryptor();
		byte[] msg = new byte[32];
		var cipherText = encryptor.TransformFinalBlock(msg, 0, msg.Length);
	}

	public void EncryptWithAesEcb2() {
		Aes key = Aes.Create();
		byte[] msg = new byte[32];
		// ruleid: ecb-mode
		var cipherText = key.EncryptEcb(msg, PaddingMode.PKCS7);
	}

	public void DecryptWithAesEcb(byte[] cipherText) {
		Aes key = Aes.Create();
		// ruleid: ecb-mode
		key.Mode = CipherMode.ECB;
		using var decryptor = key.CreateDecryptor();
		var msg = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
	}

	public void DecryptWithAesEcb2(byte[] cipherText) {
		Aes key = Aes.Create();
		// ruleid: ecb-mode
		var msgText = key.DecryptEcb(cipherText, PaddingMode.PKCS7);
	}

	public void EncryptWith3DESEcb() {
		TripleDES key = TripleDES.Create();
		// ruleid: ecb-mode
		key.Mode = CipherMode.ECB;
		using var encryptor = key.CreateEncryptor();
		byte[] msg = new byte[32];
		var cipherText = encryptor.TransformFinalBlock(msg, 0, msg.Length);
	}

	public void EncryptWith3DESEcb2() {
		TripleDES key = TripleDES.Create();
		byte[] msg = new byte[32];
		// ruleid: ecb-mode
		var cipherText = key.EncryptEcb(msg, PaddingMode.PKCS7);
	}

	public void DecryptWith3DESEcb(byte[] cipherText) {
		TripleDES key = TripleDES.Create();
		// ruleid: ecb-mode
		key.Mode = CipherMode.ECB;
		using var decryptor = key.CreateDecryptor();
		var msg = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
	}

	public void DecryptWith3DESEcb2(byte[] cipherText) {
		TripleDES key = TripleDES.Create();
		// ruleid: ecb-mode
		var msgText = key.DecryptEcb(cipherText, PaddingMode.PKCS7);
	}

	public void EncryptWithEcb(SymmetricAlgorithm key) {
		// ruleid: ecb-mode
		key.Mode = CipherMode.ECB;
		using var encryptor = key.CreateEncryptor();
		byte[] msg = new byte[32];
		var cipherText = encryptor.TransformFinalBlock(msg, 0, msg.Length);
	}

	public void EncryptWithEcb2(SymmetricAlgorithm key) {
		byte[] msg = new byte[32];
		// ruleid: ecb-mode
		var cipherText = key.EncryptEcb(msg, PaddingMode.PKCS7);
	}

	public void DecryptWithEcb(SymmetricAlgorithm key, byte[] cipherText) {
		// ruleid: ecb-mode
		key.Mode = CipherMode.ECB;
		using var decryptor = key.CreateDecryptor();
		var msg = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
	}

	public void DecryptWithEcb2(SymmetricAlgorithm key, byte[] cipherText) {
		// ruleid: ecb-mode
		var msgText = key.DecryptEcb(cipherText, PaddingMode.PKCS7);
	}

  public void ProvidersAndFieldAssignments() {
    // ruleid: ecb-mode
    AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider
    {
        Mode = CipherMode.ECB
    };

    AesCryptoServiceProvider aesCryptoServiceProvider2 = new AesCryptoServiceProvider();
    // ruleid: ecb-mode
    aesCryptoServiceProvider2.Mode = CipherMode.ECB;

    // ruleid: ecb-mode
    AesManaged aesManaged = new AesManaged
    {
        Mode = CipherMode.ECB
    };

    AesManaged aesManaged2 = new AesManaged();
    // ruleid: ecb-mode
    aesManaged2.Mode = CipherMode.ECB;

    // ruleid: ecb-mode
    RijndaelManaged rm = new RijndaelManaged
    {
        Mode = CipherMode.ECB
    };

    RijndaelManaged rm2 = new RijndaelManaged();
    // ruleid: ecb-mode
    rm2.Mode = CipherMode.ECB;

    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
    // ruleid: ecb-mode
    tdes.Mode = CipherMode.ECB;

    // ruleid: ecb-mode
    TripleDESCryptoServiceProvider tdes2 = new TripleDESCryptoServiceProvider
    {
        Mode = CipherMode.ECB
    };
  }
}
