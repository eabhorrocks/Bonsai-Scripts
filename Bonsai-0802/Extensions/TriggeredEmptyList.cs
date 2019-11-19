using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class TriggeredEmptyList
{
    public IObservable<List<int>> Process(IObservable<Unit> source)
    {
        return source.Select(value =>
        {
            List<int> emptyList = new List<int>();
            return emptyList;
        });
    }
}
