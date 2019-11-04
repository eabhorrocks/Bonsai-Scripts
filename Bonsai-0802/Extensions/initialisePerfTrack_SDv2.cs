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
public class initialisePerfTrack_SDv2
{
    public float meanSpeed {get; set; }
    public List<float> Ratios {get; set; }  
    public List<float> RatioProbs {get; set; }  
    public IObservable<Tuple<List<float>, List<float>, List<int[]>, float>> Process()
    {

        int nRatios = Ratios.Count();

        List<int[]> pTracking = new List<int[]>();

        for (int i=0; i<nRatios; i++)
        {
            int[] p1 = new int[4] {0,  0,  0,  0};
            pTracking.Add(p1);

        }


        var output = Tuple.Create(Ratios, RatioProbs, pTracking, meanSpeed);
        return Observable.Return(output);
    }
}
