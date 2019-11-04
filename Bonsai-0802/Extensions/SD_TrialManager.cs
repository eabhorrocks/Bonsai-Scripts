using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class SD_TrialManager
{
    public List<float> meanSpeedList {get; set; }
    public List<List<float>> RatiosList {get; set; }  
    public List<List<float>> RatioProbsList {get; set; }  
    public List<List<int[]>> pTrackList {get; set; } 
    public List<float> meanSpeedProbs {get; set; }
    public bool autoBias {get; set; }
    public float biasScaling {get; set; }
    public float pRightManual {get; set; }
    public float minPRight {get; set; }
    public IObservable<Tuple<Tuple<double, double>,Tuple<int, int, int, int>>> Process(IObservable<Unit> source)
    {
        return source.Select(value => 
        {

            ////////// INTIALISE VARIABLES //////////

            Random rng = new Random((int)DateTime.Now.Ticks); // random number generator
            float speedRatio = 0f; // output needs to be initialised for script to compile
            float bias = 0f;
            float totalNorm = 0f;
            float pRight = 0.5f;
            int ratioIndexToTest = 99;
            int nSpeeds = meanSpeedList.Count();
            int meanSpeedIndexToTest = 99;
            int nRatios = 99;
            float meanSpeed = 0f;
            int rightFaster = 1;


            //////////// PICK MEAN SPEED TO TEST //////////

            int randomMeanSpeed = rng.Next(101); // random number between 1 and 100
            for (int i=0; i<nSpeeds; i++) // loop through ratios until one is picked
            {
                float sum = meanSpeedProbs.Take(i+1).Sum(); // take cumulative sum of meanSpeed probs up to this one
                if (randomMeanSpeed <= sum) // if the sum is more than the random number, choose it
                {
                    meanSpeedIndexToTest = i; // index of the mean speed to be tested
                    nRatios = RatiosList[meanSpeedIndexToTest].Count(); // number of possible speed ratios for this speed
                    meanSpeed = meanSpeedList[meanSpeedIndexToTest];
                    break;
                }
            }


            ////////// PICK A SPEED RATIO //////////

            int randomRatio = rng.Next(101); // random number between 1 and 100
            for (int i=0; i<nRatios; i++) // loop through ratios until one is picked
            {
                float sum = RatioProbsList[meanSpeedIndexToTest].Take(i+1).Sum(); // take cumulative sum of ratio probs up to this one
                if (randomRatio <= sum) // if the sum is more than the random number, choose it
                {
                    ratioIndexToTest = i; 
                    break;
                }
            }
            ////////// BIAS CORRECTION //////////

            pRight = pRightManual; // assign manual by default, override if using autbias
            if (autoBias)
            {
            for (int i=0; i<nRatios; i++)
            {   // here 1st [] indexes into perf track list for the mean speed, [i] is ratio index, [0:3] are the int array indexes
                float nr = pTrackList[meanSpeedIndexToTest][i][0]; // # right trials
                float cr = pTrackList[meanSpeedIndexToTest][i][1]; // # correct right trials
                float nl = pTrackList[meanSpeedIndexToTest][i][2]; // # left trials
                float cl = pTrackList[meanSpeedIndexToTest][i][3]; // # correct left trials

                // adjustment for this ratio is diff in proportion correct, weighted by speed ratio, weighted by total trials for that ratio.
                bias = bias + (((float)(cr/nr) - (float)(cl/nl)) * RatiosList[meanSpeedIndexToTest][i] * (nl+nr));
                totalNorm = totalNorm + (nl+nr)*RatiosList[meanSpeedIndexToTest][i];
            }
            bias = bias / totalNorm; // bias is normalised to be between 0 and 1
            pRight = 0.5f - (bias*biasScaling); // alter p(right) using calculated bias
            // ensure pRight doesn't go outside acceptable range
            if (pRight < minPRight) { pRight = minPRight; }
            if (pRight > 1-minPRight) { pRight = 1-minPRight;}
            } //  end autobias
            if (Single.IsNaN(pRight)) // if first trials/error, revert to manual setting
            {
                pRight = pRightManual;
            }
            
            
            float biasrnd = (float)rng.NextDouble(); // random number for left / right faster
            if (biasrnd <= pRight) // compare to pre-calculated pRight value (probability of right faster)
            {
            rightFaster = 1; //right is faster
            speedRatio = RatiosList[meanSpeedIndexToTest][ratioIndexToTest]; // if right is faster, we use the normal ratio selected
            //Console.WriteLine("output: " + speedRatio);
            }
            else
            {
            rightFaster = -1; // left is faster
            speedRatio = 1f/RatiosList[meanSpeedIndexToTest][ratioIndexToTest]; // if left is faster, we use the inverse ratio (geomean)
            }
            
            // assign outputs:
            // <left speed, right speed>
            // <meanSpeedIndex, ratioIndex, left/right -1/1, nRatios>
            Console.WriteLine("meanSpeeds: " + meanSpeed);
            Console.WriteLine("ratio: " + speedRatio);
            Console.WriteLine("pRight: " + pRight);

            var speeds = new Tuple<double, double>(-1f*meanSpeed/Math.Sqrt(speedRatio), -1f*meanSpeed*Math.Sqrt(speedRatio));
            var otherInfo = new Tuple<int, int, int, int>(meanSpeedIndexToTest, ratioIndexToTest, rightFaster, nRatios);

            var output = Tuple.Create(speeds, otherInfo);
            

            return output;

            
            

        });
    }
}
