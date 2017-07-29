using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Snake : Form
    {
        const int BITMAP_HEIGHT = 600;
        const int BITMAP_WIDTH = 600;

        const int SNAKE_POINT_HEIGHT = 20;
        const int SNAKE_POINT_WIDTH = 20;

        const int SNAKE_BORDER_WIDTH = 1;
        const int SNAKE_BORDER_HEIGHT = 1;

        const int VERTICAL_NUMBER_OF_POINTS_IN_BOARD = 30; // BITMAP_HEIGHT / SNAKE_POINT_HEIGHT
        const int HORIZONAL_NUMBER_OF_POINTS_IN_BOARD = 30; // BITMAP_WIDTH / SNAKE_POINT_WIDTH

        private Bitmap bmp;

        private GameStatus gameStatus = GameStatus.WaitingToStart;

        List<KeyEventArgs> keysPressed = new List<KeyEventArgs>(); // list that contain all the arrows that was pressed
        KeyEventArgs lastKeyProccessed = null; // last key that was press and already affect the snake moving
        Point? removedPoint = null; // The point which was removed from nake, so we paint blank spot on it.

        Skeleton skeleton = new Skeleton(HORIZONAL_NUMBER_OF_POINTS_IN_BOARD, VERTICAL_NUMBER_OF_POINTS_IN_BOARD);

        RandomPoint point = new RandomPoint(); // the point to eat
        
        public Snake(int level)
        {
            InitializeComponent();
            ticv.Interval = (level - 1) * 20 + 30;
            bmp = new Bitmap(BITMAP_WIDTH, BITMAP_HEIGHT);

            while (!skeleton.checkPoint(point))
            {
                // TODO: add here code for possible starvation at the end?
                point.random();
            }
        }
                       
        private void Snake_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.P) && (gameStatus == GameStatus.Playing))
            {
                gameStatus = GameStatus.Paused;
                ticv.Enabled = false;
            }
            else if(isKeyAnArrow(e.KeyCode))
            {
                if (gameStatus == GameStatus.WaitingToStart)
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        keysPressed.Add(new KeyEventArgs(Keys.Down));
                    }
                    else
                    {
                        keysPressed.Add(new KeyEventArgs(Keys.Right));
                    }

                    ticv.Enabled = true;
                    gameStatus = GameStatus.Playing;
                }
                else
                {
                    if (gameStatus == GameStatus.Paused)
                    {
                        gameStatus = GameStatus.Playing; // Unpause the game
                        ticv.Enabled = true;
                    }
                    // Add only if key is different than last key and also is not in opposite direction.
                    if ((lastKeyProccessed == null || e.KeyCode != lastKeyProccessed.KeyCode) &&
                        (keysPressed.Count == 0 || ((e.KeyCode != keysPressed[keysPressed.Count - 1].KeyCode) &&
                        !DoesKeysAreAtOppositeDirections(keysPressed[keysPressed.Count - 1].KeyCode, e.KeyCode))))
                    {
                        keysPressed.Add(e);
                    }
                }
            }
        }

        private void ticv_Tick(object sender, EventArgs e)
        {
            if (gameStatus != GameStatus.Playing) // Move snake only if we are playing.
                return;
            
            RemoveEyes(skeleton.Snake[0].X, skeleton.Snake[0].Y);

            if (keysPressed.Count > 0)
            {
                KeyEventArgs  keyPressed = keysPressed[0];
                keysPressed.RemoveAt(0);
                removedPoint = skeleton.moveByKey(keyPressed.KeyCode);
                lastKeyProccessed = keyPressed;
            }
            else
            {
                removedPoint = skeleton.moveAutomatically();
            }

            if (skeleton.DidSnakeCompleted())
            {
                gameStatus = GameStatus.Won;
                MessageBox.Show("you won!");
                ticv.Enabled = false;
                // high scores
                return;
            }
            else if (skeleton.DidSnakeCrash())
            {
                gameStatus = GameStatus.GameOver;
                ticv.Enabled = false;
                MessageBox.Show("game over!");
                this.Close();
                return;
            }
            else if (skeleton.checkEat(point))
            {
                skeleton.DidSnakeJustEaten = true;
                while (skeleton.checkPoint(point) == false)
                    point.random();
            }

            this.Snake_Paint(this, new PaintEventArgs(this.CreateGraphics(), new Rectangle(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height)));           
        }

        private bool isKeyAnArrow(Keys keyCode)
        {
            return keyCode == Keys.Right || keyCode == Keys.Left || keyCode == Keys.Up || keyCode == Keys.Down;
        }

        private bool DoesKeysAreAtOppositeDirections(Keys keyCode1, Keys keyCode2)
        {
            return (keyCode1 == Keys.Down && keyCode2 == Keys.Up) || (keyCode1 == Keys.Up && keyCode2 == Keys.Down) ||
                (keyCode1 == Keys.Left && keyCode2 == Keys.Right) || (keyCode1 == Keys.Right && keyCode2 == Keys.Left);
        }

        private void Snake_Load(object sender, EventArgs e)
        {
            int i, j, k;

            for (i = 0; i < BITMAP_WIDTH; i++)       
                for (j = 0; j < BITMAP_HEIGHT; j++)              
                    bmp.SetPixel(i, j, Color.White);

            //painting snake
            for (k = 0; k < skeleton.Snake.Count; k++)
            {
                for (i = skeleton.Snake[k].X * SNAKE_POINT_WIDTH; i < (skeleton.Snake[k].X + 1) * SNAKE_POINT_WIDTH; i++)
                {
                    for (j = skeleton.Snake[k].Y * SNAKE_POINT_HEIGHT; j < (skeleton.Snake[k].Y + 1) * SNAKE_POINT_HEIGHT; j++)
                    {
                        if ((i % SNAKE_POINT_WIDTH == 0) || (j % SNAKE_POINT_HEIGHT == 0) ||
                            (i == ((skeleton.Snake[0].X + 1) * SNAKE_POINT_WIDTH - SNAKE_BORDER_WIDTH)) || (j == ((skeleton.Snake[0].Y + 1) * SNAKE_POINT_HEIGHT - SNAKE_BORDER_HEIGHT)))
                            bmp.SetPixel(i, j, Color.Black);
                        else
                            bmp.SetPixel(i, j, Color.Red);
                    }
                }
            }

            paintEyes(skeleton.Snake[0].X, skeleton.Snake[0].Y);
        }

        private void Snake_Paint(object sender, PaintEventArgs e)
        {
            int i, j;
            try
            {
                Graphics gScreen = e.Graphics;

                if (removedPoint.HasValue)
                {
                    for (i = removedPoint.Value.X * SNAKE_POINT_WIDTH; i < (removedPoint.Value.X + 1) * SNAKE_POINT_WIDTH + 20; i++)
                    {
                        for (j = removedPoint.Value.Y * SNAKE_POINT_HEIGHT; j < (removedPoint.Value.Y + 1) * SNAKE_POINT_HEIGHT; j++)
                        {
                            bmp.SetPixel(i, j, Color.White);
                        }
                    }
                }

                // Painting snake head
                for (i = skeleton.Snake[0].X * SNAKE_POINT_WIDTH; i < (skeleton.Snake[0].X + 1) * SNAKE_POINT_WIDTH; i++)
                {
                    for (j = skeleton.Snake[0].Y * SNAKE_POINT_HEIGHT; j < (skeleton.Snake[0].Y + 1) * SNAKE_POINT_HEIGHT; j++)
                    {
                        if ((i % SNAKE_POINT_WIDTH == 0) || (j % SNAKE_POINT_HEIGHT == 0) ||
                            (i == ((skeleton.Snake[0].X + 1) * SNAKE_POINT_WIDTH - SNAKE_BORDER_WIDTH)) || (j == ((skeleton.Snake[0].Y + 1) * SNAKE_POINT_HEIGHT - SNAKE_BORDER_HEIGHT)))
                            bmp.SetPixel(i, j, Color.Black);
                        else
                            bmp.SetPixel(i, j, Color.Red);
                    }
                }

                //painting point
                for (i = point.X * 20; i < point.X * 20 + 20; i++)
                {
                    for (j = point.Y * 20; j < point.Y * 20 + 20; j++)
                    {
                        bmp.SetPixel(i, j, Color.Red);
                    }
                }

                paintEyes(skeleton.Snake[0].X, skeleton.Snake[0].Y);

                gScreen.DrawImage(bmp, 10, 20, bmp.Width, bmp.Height);
            }
            catch (Exception e1)
            {
            }
        }


        private void paintEyes(int HeadX, int HeadY)
        {
            paintEyesWithColor(HeadX, HeadY, Color.BlueViolet);
        }

        private void RemoveEyes(int HeadX, int HeadY)
        {
            paintEyesWithColor(HeadX, HeadY, Color.Red);
        }

        private void paintEyesWithColor(int HeadX, int HeadY, Color color)
        {
            if ((HeadX < 30) && (HeadX >= 0) && (HeadY < 30) && (HeadY >= 0))
            {
                if (skeleton.Snake[0].Y == skeleton.Snake[1].Y && skeleton.Snake[0].X > skeleton.Snake[1].X) // Moving right
                {
                    // first eye
                    bmp.SetPixel(HeadX * 20 + 12, HeadY * 20 + 5, color);
                    bmp.SetPixel(HeadX * 20 + 13, HeadY * 20 + 5, color);
                    bmp.SetPixel(HeadX * 20 + 12, HeadY * 20 + 6, color);
                    bmp.SetPixel(HeadX * 20 + 13, HeadY * 20 + 6, color);

                    // second eye
                    bmp.SetPixel(HeadX * 20 + 12, HeadY * 20 + 15, color);
                    bmp.SetPixel(HeadX * 20 + 13, HeadY * 20 + 15, color);
                    bmp.SetPixel(HeadX * 20 + 12, HeadY * 20 + 14, color);
                    bmp.SetPixel(HeadX * 20 + 13, HeadY * 20 + 14, color);
                }
                else if (skeleton.Snake[0].Y == skeleton.Snake[1].Y && skeleton.Snake[0].X < skeleton.Snake[1].X) // Moving left
                {
                    // first eye
                    bmp.SetPixel(HeadX * 20 + 6, HeadY * 20 + 5, color);
                    bmp.SetPixel(HeadX * 20 + 7, HeadY * 20 + 5, color);
                    bmp.SetPixel(HeadX * 20 + 6, HeadY * 20 + 6, color);
                    bmp.SetPixel(HeadX * 20 + 7, HeadY * 20 + 6, color);

                    // second eye
                    bmp.SetPixel(HeadX * 20 + 6, HeadY * 20 + 15, color);
                    bmp.SetPixel(HeadX * 20 + 7, HeadY * 20 + 15, color);
                    bmp.SetPixel(HeadX * 20 + 6, HeadY * 20 + 14, color);
                    bmp.SetPixel(HeadX * 20 + 7, HeadY * 20 + 14, color);
                }
                else if (skeleton.Snake[0].X == skeleton.Snake[1].X && skeleton.Snake[0].Y < skeleton.Snake[1].Y) // Moving up
                {
                    // first eye
                    bmp.SetPixel(HeadX * 20 + 5, HeadY * 20 + 6, color);
                    bmp.SetPixel(HeadX * 20 + 5, HeadY * 20 + 7, color);
                    bmp.SetPixel(HeadX * 20 + 6, HeadY * 20 + 6, color);
                    bmp.SetPixel(HeadX * 20 + 6, HeadY * 20 + 7, color);

                    // second eye
                    bmp.SetPixel(HeadX * 20 + 15, HeadY * 20 + 6, color);
                    bmp.SetPixel(HeadX * 20 + 15, HeadY * 20 + 7, color);
                    bmp.SetPixel(HeadX * 20 + 14, HeadY * 20 + 6, color);
                    bmp.SetPixel(HeadX * 20 + 14, HeadY * 20 + 7, color);
                }
                else if (skeleton.Snake[0].X == skeleton.Snake[1].X && skeleton.Snake[0].Y > skeleton.Snake[1].Y) // Moving down
                {
                    // first eye
                    bmp.SetPixel(HeadX * 20 + 5, HeadY * 20 + 14, color);
                    bmp.SetPixel(HeadX * 20 + 5, HeadY * 20 + 15, color);
                    bmp.SetPixel(HeadX * 20 + 6, HeadY * 20 + 14, color);
                    bmp.SetPixel(HeadX * 20 + 6, HeadY * 20 + 15, color);

                    // second eye
                    bmp.SetPixel(HeadX * 20 + 15, HeadY * 20 + 14, color);
                    bmp.SetPixel(HeadX * 20 + 15, HeadY * 20 + 15, color);
                    bmp.SetPixel(HeadX * 20 + 14, HeadY * 20 + 14, color);
                    bmp.SetPixel(HeadX * 20 + 14, HeadY * 20 + 15, color);
                }
            }
        }
    }
        
}
