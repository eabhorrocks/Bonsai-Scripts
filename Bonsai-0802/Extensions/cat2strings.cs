using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class cat2strings
{
    public IObservable<string> Process(IObservable<Tuple<string, string>> source)
    {
        return source.Select(value => 
        {
        string output = value.Item1 + value.Item2;

        return output;
        });
    }
}
