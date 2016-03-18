using System;
using System.Drawing;
using System.Linq;
using Codicon.Utility;

namespace Codicon.Bitmaps
{
    class BitmapMatrixInterpreter : IMatrixInterpreter<Bitmap>
    {
        private Pen Pen { get; set; }

        public Bitmap Process(Matrix matrix)
        {
            return Process(matrix, InterpreterOptions.Default);
        }

        public Bitmap Process(Matrix matrix, InterpreterOptions options)
        {
            int height = (int)(matrix.Height * options.Scale);
            int width = (int)(matrix.Width * options.Scale);

            // Set up the pen
            Pen = new Pen(options.Foreground)
            {
                Width = (float)options.Scale,
                Brush = new SolidBrush(options.Background)
            };

            var bitmap = new Bitmap(height, width);

            var g = Graphics.FromImage(bitmap);

            var matrixReader = new MatrixReader(matrix);
            while (matrixReader.MoveNext())
            {
                if (matrixReader.PeekCharacter() == matrixReader.NextSequentialCharacter)
                {
                    DrawPolygon(matrix, g, matrixReader.CurrentChar);
                }
                else
                {
                    DrawShape(matrix, g, matrixReader.CurrentChar);
                }
            }
            g.Flush();

            return bitmap;
        }

        private void DrawShape(Matrix m, Graphics g, char c)
        {
            switch (m.CharacterCount(c))
            {
                case 1:
                    DrawPixel(m, g, c);
                    break;
                case 2:
                    DrawLine(m, g, c);
                    break;
                case 4:
                    DrawSquare(m, g, c);
                    break;
            }
        }

        private void DrawPixel(Matrix m, Graphics g, char c)
        {
            var point = m.GetSingleCharacterPosition(c);

            g.DrawEllipse(Pen, new Rectangle(point.X, point.Y, 1, 1));
        }

        private void DrawLine(Matrix m, Graphics g, char c)
        {
            var points = m.GetCharacterPositions(c, 2);
            if (points.Count < 2)
            {
                throw new Exception($"Expected 2 characters, but got {points.Count} instead.");
            }

            g.DrawLine(Pen, points[0], points[1]);
        }

        private void DrawSquare(Matrix m, Graphics g, char c)
        {
            var points = m.GetCharacterPositions(c, 4);
            if (points.Count < 2)
            {
                throw new Exception($"Expected 4 characters, but got {points.Count} instead.");
            }

            g.DrawRectangle(Pen, new Rectangle(points.Min(p => p.X),
                                               points.Min(p => p.Y),
                                               points.Max(p => p.X) - points.Min(p => p.X),
                                               points.Max(p => p.Y) - points.Min(p => p.Y)));
        }

        private void DrawPolygon(Matrix m, Graphics g, char c)
        {
            var points = m.GetCharacterPositions(c);
            
            g.DrawPolygon(Pen, points.ToArray());
        }
    }
}
