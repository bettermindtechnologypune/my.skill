   using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Operation
{
   public class AesOperation
   {
      private readonly Random _random = new Random();
      public static string EncryptString(string key, string plainText)
      {
         byte[] iv = new byte[16];
         byte[] array;

         using (Aes aes = Aes.Create())
         {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
               using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
               {
                  using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                  {
                     streamWriter.Write(plainText);
                  }

                  array = memoryStream.ToArray();
               }
            }
         }

         return Convert.ToBase64String(array);
      }

      public static string DecryptString(string key, string cipherText)
      {
         byte[] iv = new byte[16];
         byte[] buffer = Convert.FromBase64String(cipherText);

         using (Aes aes = Aes.Create())
         {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
               using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
               {
                  using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                  {
                     return streamReader.ReadToEnd();
                  }
               }
            }
         }
      }

      // Generates a random password.  
      // 4-LowerCase + 4-Digits + 2-UpperCase  
      public static string RandomPassword()
      {
         var passwordBuilder = new StringBuilder();
         AesOperation aesOperation = new AesOperation();
         // 4-Letters lower case   
         passwordBuilder.Append(aesOperation.RandomString(4, true));

         // 4-Digits between 1000 and 9999  
         passwordBuilder.Append(aesOperation.RandomNumber(1000, 9999));

         // 2-Letters upper case  
         passwordBuilder.Append(aesOperation.RandomString(2));
         string special = "@#$-=/";
         passwordBuilder.Append(aesOperation.getRandomChar(special));


         return passwordBuilder.ToString();
      }


      private string getRandomChar(string fullString)
      {
         return fullString.ToCharArray()[(int)Math.Floor(_random.NextDouble() * fullString.Length)].ToString();
      }

     
      public string RandomNumber(int min, int max)
      {
         return _random.Next(min, max).ToString();
      }

      public string RandomString(int size, bool lowerCase = false)
      {
         var builder = new StringBuilder(size);

         // Unicode/ASCII Letters are divided into two blocks
         // (Letters 65–90 / 97–122):   
         // The first group containing the uppercase letters and
         // the second group containing the lowercase.  

         // char is a single Unicode character  
         char offset = lowerCase ? 'a' : 'A';
         const int lettersOffset = 26; // A...Z or a..z: length = 26  

         for (var i = 0; i < size; i++)
         {
            var @char = (char)_random.Next(offset, offset + lettersOffset);
            builder.Append(@char);
         }

         return lowerCase ? builder.ToString().ToLower() : builder.ToString();
      }
   }
}
