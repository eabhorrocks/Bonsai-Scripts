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
public class moveDots_expansion
{
        public float Velocity {get; set; }
        public float dir { get; set; }
        
        public int Coherence { get ; set; }
        public float left { get; set; }
        public float right { get; set;}
        public float top { get; set; }
        public float bottom { get; set; }



    public IObservable<Vector2[]> Process(IObservable<Vector2[]> source)
    {
        return source.Select(value => 
        {
            Random rng = new Random(25);
            var nDots = value.Length;
            int nCoh = nDots/100 * Coherence;
            var result = value;
            //double vel = Math.Sqrt(VelocityX*VelocityX + VelocityY*VelocityY);
            float[] borders = { left, right, top, bottom };

        // Coherent dots
            for (int i = 0; i < nCoh; i++)
        {
        float dx = (result[i].X - 0) * (float)Math.Sqrt((result[i].X * result[i].X + result[i].Y*result[i].Y)) * Velocity;
        float dy = (result[i].Y - 0) * (float)Math.Sqrt((result[i].X * result[i].X + result[i].Y*result[i].Y)) * Velocity;
        result[i].X = (float)(result[i].X + dx*dir);
        result[i].Y = (float)(result[i].Y + dy*dir);

       }
        // Incoherent Dots
        for (int i = nCoh; i < nDots; i++)
        {

            float dx = (result[i].X - 0) * (float)Math.Sqrt((result[i].X * result[i].X + result[i].Y*result[i].Y)) * Velocity;
            float dy = (result[i].Y - 0) * (float)Math.Sqrt((result[i].X * result[i].X + result[i].Y*result[i].Y)) * Velocity;

            float vel = (float)Math.Sqrt(dx*dx + dy*dy);

            Vector2 randomVector = new Vector2(rng.Next(0, 100) - 50, rng.Next(0,100)-50);
            randomVector = Vector2.Normalize(randomVector) * vel;

            float rNum = rng.Next(0, 100);
            rNum = rNum/100;
            double rNum2 = Math.Sqrt(1 - rNum*rNum);
            int rdir = rng.Next(0, 2) * 2 - 1;
            int rdir2 = rng.Next(0, 2) * 2 - 1;
        
            result[i].X = (float)(result[i].X + randomVector[0]);
            result[i].Y = (float)(result[i].Y + randomVector[1]);
       }

       wrapAroundExpansion(result, nDots, borders);

            return result;
        });

        
        
    }
        private void wrapAroundExpansion(Vector2[] result, int nDots, float[] borders)
    {
        var random = new Random((int)DateTime.Now.Ticks);

        for (int i=0; i < nDots; i++)
        {
        if(result[i].X > borders[1] || result[i].X < borders[0] || result[i].Y > borders[2]+1 || result[i].Y < borders[3])
        {
            result[i].X = (float)(random.NextDouble() * 2 - 1) * Math.Abs(right - left) /2f + 5;
            result[i].Y = (float)(random.NextDouble() * 2 - 1) * Math.Abs(top - bottom) /2f + 5;
        }

        if(Math.Abs(result[i].X) < 10 && Math.Abs(result[i].Y) < 10)
        {
            result[i].X = (float)(random.NextDouble() * 2 - 1) * Math.Abs(right - left) /2f + 5;
            result[i].Y = (float)(random.NextDouble() * 2 - 1) * Math.Abs(top - bottom) /2f + 5;
        }
    }
    }
}
