using System.Text;
using System.Text.RegularExpressions;

namespace DataAcccess.Common
{
    public static class Sercurity
    {
        public static string ComputeSha256Hash(string password)
        {
            // Create a SHA256
            using (System.Security.Cryptography.SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        public static string GetSaltedHash(string password)
        {
            // Generate a salt
            var salt = "Hqr^@iG%.GlE3)o"; // 32 bytes = 256 bits

            // Compute the hash
            var hash = ComputeHash(password, salt);

            // Combine salt and hash for storage
            var saltedHash = new byte[salt.Length + hash.Length];
            //  Buffer.BlockCopy(salt, 0, saltedHash, 0, salt.Length);
            //Buffer.BlockCopy(hash, 0, saltedHash, salt.Length, hash.Length);

            // Return as a base64 string
            return Convert.ToBase64String(saltedHash);
        }
        public static byte[] ComputeHash(string password, string salt)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var saltedPassword = Encoding.UTF8.GetBytes(password);
                var saltedPasswordWithSalt = new byte[saltedPassword.Length + salt.Length];

                //Buffer.BlockCopy(saltedPassword, 0, saltedPasswordWithSalt, 0, saltedPassword.Length);
                //Buffer.BlockCopy(salt, 0, saltedPasswordWithSalt, saltedPassword.Length, salt.Length);

                return sha256.ComputeHash(saltedPasswordWithSalt);
            }
        }
        public static bool CheckSpecicalCharacter(string inputString)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(inputString)) { return false; }
            return true;
        }
        public static bool CheckXSSInput(string input)
        {
            try
            {
                var listdangerousString = new List<string> { "<applet", "<body", "<embed", "<frame", "<script", "<frameset", "<html", "<iframe", "<img", "<style", "<layer", "<link", "<ilayer", "<meta", "<object", "<h", "<input", "<a", "&lt", "&gt" };
                if (string.IsNullOrEmpty(input)) return false;
                foreach (var dangerous in listdangerousString)
                {
                    if (input.Trim().ToLower().IndexOf(dangerous) >= 0) return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
