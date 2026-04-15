using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var solver = new Solver();
        await solver.SolutionIndexAsync("");

        Console.WriteLine("Terminado");
        Console.ReadLine();
    }
}

public class Solver
{
    public async Task<bool> SolutionIndexAsync(string code)
    {
        var letters = "REKGVZQ".ToCharArray();
        return await TryPermutations(letters, 0);
    }

    private async Task<bool> TryPermutations(char[] letters, int start)
    {
        if (start == letters.Length)
        {
            var code = new string(letters);
            Console.WriteLine($"Probando: {code}");

            // COMPARA CON TU HASH REAL
            var resultHash = Evaluate(0, code);
            var resultApi = await CallApiAsync("test", code);

            if (resultHash && resultApi)
            {
                Console.WriteLine($"Código encontrado: {code}");
                return true;
            }

            return false;
        }

        for (int i = start; i < letters.Length; i++)
        {
            (letters[start], letters[i]) = (letters[i], letters[start]);

            if (await TryPermutations(letters, start + 1))
                return true;

            (letters[start], letters[i]) = (letters[i], letters[start]);
        }

        return false;
    }

    // IMPORTANTE: aquí debes usar tus métodos reales
    private bool Evaluate(int room, string code)
    {
        // conecta con tu hash real
        throw new NotImplementedException();
    }

    private async Task<bool> CallApiAsync(string test, string code)
    {
        // conecta con tu API real
        throw new NotImplementedException();
    }
}