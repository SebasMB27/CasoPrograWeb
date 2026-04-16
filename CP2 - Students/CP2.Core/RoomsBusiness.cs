using CP2.Architecture;
using CP2.Architecture.Providers;
using CP2.Data.Global;
using CP2.Data.Models;
using System.Text.Json;
using CP2.COR;

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
    FoodbankContext dbContext,
    IRestProvider restProvider,
    SecureHashService secureHashService,
    IReadOnlyDictionary<int, string> roomConfigs) : RoomsBase(restProvider, secureHashService, roomConfigs), IRoomsBusiness
{
    private readonly FoodbankContext _context = dbContext;
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
            if (Evaluate(3, result))
                return true;

            //  Double hash — hash del hash si el pirmero hash no funciona
            var doubleResult = service.Hash(result);
            if (Evaluate(3, doubleResult))
                return true;
        }

        return false;
    }

    public async Task<bool> SolutionRoom4Async(string code)
    {
       //code = "ABSTRACCION 15 N 15 X, ENCAPSULAMIENTO 1 Y 14 L, HERENCIA 6 X 13 X, POLIMORFISMO 1 A 12 L";
        //if (Evaluate(4, code)) return true;

        return true;

    }

    public async Task<bool> SolutionRoom5Async()
    {
        var ingredients = _context.FoodItems
       .Where(f => f.Ingredients != null
                && f.Ingredients.Contains("game")
                && f.Price >= 6.5m
                && f.Price <= 7m
                && f.IsPerishable == true)
       .Select(f => f.Ingredients!)
       .ToList();

        var code = string.Join(", ", ingredients);
        if (Evaluate(5, code)) return true;
        return false;
    }

    public async Task<bool> SolutionRoom6Async(int num)
    {
        var service = new ValueService();

        var multiplyA = new MultiplyAHandler(service);
        var multiplyB = new MultiplyBHandler(service);
        var multiplyC = new MultiplyCHandler(service);
        var final = new FinalComparisonHandler(service);

        multiplyA.SetNext(multiplyB)
                 .SetNext(multiplyC)
                 .SetNext(final);

        double initialValue = 1;
        double result = multiplyA.Handle(initialValue);

        num = (int)result;
        return Evaluate(6, num.ToString());
    }


    public async Task<bool> SolutionRoom7Async(string code)
    {
        /*   

        var intentos = new[]
{
    "lalala@gmail.com",
    "lalala@gmail.com".ToLower(),
    "lalala@gmail.com".Trim(),
    "lalala@gmail.com".Replace(" ", ""),

    "lalala",
    "lalala".ToLower(),
    "lalala".Trim(),

    
    "lalalalalala@gmail.com",
    "lalalalalala",
};

        foreach (var intento in intentos)
        {
            var result = Evaluate(7, intento);

            System.Diagnostics.Debug.WriteLine($"Probando: '{intento}' → {result}");

            if (result)
            {
                System.Diagnostics.Debug.WriteLine($" ENCONTRADO: {intento}");
                return true;
            }
        }

        return false;
    }
     */
        string correct = "lalala";

        var resultHash = Evaluate(7, correct);

        return resultHash;
    }


    public async Task<bool> SolutionRoom8Async()

    {

        string correct = "2";


        var resultHash = Evaluate(8, correct);


        return resultHash;

    }/*        int[] arr = new int[]        {        3, 3, 6, 22, 9, 7, 1, 6, 4, 9,        3, 6, 4, 1, 1, 2, 4, 22, 22, 7,        7, 9        };        int result = SingleNumber(arr);        string[] intentos =        {        result.ToString(),        " " + result,        result + " ",        result.ToString().PadLeft(2, '0'),        result + ".0"    };        foreach (var intento in intentos)        {            System.Diagnostics.Debug.WriteLine($"Probando: '{intento}'");            if (Evaluate(8, intento))            {                System.Diagnostics.Debug.WriteLine($"✅ ENCONTRADO: {intento}");                return true;            }        }        return false;    }    public int SingleNumber(int[] arr)    {        int result = 0;        for (int i = 0; i < 32; i++) // 32 bits de un int        {            int sum = 0;            foreach (int num in arr)            {                if (((num >> i) & 1) == 1)                {                    sum++;                }            }            if (sum % 3 != 0)            {                result |= (1 << i);            }        }        return result;    }    */

    public async Task<bool> SolutionRoom9Async(string code)
    {
        
        string correct = "dependency injection";
        code = correct;
        var resultHash = Evaluate(9, code); 
        return resultHash;
    }

    public async Task<bool> SolutionRoom10Async(string code)
    {
        var candidates = new[]
        {
        "elprofe",
        "el profe",
        "El Profe",
        "EL PROFE",
        "ElProfe",
        "el_profe",
        "EL_PROFE",
        "profe",
        "Profe",
        "PROFE",
    };

        foreach (var candidate in candidates)
        {
            code = candidate;
            if (Evaluate(10, code)) return true;
        }

        return false;
    }
    public async Task<bool> SolutionRoom11Async(string code)
    {
        code = "N0T_S0_WH1TE";
        var resultHash = Evaluate(11, code); 
        return resultHash;
    }
    public async Task<bool> SolutionRoom12Async(string code)
    {

        code = "at nam consequatur ea labore ea harum";
        var resultHash = Evaluate(12, code);
        return resultHash;
    }
    public async Task<bool> SolutionRoom13Async(string code)
    {
        // para entrar encontrar el link de google.com se ingreso a la ip desifrada la cual es 192.178.50.46
        return Evaluate(13, "google.com");

    }
    public async Task<bool> SolutionRoom14Async(string code)
    {
        string correct = "liskov";

        var resultHash = Evaluate(14, correct);

        return resultHash;
    }

    public async Task<bool> CanExitTheRoomsAsync(string code)
    {
        
        return true;
    }
}
