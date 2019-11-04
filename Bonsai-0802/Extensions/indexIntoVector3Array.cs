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
public class indexIntoVector3Array
{
    public int indexValue {get; set;}
    public IObservable<Vector3> Process(IObservable<Vector3[]> source)
    {
        return source.Select(value => 
        {
            Vector3 result = value[indexValue];
            return result;
        });
    }
}
