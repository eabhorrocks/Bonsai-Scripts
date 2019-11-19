using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Source)]
public class InitialiseEmptyIntList
{
    public IObservable<List<int>> Process()
    {
        List<int> emptyList = new List<int>();
        return Observable.Return(emptyList);
    }
}
