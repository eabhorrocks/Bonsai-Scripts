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
public class elementAtTest
{
    public int indexValue { get; set; }
    public IObservable<Vector3> Process(IObservable<List<Vector3>> source)
    {
        return source.Select(value => 
        {
            Vector3 result = value[indexValue];
            
            return result;
        });
        
    }
}
