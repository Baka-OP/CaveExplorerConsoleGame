using System;

namespace Space_cave_expedition
{
    public delegate void EntityPositionChanged(object sender, EntityPositionChangedArgs e);
    public class EntityPositionChangedArgs: EventArgs
    {
        public int PreviousPositionLeft { get; private set; }
        public int PreviousPositionTop { get; private set; }

        public int NewPositionLeft { get; private set; }
        public int NewPositionTop { get; private set; }

        public EntityPositionChangedArgs(int previousPositionLeft, int previousPositionTop, int newPositionLeft, int newPositionTop)
        {
            PreviousPositionLeft = previousPositionLeft;
            PreviousPositionTop = previousPositionTop;
            NewPositionLeft = newPositionLeft;
            NewPositionTop = newPositionTop;
        }
    }
}
