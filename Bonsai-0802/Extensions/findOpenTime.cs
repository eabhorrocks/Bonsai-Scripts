using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class findOpenTime
{
    public IObservable<Tuple<float[], float[]>> Process(IObservable<Tuple<float[], float[]>> source)
    {
        return source.Select(value => value);
    }
}
