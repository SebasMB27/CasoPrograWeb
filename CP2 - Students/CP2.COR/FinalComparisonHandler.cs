namespace CP2.COR;

public class FinalComparisonHandler : MultiplyHandler
{
    private readonly IValueService _service;

    public FinalComparisonHandler(IValueService service)
    {
        _service = service;
    }

    public override double Handle(double currentValue)
    {
        double expected = _service.GetValue("Expected");

        Console.WriteLine($"Comparing Final Result: {currentValue} == {expected}");

        if (currentValue == expected)
            Console.WriteLine("✅ RESULT: Correct final value!");
        else
            Console.WriteLine("❌ RESULT: Incorrect final value.");

        return currentValue;
    }
}

