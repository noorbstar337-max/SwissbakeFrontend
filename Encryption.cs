// Decompiled with JetBrains decompiler
// Type: prolib.Encryption
// Assembly: prolib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2FF12A11-4E83-40E7-A1E3-AE3629DFF068
// Assembly location: D:\CURRENT\PROJECTS\CLIENTS\SwissbakeBH\BACKUP\uat.swissbake.bh\bin\prolib.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace prolib
{
  public class Encryption
  {
    public static string Encrypt(string clearText)
    {
      string password = "MAKV2SPBNI99212";
      byte[] bytes = Encoding.Unicode.GetBytes(clearText);
      using (Aes aes = Aes.Create())
      {
        Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
        {
          (byte) 73,
          (byte) 118,
          (byte) 97,
          (byte) 110,
          (byte) 32,
          (byte) 77,
          (byte) 101,
          (byte) 100,
          (byte) 118,
          (byte) 101,
          (byte) 100,
          (byte) 101,
          (byte) 118
        });
        aes.Key = rfc2898DeriveBytes.GetBytes(32);
        aes.IV = rfc2898DeriveBytes.GetBytes(16);
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
          {
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.Close();
          }
          clearText = Convert.ToBase64String(memoryStream.ToArray());
        }
      }
      return clearText;
    }

    public static string Decrypt(string cipherText)
    {
      string password = "MAKV2SPBNI99212";
      byte[] buffer = Convert.FromBase64String(cipherText);
      using (Aes aes = Aes.Create())
      {
        Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
        {
          (byte) 73,
          (byte) 118,
          (byte) 97,
          (byte) 110,
          (byte) 32,
          (byte) 77,
          (byte) 101,
          (byte) 100,
          (byte) 118,
          (byte) 101,
          (byte) 100,
          (byte) 101,
          (byte) 118
        });
        aes.Key = rfc2898DeriveBytes.GetBytes(32);
        aes.IV = rfc2898DeriveBytes.GetBytes(16);
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
          {
            cryptoStream.Write(buffer, 0, buffer.Length);
            cryptoStream.Close();
          }
          cipherText = Encoding.Unicode.GetString(memoryStream.ToArray());
        }
      }
      return cipherText;
    }
  }
}
