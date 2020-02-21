using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace XamarinFormsApp.Model
{
  public static class PasswordEncrypter
  {
    private static RNGCryptoServiceProvider _rngCsp = new RNGCryptoServiceProvider();
    private static int _size = 50;

    public static bool CheckMatchingPasswords(string password, byte[] salt, byte[] hash)
    {
      var hashPassword = DoPasswordHashing(Encoding.ASCII.GetBytes(password), salt);

      return hash.Equals(hashPassword);
    }

    public static Tuple<string, byte[]> EncryptPassword(string password)
    {
      var salt = GenerateRandomSalt();
      var hashedPassword = DoPasswordHashing(Encoding.ASCII.GetBytes(password), salt);

      return new Tuple<string, byte[]>(hashedPassword, salt);
    }

    private static string DoPasswordHashing(byte[] password, byte[] salt)
    {
      var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
      byte[] hash = pbkdf2.GetBytes(20);
      byte[] hashBytes = new byte[36];
      Array.Copy(salt, 0, hashBytes, 0, 16);
      Array.Copy(hash, 0, hashBytes, 16, 20);
      return Convert.ToBase64String(hashBytes);
    }

    private static byte[] GenerateRandomSalt()
    {
      var bytes = new byte[_size];
      _rngCsp.GetBytes(bytes);
      return bytes;
    }
  }
}
