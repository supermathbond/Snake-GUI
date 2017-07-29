using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Snake
{
    class Skeleton
    {
        public List<Point> Snake;
        
        public bool DidSnakeJustEaten { get; set; }

        private int _maxWidth;
        private int _maxHeight;

        /// <summary>
        /// constructor to skeleton, initilize a snake with 5 links
        /// </summary>
        public Skeleton(int maxWidth = 30, int maxHeight = 30)
        {
            _maxHeight = maxHeight;
            _maxWidth = maxWidth;

            Snake = new List<Point>();
            Snake.Add(new Point(4, 0)); // head
            Snake.Add(new Point(3, 0));
            Snake.Add(new Point(2, 0));
            Snake.Add(new Point(1, 0));
            Snake.Add(new Point(0, 0)); // tail
            DidSnakeJustEaten = false;
        }

        public bool DidSnakeCompleted()
        {
            if (Snake.Count == _maxHeight * _maxWidth)
                return true;
            return false;
        }

        /// <summary>
        /// checks if the snake was crash in the wall. or if one link collide with other link. 
        /// </summary>
        /// <returns>true - if was a crash (game over), otherwise - false</returns>
        public bool DidSnakeCrash()
        {
            int i, j;
            for(i = 0; i < Snake.Count; i++)
            {
                if ((Snake[i].X > _maxWidth - 1) || (Snake[i].X < 0) || (Snake[i].Y > _maxHeight - 1) || (Snake[i].Y < 0))
                    return true;
                for (j = 0; j < i; j++)
                {
                    if ((Snake[i].X == Snake[j].X) && (Snake[i].Y == Snake[j].Y))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// checks if the snake "collide" with the point
        /// </summary>
        /// <param name="po">a "point" to eat</param>
        /// <returns>true if we eat. otherwise - false</returns>
        public bool checkEat(RandomPoint po)
        {
            return (Snake[0].X == po.X && Snake[0].Y == po.Y);
        }


        /// <summary>
        /// checks if we didn't put the point on the snake
        /// </summary>
        /// <param name="po">the "point" to eat</param>
        /// <returns>false - if the location is bad, otherwise - true.</returns>
        public bool checkPoint(RandomPoint po)
        {
            int i;
            for (i = 0; i < Snake.Count; i++)
            {
                if ((Snake[i].X == po.X) && (Snake[i].Y == po.Y))
                    return false;
            }
            return true;
        }


        /// <summary>
        /// move the snake automatically. according to the two headers links.
        /// </summary>
        public Point? moveAutomatically()
        {
            Point? removedPoint = null;

            Point tmp = new Point();
            if (Snake[0].X == Snake[1].X)
            {
                tmp.X = Snake[0].X;
                if (Snake[0].Y > Snake[1].Y)
                    tmp.Y = Snake[0].Y + 1;
                if (Snake[0].Y < Snake[1].Y)
                    tmp.Y = Snake[0].Y - 1;
            }
            else if (Snake[0].Y == Snake[1].Y)
            {
                tmp.Y = Snake[0].Y;
                if (Snake[0].X > Snake[1].X)
                    tmp.X = Snake[0].X + 1;
                if (Snake[0].X < Snake[1].X)
                    tmp.X = Snake[0].X - 1;
            }

            Snake.Insert(0, tmp);
            if (DidSnakeJustEaten) 
            {
                DidSnakeJustEaten = false;
            }
            else 
            {
                removedPoint = Snake[Snake.Count - 1];
                Snake.RemoveAt(Snake.Count - 1);
            }

            return removedPoint;
        }


        /// <summary>
        /// move the snake according to the user.
        /// </summary>
        /// <param name="e">contain the key that was pressed.</param>
        /// <returns>The point which was removed from snake.</returns>
        public Point? moveByKey(Keys key)
        {
            Point? removedPoint = null;

            Point tmp = new Point();
            switch (key)
            {
                case Keys.Left:
                    {
                        if (Snake[0].Y == Snake[1].Y)
                            return moveAutomatically();
                        if (Snake[0].X == Snake[1].X)
                        {
                            tmp.Y = Snake[0].Y;
                            tmp.X = Snake[0].X - 1;
                        }
                    }
                    break;
                case Keys.Right:
                    {
                        if (Snake[0].Y == Snake[1].Y)
                            return moveAutomatically();
                        if (Snake[0].X == Snake[1].X)
                        {
                            tmp.Y = Snake[0].Y;
                            tmp.X = Snake[0].X + 1;
                        }
                    }
                    break;
                case Keys.Up:
                    {
                        if (Snake[0].X == Snake[1].X)
                            return moveAutomatically();
                        if (Snake[0].Y == Snake[1].Y)
                        {
                            tmp.X = Snake[0].X;
                            tmp.Y = Snake[0].Y - 1;
                        }
                    }
                    break;
                case Keys.Down:
                    {
                        if (Snake[0].X == Snake[1].X)
                            return moveAutomatically();
                        if (Snake[0].Y == Snake[1].Y)
                        {
                            tmp.X = Snake[0].X;
                            tmp.Y = Snake[0].Y + 1;
                        }
                    }
                    break;
                default:
                    return moveAutomatically();
            }

            Snake.Insert(0, tmp);
            if (!DidSnakeJustEaten)
            {
                removedPoint = Snake[Snake.Count - 1];
                Snake.RemoveAt(Snake.Count - 1);
            }

            return removedPoint;
        }
    }
}
