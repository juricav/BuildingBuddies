using System;

namespace BuildingBuddies.Helpers
{
    public class LinkGenerator
    {
        public static string GenerateRandomString(int? length)
        {
            int Length = (length.HasValue) ? (int)length : 10;
            var Chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";
            var StringChars = new char[Length];
            var Seed = (int)DateTime.Now.Ticks;
            var Random = new Random(Seed);

            for (int i = 0; i < Length; i++)
            {
                StringChars[i] = Chars[Random.Next(Chars.Length)];
            }

            var GeneratedIdentifier = new String(StringChars);

            return GeneratedIdentifier;
        }
    }
}
