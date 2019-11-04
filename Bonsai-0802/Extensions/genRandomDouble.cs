using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Source)]
public class genRandomDouble
{
    public IObservable<Double> Process()
    {
        Random rng = new Random();
        Double output = rng.NextDouble();
        return Observable.Return(output);
    }
}
