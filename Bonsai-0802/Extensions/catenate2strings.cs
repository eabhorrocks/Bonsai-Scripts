using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Combinator)]
public class catenate2strings
{
    public IObservable<Tuple<string, string>> Process(IObservable<Tuple<string, string>> source)
    {
        return source;
    }
}
