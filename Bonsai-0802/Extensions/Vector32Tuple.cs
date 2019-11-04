using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenTK;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class Vector32Tuple
{
    public IObservable<Tuple<List<Vector3>, int>> Process(IObservable<Tuple<List<Vector3>, int>> source)
    {
        return source.Select(value => value);
    }
}
