namespace Rogal.Components.Base
{
    public class Transform
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int PreviousX { get; private set; }
        public int PreviousY { get; private set; }
        public int Speed { get; }
        private int frameCounter;

        public Transform(int x, int y, int speed)
        {
            X = PreviousX = x;
            Y = PreviousY = y;
            Speed = speed;
            frameCounter = 0;
        }

        public bool CanMove()
        {
            return frameCounter >= Speed;
        }

        public void Update(int newX, int newY)
        {
            if (CanMove())
            {
                PreviousX = X;
                PreviousY = Y;
                X = newX;
                Y = newY;
                frameCounter = 0;
            }
        }

        public void IncreaseFrameCounter()
        {
            frameCounter++;
        }
    }
}

