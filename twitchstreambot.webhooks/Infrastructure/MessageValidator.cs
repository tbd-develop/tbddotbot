using System.Security.Cryptography;
using System.Text;

namespace twitchstreambot.webhooks.Infrastructure;

public class MessageValidator(string secret)
{
    public bool IsValid(string messageId, string timestamp, string body, string signature)
    {
        var message = messageId + timestamp + body;

        var hash = $"sha256={ComputeHash(message, secret).ToLowerInvariant()}";

        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(hash).AsSpan(),
            Encoding.UTF8.GetBytes(signature).AsSpan());
    }

    private static string ComputeHash(string message, string secret)
    {
        var key = Encoding.UTF8.GetBytes(secret);
        var messageBytes = Encoding.UTF8.GetBytes(message);

        using var hmac = new HMACSHA256(key);

        var hash = hmac.ComputeHash(messageBytes);

        return Convert.ToHexString(hash);
    }
}