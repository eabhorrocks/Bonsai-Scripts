using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class getOpenTime
{
    public IObservable<float> Process(IObservable<Tuple<Tuple<float[], float[]>, float>> source)
    {
        return source.Select(value => 
        {
            float[] rewardAmounts = value.Item1.Item1;
            float[] openTimes = value.Item1.Item2;
            float desiredReward = value.Item2;
            
            int index = Array.IndexOf(rewardAmounts, desiredReward);
            float openTime = openTimes[index];
            return openTime;
        });
    }
}
