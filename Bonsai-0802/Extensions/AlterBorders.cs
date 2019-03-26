using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;



[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class AlterBorders
{
    public float TopAdjust {get; set; }
    public float BottomAdjust {get; set; }
    public float LeftAdjust {get; set; }
    public float RightAdjust {get; set; }
    public IObservable<Tuple<float, float, float, float>> Process(IObservable<Tuple<float, float, float, float>> source)
    {
        return source.Select(value => 
        {        
        float top = value.Item1 + TopAdjust;
        float bottom = value.Item2 + BottomAdjust;
        float left = value.Item3 + LeftAdjust;
        float right = value.Item4 + RightAdjust;

        var res = new Tuple<float, float, float, float>(top, bottom, left, right);

        
        return res;
        }
        );
    }
}
