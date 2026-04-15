namespace CP2.COR;

public class MultiplyBHandler : MultiplyHandler
{
    private readonly IValueService _service;

    public MultiplyBHandler(IValueService service)
    {
        _service = service;
    }

    public override double Handle(double currentValue)
    {
        double factor = _service.GetValue("FactorB");
        double newValue = currentValue * factor;

        Console.WriteLine($"MultiplyBHandler: {currentValue} * {factor} = {newValue}");

        return _next?.Handle(newValue) ?? newValue;
    }
}

