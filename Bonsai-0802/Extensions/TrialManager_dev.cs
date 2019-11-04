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
public class TrialManager_dev
{
    public float meanSpeed {get; set; }
    public List<float> Ratios {get; set; }  
    public List<float> RatioProbs {get; set; }  
    public List<int[]> pTrack { get; set; } 
    public bool autoBias {get; set; }
    public float biasScaling {get; set;}
    public float pRightManual {get; set; }
                                    //<left speed, right speed>, mean speed, ratioIndex, left/right -1/1, nRatios
    public IObservable<Tuple<Tuple<double, double>,Tuple<float, int, int, int>>> Process(IObservable<int> source)
    {
        return source.Select(value => 
        {

            float speedRatio = 0f; // output needs to be initialised for script to compile
            Random rng = new Random((int)DateTime.Now.Ticks); // random number generator
            int nRatios = Ratios.Count(); // number of speed ratios 
            float bias = 0f;
            float totalNorm = 0f;
            float pRight = 0.5f;
            int ratioIndexToTest = 99;
            // use performance tracking for bias correction adjustment
            if (autoBias)
            {
            for (int i=0; i<nRatios; i++)
            {
                float nr = pTrack[i][0];
                float cr = pTrack[i][1];
                float nl = pTrack[i][2];
                float cl = pTrack[i][3];

                // adjustment for this ratio is diff in proportion correct, weighted by speed ratio, weighted by total trials.
                bias = bias + (((float)(cr/nr) - (float)(cl/nl)) * Ratios[i] * (nl+nr));
                totalNorm = totalNorm + (nl+nr)*Ratios[i];
            }
            bias = bias / totalNorm;
            pRight = 0.5f - (bias*biasScaling); // alter p(right) using calculated bias
            if (pRight < 0.1f) // ensure pRight doesn't go under 0.1
            {
                pRight = 0.1f;
            }
            
            } //  end autobias
            else // assign manual bias
            {
                pRight = pRightManual;
            }

            if (Single.IsNaN(pRight)) // if first trials/error, revert to manual setting
            {
                pRight = pRightManual;
            }

            Console.WriteLine("pright: " + pRight);


            // probabilistically choose a speed ratio to test

            int randomRatio = rng.Next(101); // random number between 1 and 100
            for (int i=0; i<nRatios; i++) // loop through ratios until one is picked
            {
                float sum = RatioProbs.Take(i+1).Sum(); // take cumulative sum of ratio probs up to this one
                if (randomRatio <= sum) // if the sum is more than the random number, choose it
                {
                    ratioIndexToTest = i; 

                    // using bias correction, pick whether ratio should be +ve (right faster) or -ve
                    float biasrnd = (float)rng.NextDouble(); // random number for left / right faster
                    if (biasrnd <= pRight) // compare to pre-calculated pRight value (probability of right faster)
                    {
                        speedRatio = Ratios[ratioIndexToTest]; // if right is faster, we use the normal ratio
                        Console.WriteLine("output: " + speedRatio);
                    }
                    else
                    {
                    speedRatio = -1*Ratios[ratioIndexToTest]; // if left is faster, we use a 'negative' ratio
                    Console.WriteLine("output: " + speedRatio); 
                    }
                    break;
                }
            }
            int rightFaster = 1;
            // if we have -ve speed ratio (i.e. left is faster), we make ratio 1/abs(SpeedRatio) (for geometric mean)
            if (speedRatio<0) 
            { 
                speedRatio = 1f/Math.Abs(speedRatio); // if using a negative ratio, we compute the inverse
                rightFaster = -1;
            }
            
            // assign outputs:
            // <left speed, right speed>
            // meanSpeedIndex, 

            var speeds = new Tuple<double, double>(-1f*meanSpeed/Math.Sqrt(speedRatio), -1f*meanSpeed*Math.Sqrt(speedRatio));
            var otherInfo = new Tuple<float, int, int, int>(meanSpeed, ratioIndexToTest, rightFaster, nRatios);

            var output = Tuple.Create(speeds, otherInfo);
            

            return output;

            
            
            
        });
    }
}
