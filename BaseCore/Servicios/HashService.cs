using BaseCore.Entidades.Util;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace BaseCore.Servicios
{
    public interface IHashService
    {
        ResultadoHash Hash(string textoSinHash);
        ResultadoHash Hash(string textoSinHash, byte[] sal);
    }
    public class HashService: IHashService
    {
        public ResultadoHash Hash(string textoSinHash)
        {
            var sal = new byte[16];
            using (var ramdom = RandomNumberGenerator.Create())
            {
                ramdom.GetBytes(sal);
            }

            return Hash(textoSinHash, sal);
        }

        public ResultadoHash Hash(string textoSinHash, byte[] sal)
        {
            var llaveDerivada = KeyDerivation.Pbkdf2(password: textoSinHash,
                salt: sal,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32);
            var hash = Convert.ToBase64String(llaveDerivada);
            return new ResultadoHash()
            {
                Hash = hash,
                Sal = sal
            };
        }
    }
}
