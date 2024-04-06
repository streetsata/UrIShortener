using Microsoft.EntityFrameworkCore;

namespace UrIShortener.Services
{
    public class UrlShorteningService(ApplicationDbContext dbContext)
    {
        public const int NumberOfCharsInShortUrl = 7;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private readonly Random _random = new();
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<string> GenerateUniqueCode()
        {
            var codeChars = new char[NumberOfCharsInShortUrl];

            while (true)
            {
                for (var i = 0; i < NumberOfCharsInShortUrl; i++)
                {
                    var randomIndex = _random.Next(Alphabet.Length);

                    codeChars[i] = Alphabet[randomIndex];
                }

                var code = new string(codeChars);

                if (!await _dbContext.ShortenedUrls.AnyAsync(s => s.Code == code))
                {
                    return code;
                }
            }
        }
    }
}
