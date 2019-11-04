using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class genRewardString
{
    public IObservable<string> Process(IObservable<Tuple<string, float>> source)
    {
        return source.Select(value =>
        {
            string output = value.Item1+value.Item2.ToString();
            return output;
        });
    }
}
