using CP2.Architecture;
using CP2.Architecture.Providers;
using CP2.Data.Global;
using CP2.Data.Models;
using System.Security.Cryptography;
using System.Text;

namespace CP2.Core;

public abstract class RoomsBase
{
    private readonly SecureHashService _secureHashService;
    private readonly IReadOnlyDictionary<int, string> _roomConfigs;
    private readonly IRestProvider _restProvider;

    protected RoomsBase(IRestProvider restProvider, SecureHashService secureHashService, IReadOnlyDictionary<int, string> roomConfigs)
    {
        _restProvider = restProvider;
        _secureHashService = secureHashService;
        _roomConfigs = roomConfigs;
    }

    protected bool HashAndCompare(string inputValue, string expectedValue, string? key = null)
    {
        key ??= "CB9BF1B1-3D9A-4B4C-B1DE-B83E1F4C2FCA";
        var inputHash = HashValue(inputValue, key);
        var expectedHash = HashValue(expectedValue, key);

        return CryptographicOperations.FixedTimeEquals(
            Convert.FromBase64String(inputHash),
            Convert.FromBase64String(expectedHash));
    }

    protected bool Evaluate(int roomNumber, string inputValue, string? key = "CB9BF1B1-3D9A-4B4C-B1DE-B83E1F4C2FCA")
        => CompareInputWithRoomValue(roomNumber, inputValue, key);

    protected async Task<bool> CallApiAsync(string roomName, string inputValue)
    {
        var request = new MatchRequest
        {
            RoomName = roomName,
            InputValue = inputValue
        };

        var json = System.Text.Json.JsonSerializer.Serialize(request);

        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var resp = await _restProvider.PostAsync(
            "https://restfulapi-dp29.onrender.com/api/items/test",
            await content.ReadAsStringAsync()
        );

        if (string.IsNullOrWhiteSpace(resp))
            return false;

        var result =  JsonProvider.DeserializeSimple<MatchResponse>(resp);
        return result != null && result.Match;
    }

    private bool CompareInputWithRoomValue(int roomNumber, string inputValue, string? key = null)
    {
        if (!_roomConfigs.TryGetValue(roomNumber, out var expectedValue))
            return false;

        try
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                var inputHash = HashValue(inputValue, key);
                return CryptographicOperations.FixedTimeEquals(
                    Convert.FromBase64String(inputHash),
                    Convert.FromBase64String(expectedValue));
            }

            return _secureHashService.Validate(inputValue, expectedValue);
        }
        catch (FormatException)
        {
            return HashAndCompare(inputValue, expectedValue, key);
        }
    }

    private string HashValue(string value, string? key = null)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return _secureHashService.Hash(value);
        }

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        var bytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(hmac.ComputeHash(bytes));
    }
}
