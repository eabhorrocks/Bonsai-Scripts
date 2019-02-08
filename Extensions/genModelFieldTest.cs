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
public class genModelFieldTest
{
    public int numDots { get; set; }
    public float left { get; set; }
    public float right { get; set;}
    public float top { get; set; }
    public float bottom { get; set; }

    public IObservable<Tuple<float[], float[], float[]>> Process()
    {
        var random = new Random((int)DateTime.Now.Ticks);
        var X = new float[numDots];
        var Y = new float[numDots];
        var Z = new float[numDots];
        for (int i = 0; i < numDots; i++)
        {
            X[i] = (float)(random.NextDouble() * 2 - 1) * Math.Abs(right - left) /2f;
            Y[i] = (float)(random.NextDouble() * 2 - 1) * Math.Abs(top - bottom) /2f;
            Z[i] = -5f;

            
        }

        var result = new Tuple<float[], float[], float[]>(X, Y, Z);
        return Observable.Return(result);
    }

   // public IObservable<Vector2[]> Process<TSource>(IObservable<TSource> source)
   // {
  //      var random = new Random(32);
  //      return source.Select(input =>
     //   {
  //          var result = new Vector2[numDots];
  //          for (int i = 0; i < result.Length; i++)
   //         {
   //             result[i].X = (float)(random.NextDouble() * 2 - 1) * Scale;
    //            result[i].Y = (float)(random.NextDouble() * 2 - 1) * Scale;
   //         }
        //    return result;
   //     });
 //   }
}
