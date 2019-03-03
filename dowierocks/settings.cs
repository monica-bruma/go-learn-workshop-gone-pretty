using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dowierocks
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
    class Settings
    {
        public static bool GameOver { get; set; }
        public static int Score { get; set; }
        public static int Height { get; set; }
        public static int Width  { get; set; }
    
        public static int Speed { get; set; }
        public static Direction Direction { get; set; }
        public static int Points { get; set; }
        public Settings()
        {
            GameOver = false;
            Score = 0;
            Speed = 10;
            Direction = Direction.Down;
            Height = 16;
            Width = 16;
            Points = 100;
        }
}

}
