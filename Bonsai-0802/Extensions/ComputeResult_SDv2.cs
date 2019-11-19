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
public class ComputeResult_SDv2
{    
    public List<List<int[]>> pTrack {get; set; }
    //<Tuple<Tuple<double, double>, Tuple<float, int, int, int>, float>
    
        public IObservable<Tuple<List<List<int[]>>,int>> Process(IObservable<Tuple<Tuple<double, double>, Tuple<int, int, int, int>, float>> source)
    {
        return source.Select(value => 
        {
            //left speed, right speed, mean speed, ratioIndex, left/right -1/1, nRatios
            Tuple<double, double> speeds = value.Item1;
            Tuple<int, int, int, int> otherInfo = value.Item2;
            float response = value.Item3;

            int meanSpeedIndex = otherInfo.Item1;
            int ratioIndex = otherInfo.Item2;
            int rightFaster = otherInfo.Item3;
            int nRatios = otherInfo.Item4;
            

            //Console.WriteLine("response: " + response);
            // if mouse responded, increment n trials for left/right at that ratio.
            // if mouse was also correct, increment total correct trials for left/right at that ratio
            int trialOutcome = 3; // assume no response by default
            if (response != 0)
            {
                if (rightFaster==1) //right
                {   trialOutcome = 0; // assume incorrect
                    pTrack[meanSpeedIndex][ratioIndex][0]++;
                    if (response == 1) // response was right
                    {
                        pTrack[meanSpeedIndex][ratioIndex][1]++;
                        trialOutcome = 2; // correct right response
                    }
                }
                else if (rightFaster==-1) //left
                {   trialOutcome = 0; // assume incorrect
                   pTrack[meanSpeedIndex][ratioIndex][2]++;
                    if (response == -1)
                    {
                    pTrack[meanSpeedIndex][ratioIndex][3]++;
                    trialOutcome = 1; //correct left response
                    }
                }
            }

            //Console.WriteLine(pTrack[meanSpeedIndex][ratioIndex][0]);
            //Console.WriteLine(pTrack[meanSpeedIndex][ratioIndex][1]);
            //Console.WriteLine(pTrack[meanSpeedIndex][ratioIndex][2]);
            //Console.WriteLine(pTrack[meanSpeedIndex][ratioIndex][3]);

            // trial outcomes (int)
            // 0: incorrect, 1: left correct, 2: right correct, 3: no response
            var result = new Tuple<List<List<int[]>>,int>(pTrack,trialOutcome);
            return result;

        });
    }
}
