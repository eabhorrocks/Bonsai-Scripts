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
public class genVector3ListRand
{
    public int nDots {get; set; }

    public IObservable<List<Vector3>> Process()
    {
        var random = new Random((int)DateTime.Now.Ticks);
        List<Vector3> DotPoses = new List<Vector3>();
        for (int i = 0; i < nDots; i++)
        {
            Vector3 pos = new Vector3();
            pos.X = (float)(random.NextDouble() * 2 - 1) /2f;
            pos.Y = (float)(random.NextDouble() * 2 - 1) /2f;
            pos.Z = (float)(random.NextDouble() * 2 - 1) /2f;
            DotPoses.Add(pos);
        }

        return Observable.Return(DotPoses);
    }
}
