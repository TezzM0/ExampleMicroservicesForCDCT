using System;

namespace AngleClient
{
    internal class Clients
    {
        private static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static Random random = new Random();
        
        public static string GetClientCode()
        {
            var clientCodeLength = random.Next(3, 5);
            var stringChars = new char[clientCodeLength];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}