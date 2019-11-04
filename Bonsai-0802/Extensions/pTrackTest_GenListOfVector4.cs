using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenTK;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Source)]
public class pTrackTest_GenListOfVector4
{
    public IObservable<List<Vector4>> Process()
    {

         
        
        List<Vector4> pTracking = new List<Vector4>();
        Vector4 p1 = new Vector4(10,  10,  10,  7);
        Vector4 p2 = new Vector4( 10,  8,  10,  5);
        Vector4 p3 = new Vector4( 10,  6,  10,  5);
        Vector4 p4 = new Vector4( 10,  7,  10,  4);


        pTracking.Add(p1);
        pTracking.Add(p2);
        pTracking.Add(p3);
        pTracking.Add(p4);


        return Observable.Return(pTracking);
    }
}
