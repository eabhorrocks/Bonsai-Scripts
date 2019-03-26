using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

public class SpeedDiscParameters
{
    public float? Duration { get; set; }
    public float? Size { get; set; }
    public float? Colour1 { get; set; }
    public float? Colour2 { get; set; }
    public float? numDots1 { get; set; }
    public float? numDots2 { get; set; }
    public int? dotLifeBool { get; set; }
    public int? dotLifetime { get; set; }

    public float? Contrast { get; set; }
    public float? VelocityXLeft { get; set; }
    public float? VelocityYLeft { get; set; }
    public float? CoherenceLeft { get; set; }
    public float? VelocityXRight { get; set; }
    public float? VelocityYRight { get; set; }
    public float? CoherenceRight { get; set; }

}

[Combinator]
[Description("Creates a sequence of dot motion parameters used for stimulus presentation.")]
[WorkflowElementCategory(ElementCategory.Source)]
public class SpeedDiscSpecification
{
    private List<SpeedDiscParameters> trials = new List<SpeedDiscParameters>();

    public List<SpeedDiscParameters> Trials
    {
        get { return trials;}
    }
    
    public IObservable<SpeedDiscParameters> Process()
    {
        return trials.ToObservable();
    }

    public IObservable<SpeedDiscParameters> Process<TSource>(IObservable<TSource> source)
    {
        return source.SelectMany(input => trials);
    }
}
