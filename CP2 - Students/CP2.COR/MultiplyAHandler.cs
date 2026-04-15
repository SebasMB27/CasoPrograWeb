namespace CP2.COR;

public class MultiplyAHandler : MultiplyHandler
{
    private readonly IValueService _service;

    public MultiplyAHandler(IValueService service)
    {
        _service = service;
    }

    public override double Handle(double currentValue)
    {
        double factor = _service.GetValue("FactorA");
        double newValue = currentValue * factor;
        return _next?.Handle(newValue) ?? newValue;
    }
}

