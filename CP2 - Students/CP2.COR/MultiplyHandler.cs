using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP2.COR;

public abstract class MultiplyHandler
{
    protected MultiplyHandler? _next;

    public MultiplyHandler SetNext(MultiplyHandler next)
    {
        _next = next;
        return next;
    }

    public abstract double Handle(double currentValue);
}

