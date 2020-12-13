using System;
using System.Collections.Generic;
using System.Text;

namespace Space_cave_expedition
{
    delegate void EntityPositionChanged(object sender, EntityPositionChangedArgs e);
    class EntityPositionChangedArgs: EventArgs
    {
        public int PreviousPositionX { get; private set; }
        public int PreviousPositionY { get; private set; }

        public int NewPositionX { get; private set; }
        public int NewPositionY { get; private set; }

        public EntityPositionChangedArgs(int previousPositionX, int previousPositionY, int newPositionX, int newPositionY)
        {
            PreviousPositionX = previousPositionX;
            PreviousPositionY = previousPositionY;
            NewPositionX = newPositionX;
            NewPositionY = newPositionY;
        }
    }
}
