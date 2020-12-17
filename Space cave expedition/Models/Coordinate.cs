using System;
using System.Collections.Generic;
using System.Text;

namespace Space_cave_expedition.Models
{
    /// <summary>
    /// Represents a single coordinate on a field.
    /// </summary>
    public class Coordinate
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Coordinate(int xPosition, int yPosition, char content)
        {
            X = xPosition;
            Y = yPosition;
            Content = content;
        }
        public char Content { get; private set; }
    }
}
