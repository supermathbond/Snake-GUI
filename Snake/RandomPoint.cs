using System;

namespace Snake
{
    class RandomPoint
    {
        public int X;
        public int Y;

        public RandomPoint()
        {
            Random r1 = new Random();
            Random r2 = new Random();
            this.X = r1.Next(0,29);
            this.Y = r1.Next(0,29);
        }

        public void random()
        {
            Random r1 = new Random();
            Random r2 = new Random();
            this.X = r1.Next(0, 29);
            this.Y = r1.Next(0, 29);     
        }
    }
}
