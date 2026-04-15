using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP2.COR;

public class MultiplyCHandler : MultiplyHandler
{
    private readonly IValueService _service;

    public MultiplyCHandler(IValueService service)
    {
        _service = service;
    }

    public override double Handle(double currentValue)
    {
        double factor = _service.GetValue("FactorC");
        double newValue = currentValue * factor;

        Console.WriteLine($"MultiplyCHandler: {currentValue} * {factor} = {newValue}");

        return _next?.Handle(newValue) ?? newValue;
    }
}

