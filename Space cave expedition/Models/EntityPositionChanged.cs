using System;

namespace Space_cave_expedition.Models
{
    public delegate void EntityPositionChanged(object sender, EntityPositionChangedArgs e);
    public class EntityPositionChangedArgs: EventArgs
    {
        public int PreviousX { get; private set; }
        public int PreviousY { get; private set; }

        public int NewX { get; private set; }
        public int NewY { get; private set; }

        public EntityPositionChangedArgs(int previousX, int previousY, int newX, int newY)
        {
            PreviousX = previousX;
            PreviousY = previousY;
            NewX = newX;
            NewY = newY;
        }
    }
}
