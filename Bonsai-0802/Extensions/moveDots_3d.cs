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
public class moveDots_3d
{
        public float VelocityX {get; set; }
        public float VelocityY { get; set; }
        
        public int Coherence { get ; set; }
        public float left { get; set; }
        public float right { get; set;}
        public float top { get; set; }
        public float bottom { get; set; }



    public IObservable<Vector3[]> Process(IObservable<Vector3[]> source)
    {
        return source.Select(value => 
        {
            Random rng = new Random(25);
            var nDots = value.Length;
            int nCoh = nDots/100 * Coherence;
            var result = value;
            double vel = Math.Sqrt(VelocityX*VelocityX + VelocityY*VelocityY);
            float[] borders = { left, right, top, bottom };

        // Coherent dots
            for (int i = 0; i < nCoh; i++)
        {
            result[i].X = (float)(result[i].X + VelocityX);
            result[i].Y = (float)(result[i].Y + VelocityY);
            result[i].Z = 0;
       }

        // Incoherent Dots
        for (int i = nCoh; i < nDots; i++)
        {
            float rNum = rng.Next(0, 100);
            rNum = rNum/100;
            double rNum2 = Math.Sqrt(1 - rNum*rNum);
            int rdir = rng.Next(0, 2) * 2 - 1;
            int rdir2 = rng.Next(0, 2) * 2 - 1;
        
            result[i].X = (float)(result[i].X + rdir*rNum*vel);
            result[i].Y = (float)(result[i].Y + rdir2*rNum2*vel);
            result[i].Z = 0;
       }

       wrapAround(result, nDots, borders);

            return result;
        });

        
        
    }
        private void wrapAround(Vector3[] dotPos, int nDots, float[] borders)
    {
        for (int i=0; i < nDots; i++)
        {
        if(dotPos[i].X > borders[1]) {dotPos[i].X = borders[0]+0.05f;}
        else if(dotPos[i].X < borders[0]) {dotPos[i].X = borders[1]-0.05f;}
        if(dotPos[i].Y > borders[2]+1){dotPos[i].Y = borders[3]-0.05f;}
        else if(dotPos[i].Y < borders[3]) {dotPos[i].Y = borders[2]+0.05f;}
        }
    }
}
