using Space_cave_expedition.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Space_cave_expedition.Models;

namespace Space_cave_expedition.LegacyModels
{
    class Player: ControllableEntity
    {
        public override void Move(EntityMoveDirection directionOfMovement)
        {
            switch (directionOfMovement)
            {
                //No need to fire events, by changing Position and Appearance properties, the event fires automatically
                case EntityMoveDirection.Up:
                    Appearance = "^";
                    YPosition--;
                    break;
                case EntityMoveDirection.Down:
                    Appearance = "v";
                    YPosition++;
                    break;
                case EntityMoveDirection.Left:
                    Appearance = "<";
                    XPosition--;
                    break;
                case EntityMoveDirection.Right:
                    Appearance = ">";
                    XPosition++;
                    break;
                default:
                    throw new ArgumentException("Error. Unexpected movement direction.");
            }
        }
        /// <summary>
        /// New instance of a player entity, doesn't mean you automatically control it though, just has the functions and appearance of one.
        /// </summary>
        /// <param name="positionLeft">Starting CursorLeft position</param>
        /// <param name="positionTop">Starting CursorTop position</param>
        public Player(int positionLeft, int positionTop)
        {
            XPosition = positionLeft;
            YPosition = positionTop;
            Appearance = ">";
        }
    }
}
