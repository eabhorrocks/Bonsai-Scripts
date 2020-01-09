using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

public class MC3DBasicParameters
{
    public float? Duration { get; set; }
    public float? Colour1 { get; set; }
    public float? Colour2 { get; set; }
    public float? numDots1 { get; set; }
    public float? numDots2 { get; set; }
    public int? dotLifeBool { get; set; }
    public int? dotLifetime { get; set; }

    public double? Contrast { get; set; }
    public double? Coherence { get; set; }
    public double? VelX { get; set; }
    public double? VelY { get; set; }
    public double? VelZ { get; set; }

}

[Combinator]
[Description("Creates a sequence of dot motion parameters used for stimulus presentation.")]
[WorkflowElementCategory(ElementCategory.Source)]
public class MC3DBasicSpec
{
    private List<MC3DBasicParameters> trials = new List<MC3DBasicParameters>();

    public List<MC3DBasicParameters> Trials
    {
        get { return trials;}
    }
    
    public IObservable<MC3DBasicParameters> Process()
    {
        return trials.ToObservable();
    }

    public IObservable<MC3DBasicParameters> Process<TSource>(IObservable<TSource> source)
    {
        return source.SelectMany(input => trials);
    }
}
