using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Helpers
{
    public class LinkGenerator
    {
        public string GenerateCustom(string baseUrl, int length)
        {
            var Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var StringChars = new char[length];
            var Seed = (int)DateTime.Now.Ticks;
            var Random = new Random(Seed);

            for(int i = 0; i < length; i++)
            {
                StringChars[i] = Chars[Random.Next(Chars.Length)];
            }

            var GeneratedIdentifier = new String(StringChars);

            return baseUrl + "/" + GeneratedIdentifier;
        }

        public string GenerateSignup()
        {
            var BaseUrl = "https://localhost:44315/Users/Create";
            var Length = 10;

            return GenerateCustom(BaseUrl, Length);
        }

        public string GenerateJoin()
        {
            var BaseUrl = "https://localhost:44315/AgreedMeetings";
            var Length = 10;

            return GenerateCustom(BaseUrl, Length);
        }
    }
}
