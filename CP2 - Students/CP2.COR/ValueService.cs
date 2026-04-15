using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP2.COR;
public interface IValueService
{
    double GetValue(string key);
}

public class ValueService : IValueService
{
    private readonly Dictionary<string, double> _values = new()
    {
        { "FactorA", 2 },
        { "FactorB", 5 },
        { "FactorC", 3 },
        { "Expected", 30 }
    };

    public double GetValue(string key) => _values[key];
}

