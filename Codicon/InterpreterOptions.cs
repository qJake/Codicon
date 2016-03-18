using System.Drawing;

namespace Codicon
{
    public class InterpreterOptions
    {
        public static InterpreterOptions Default => new InterpreterOptions
        {
            Scale = 1.0,
            Foreground = Color.Black,
            Background = Color.Transparent
        };

        public double Scale { get; set; }
        public Color Foreground { get; set; }
        public Color Background { get; set; }
    }
}
