using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class genRandDouble
{
    public IObservable<double> Process(IObservable<float> source)
    {
        return source.Select(value => 
        {
            Random rng = new Random();
            Double output = rng.NextDouble();
            return output;
        }
        );
    }
}
