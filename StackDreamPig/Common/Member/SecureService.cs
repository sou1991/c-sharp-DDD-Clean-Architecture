using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Common.Member
{
    public class SecureService
    {

        public (string secureValue, byte[] salt) HashToValue(string notSecureValue)
        {
            var salt = GetSalt();
            var secureValue = HashToValue(notSecureValue, salt);
            return (secureValue, salt);
        }

        public bool VerifyPassword(string secureValue, string notSecureValue, byte[] salt) =>
          secureValue == HashToValue(notSecureValue, salt);

        private string HashToValue(string notSecureValue, byte[] salt) =>
          Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
              password: notSecureValue,
              salt: salt,
              prf: KeyDerivationPrf.HMACSHA512,
              iterationCount: 10000,
              numBytesRequested: 256 / 8));

        private byte[] GetSalt()
        {
            using (var gen = RandomNumberGenerator.Create())
            {
                var salt = new byte[128 / 8];
                gen.GetBytes(salt);
                return salt;
            }
        }
        //To Do 他のシリアライザークラスの検討。暗号化は可能だがsalt自体がJson形式でない為。
        public string ToSerialize(byte[] salt)
        {
            return JsonSerializer.Serialize(salt);
        }
        //To Do 他のシリアライザークラスの検討。暗号化は可能だがsalt自体がJson形式でない為。
        public byte[] ToDeserialize(string salt)
        {
            return JsonSerializer.Deserialize<byte[]>(salt);
        }
    }
}
