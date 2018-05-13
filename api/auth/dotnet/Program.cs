using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DotNetSample
{
    // NOTE: nimal, 3/23/2018 More info https://devblog.skype.com/skype-interviews/2018/02/27/new-authentication-model-skype-interviews-api/
    class Program
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static string YOUR_API_KEY = "PUT YOUR API KEY HERE";
        private static string YOUR_API_SECRET = "PUT YOUR API SECRET HERE";

        static void Main()
        {
            Console.WriteLine("Requesting new interview...");

            var result = CreateInterviewAsync().GetAwaiter().GetResult();

            Console.WriteLine("Response:" + result);

            Console.WriteLine("---- Press any key to end demo ---");
            Console.ReadKey();
        }

        private static async Task<string> CreateInterviewAsync()
        {
            // NOTE: nimal, 3/23/2018 - read about different payloads on https://dev.skype.com/interviews
            var payload = "{}";
            var payloadHash = GetSha256Hash(payload);
            
            // create a token valid for the next 10 secomnds
            var token = GetJwtToken(YOUR_API_KEY, YOUR_API_SECRET, payloadHash, DateTime.UtcNow, 10);

            // create interview
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                var response = await client.PostAsync("https://interviews.skype.com/api/interviews", new StringContent(payload, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }


        private static string GetJwtToken(string apiKey, string secret, string sha256PayloadHash, DateTime? issuedAt = null, int expirationInSeconds = 60)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            if (issuedAt.HasValue)
            {
                now= issuedAt.Value;
            }

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, apiKey),
                new Claim(JwtRegisteredClaimNames.Iat, ToEpochMillis(now).ToString(), ClaimValueTypes.DateTime)
                
            };

            if (!string.IsNullOrWhiteSpace(sha256PayloadHash))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, sha256PayloadHash));
            }

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: now.AddSeconds(expirationInSeconds),
                signingCredentials: signingCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
        
        private static string GetSha256Hash(string payload)
        {
            using (var crypt = new SHA256Managed())
            {
                var hash = new StringBuilder();
                var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(payload), 0, Encoding.UTF8.GetByteCount(payload));
                foreach (var theByte in crypto)
                {
                    hash.Append(theByte.ToString("x2"));
                }
                return hash.ToString();
            }
            
        }

                
        private static long ToEpochMillis(DateTime dateTime)
        {
            return (long) (dateTime - UnixEpoch).TotalMilliseconds;
        }

    }


}
