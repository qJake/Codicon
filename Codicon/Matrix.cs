using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Codicon
{
    public class Matrix
    {
        public int Height => Definition.Length;
        public int Width => Definition[0].Length;

        private string[] Definition { get; }

        public Matrix(params string[] data)
        {
            Definition = data;
        }

        public bool HasCharacter(char c) => Definition.Any(s => s.Contains(c));

        public int CharacterCount(char c) => Definition.Sum(s => s.Count(sc => sc == c));

        public Point GetSingleCharacterPosition(char c)
        {
            for (var i = 0; i < Definition.Length; i++)
            {
                for (var j = 0; j < Definition[i].Length; j++)
                {
                    if (Definition[i][j] == c)
                    {
                        return new Point(i, j);
                    }
                }
            }
            throw new Exception("Character not found in matrix.");
        }
        public List<Point> GetCharacterPositions(char c, int limit = int.MaxValue)
        {
            var points = new List<Point>();
            for (var i = 0; i < Definition.Length; i++)
            {
                for (var j = 0; j < Definition[i].Length; j++)
                {
                    if (Definition[i][j] == c)
                    {
                        if (points.Count < 2)
                        {
                            points.Add(new Point(i, j));
                        }
                    }
                }
            }
            return points.Take(limit).ToList();
        }
    }
}
