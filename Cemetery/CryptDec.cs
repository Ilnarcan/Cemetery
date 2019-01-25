using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Cemetery
{
    internal class CryptDec
    {
        private static readonly byte[] RgbKey = {0, 1, 2, 3, 4, 5, 6, 7};
        // Вектор (8 байт)
        private static readonly byte[] RgbIV = {0, 1, 2, 3, 4, 5, 6, 7};

        public static string CalculateSHA512Hash(string input)
        {
            SHA512 sha = SHA512.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        public static string CalculateSHA1Hash(string input)
        {
            SHA1 sha = SHA1.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        public static string CalculateMd5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        public static void CryptFile(string file, string file2, SymmetricAlgorithm algo)
        {
            using (SymmetricAlgorithm sa = algo)
            // Создаем поток для записи зашифрованных данных
            using (FileStream fsw = File.Open(file2, FileMode.Create, FileAccess.Write))
            // Создаем крипто-поток для записи
            using (var cs = new CryptoStream(fsw,
                sa.CreateEncryptor(RgbKey, RgbIV), CryptoStreamMode.Write))
            {
                // Читаем исходный файл
                byte[] buff;
                using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read))
                {
                    buff = new byte[fs.Length + sizeof(long)];
                    fs.Read(buff, sizeof(long), buff.Length - sizeof(long));
                    /* Записываем в первые 8 байт длину исходного файла
                     * нужно это для того чтобы, после дешифровки не было
                     * лишних данных
                     */
                    int i = 0;
                    foreach (byte @byte in BitConverter.GetBytes(fs.Length))
                        buff[i++] = @byte;
                }

                cs.Write(buff, 0, buff.Length);
                cs.Flush();
            }
        }

        public static void DecryptFile(string file, string file2, SymmetricAlgorithm algo)
        {
            using (SymmetricAlgorithm sa = algo)
            // Создаем поток для чтения шифрованных данных
            using (FileStream fsr = File.Open(file, FileMode.Open, FileAccess.Read))
            // Создаем крипто-поток для чтения
            using (var cs = new CryptoStream(fsr,
                sa.CreateDecryptor(RgbKey, RgbIV), CryptoStreamMode.Read))
            {
                // Дешифровываем исходный поток данных
                var buff = new byte[fsr.Length];
                cs.Read(buff, 0, buff.Length);
                // Пишем дешифрованные данные
                using (FileStream fsw = File.Open(file2, FileMode.Create, FileAccess.Write))
                {
                    // Читаем записанную длину исходного файла
                    var len = (int)BitConverter.ToInt64(buff, 0);
                    // Пишем только ту часть дешифрованных данных,
                    // которая представляет исходный файл
                    fsw.Write(buff, sizeof(long), len);
                    fsw.Flush();
                }
            }
        }
        public static string Encrypt(string plainText, string password,
             string salt = "Kosher", string hashAlgorithm = "SHA1",
           int passwordIterations = 2, string initialVector = "OFRna73m*aze01xY",
            int keySize = 256)
        {
            if (string.IsNullOrEmpty(plainText))
                return "";

            byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            var derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
#pragma warning disable 618
            var keyBytes = derivedPassword.GetBytes(keySize / 8);
#pragma warning restore 618
            var symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC};

            byte[] cipherTextBytes;

            using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, initialVectorBytes))
            {
                using (var memStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memStream.ToArray();
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmetricKey.Clear();
            return Convert.ToBase64String(cipherTextBytes);
        }



        /// <summary>
        /// Decrypts a string
        /// </summary>
        /// <param name="cipherText">Text to be decrypted</param>
        /// <param name="password">Password to decrypt with</param>
        /// <param name="salt">Salt to decrypt with</param>
        /// <param name="hashAlgorithm">Can be either SHA1 or MD5</param>
        /// <param name="passwordIterations">Number of iterations to do</param>
        /// <param name="initialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="keySize">Can be 128, 192, or 256</param>
        /// <returns>A decrypted string</returns>
        public static string Decrypt(string cipherText, string password,
           string salt = "Kosher", string hashAlgorithm = "SHA1",
           int passwordIterations = 2, string initialVector = "OFRna73m*aze01xY",
            int keySize = 256)
        {
            if (string.IsNullOrEmpty(cipherText))
                return "";

            byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            var derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
#pragma warning disable 618
            byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);
#pragma warning restore 618

            var symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC};

            var plainTextBytes = new byte[cipherTextBytes.Length];
            int byteCount;

            using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, initialVectorBytes))
            {
                using (var memStream = new MemoryStream(cipherTextBytes))
                {
                    using (var cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmetricKey.Clear();
            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
        }


        public static string NewForm(string str)
        {
            return Decrypt(str, "Passpord11", "Password22", "SHA1", 2, "16CHARSLONG12345");
        }
        public static string BackToForm(string str)
        {
            return Encrypt(str, "Passpord11", "Password22", "SHA1", 2, "16CHARSLONG12345");
        }


    }
}