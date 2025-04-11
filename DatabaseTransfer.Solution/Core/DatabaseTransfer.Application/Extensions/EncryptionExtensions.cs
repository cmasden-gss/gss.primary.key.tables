using System;
using System.Security.Cryptography;
using System.Text;

namespace DatabaseTransfer.Application.Extensions
{
    public static class EncryptionExtensions
    {
        /// <summary>
        ///     Encrypts a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EncryptString(string text)
        {
            return Convert.ToBase64String(
                ProtectedData.Protect(
                    Encoding.Unicode.GetBytes(text), null, DataProtectionScope.LocalMachine));
        }

        /// <summary>
        ///     Decrypts a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string DecryptString(string text)
        {
            return Encoding.Unicode.GetString(
                ProtectedData.Unprotect(
                    Convert.FromBase64String(text), null, DataProtectionScope.LocalMachine));
        }
    }
}