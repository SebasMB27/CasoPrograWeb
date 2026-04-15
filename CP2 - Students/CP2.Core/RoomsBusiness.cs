using CP2.Architecture;
using CP2.Architecture.Providers;
using CP2.Data.Global;
using System.Text.Json;

namespace CP2.Core;

public interface IRoomsBusiness
{
    Task<bool> SolutionIndexAsync(string code);
    Task<bool> SolutionRoom1Async(int num);
    Task<bool> SolutionRoom2Async(string code);
    Task<bool> SolutionRoom3Async(string code);
    Task<bool> SolutionRoom4Async(string code);
    Task<bool> SolutionRoom5Async();
    Task<bool> SolutionRoom6Async(int num);
    Task<bool> SolutionRoom7Async(string code);
    Task<bool> SolutionRoom8Async();
    Task<bool> SolutionRoom9Async(string code);
    Task<bool> SolutionRoom10Async(string code);
    Task<bool> SolutionRoom11Async(string code);
    Task<bool> SolutionRoom12Async(string code);
    Task<bool> SolutionRoom13Async(string code);
    Task<bool> SolutionRoom14Async(string code);
    Task<bool> CanExitTheRoomsAsync(string code);
}

public class RoomsBusiness(
    IRestProvider restProvider,
    SecureHashService secureHashService,
    IReadOnlyDictionary<int, string> roomConfigs) : RoomsBase(restProvider, secureHashService, roomConfigs), IRoomsBusiness
{
    private readonly IRestProvider _restProvider = restProvider;

    public async Task<bool> SolutionTestAsync(string code)
    {
        // solucion aqui
        code = "test";

        // codiguito aqui
        // lalalala lalalala
        // code = resultado de lalalal

        var resultHash = Evaluate(0, code);
        var resultApi = await CallApiAsync("test", code);
        return (resultHash && resultApi);
    }

    public async Task<bool> SolutionIndexAsync(string code)
    {
        var letters = "REKGVZQ".ToCharArray();

        foreach (var permutation in GetPermutations(letters))
        {
            var candidate = new string(permutation);

            if (Evaluate(0, candidate))
            {
                code = candidate; 
                return true;
            }
        }

        return false; 
    }

    private IEnumerable<char[]> GetPermutations(char[] letters)
    {
        if (letters.Length <= 1)
        {
            yield return letters;
            yield break;
        }

        for (int i = 0; i < letters.Length; i++)
        {
            var current = letters[i];
            var rest = letters.Where((_, idx) => idx != i).ToArray();

            foreach (var perm in GetPermutations(rest))
                yield return new[] { current }.Concat(perm).ToArray();
        }
    }


    public async Task<bool> SolutionRoom1Async(int x)
    {
        /*         for (int i = 2; i <= 10; i++)    {        string code = $"{i}{i * i}{3 * i}{i * i * i}";        System.Diagnostics.Debug.WriteLine($"Probando: {code}");        var resultHash = Evaluate(1, code);        if (resultHash)        {            System.Diagnostics.Debug.WriteLine($"✅ ENCONTRADO: {code}");            var resultApi = await CallApiAsync("test", code);            return resultApi;        }    }    return false;}         */
        string code = "74921343";

        var resultHash = Evaluate(1, code);

        return resultHash;
    }


    public async Task<bool> SolutionRoom2Async(string code)
    {
        code = "SOLID";

        var resultHash = Evaluate(2, code); return resultHash;
    }

    public async Task<bool> SolutionRoom3Async(string code)
    {
        var key = "E4A1F9B7C32D8F64A9F1C0D3B7E2A6CC4F18B92ED0C4A7F1D3B89C6A5F2E1D44";
        var service = new SecureHashService(key);

        var nameVariants = new[]
        {
        "Alvaro Andrei Miranda Muñoz",
        "alvaro andrei miranda muñoz",
        "Alvaro Miranda",
        "alvaro miranda",
        "Alvaro",
        "middleware",
    };

        foreach (var name in nameVariants)
        {
            // Hash directo del nombre
            var result = service.Hash(name);
            if (Evaluate(3, result)) return true;

            //  Double hash — hash del hash si el pirmero hash no funciona
            var doubleResult = service.Hash(result);
            if (Evaluate(3, doubleResult))
                return true;
        }

        return false;
    }

    public async Task<bool> SolutionRoom4Async(string code)
    {
        // solucion aqui
        return false; // Placeholder logic
    }

    public async Task<bool> SolutionRoom5Async()
    {
        // solucion aqui
        return true; // Placeholder logic
    }

    public async Task<bool> SolutionRoom6Async(int num)
    {
        // solucion aqui
        return true; // Placeholder logic
    }

    public async Task<bool> SolutionRoom7Async(string code)
    {
        // solucion aqui
        return true; // Placeholder logic
    }

    public async Task<bool> SolutionRoom8Async()
    {
        // solucion aqui
        return true; // Placeholder logic
    }

    public async Task<bool> SolutionRoom9Async(string code)
    {
        // solucion aqui
        return true; // Placeholder logic
    }

    public async Task<bool> SolutionRoom10Async(string code)
    {
        // solucion aqui
        return true; // Placeholder logic
    }
    public async Task<bool> SolutionRoom11Async(string code)
    {
        // solucion aqui
        return true; // Placeholder logic
    }
    public async Task<bool> SolutionRoom12Async(string code)
    {
        // solucion aqui
        return true; // Placeholder logic
    }
    public async Task<bool> SolutionRoom13Async(string code)
    {
        // solucion aqui
        return true; // Placeholder logic
    }
    public async Task<bool> SolutionRoom14Async(string code)
    {
        // solucion aqui
        return true; // Placeholder logic
    }

    public async Task<bool> CanExitTheRoomsAsync(string code)
    {
        // solucion aqui
        return true; // Placeholder logic
    }
}
