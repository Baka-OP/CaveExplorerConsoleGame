using Space_cave_expedition.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Space_cave_expedition.Models
{
    class Player: ControllableEntity
    {
        public override void Move(PlayerMoveDirection directionOfMovement)
        {
            switch (directionOfMovement)
            {
                //No need to fire events, by changing Position and Appearance properties, the event fires automatically
                case PlayerMoveDirection.Up:
                    Appearance = "^";
                    YPosition--;
                    break;
                case PlayerMoveDirection.Down:
                    Appearance = "v";
                    YPosition++;
                    break;
                case PlayerMoveDirection.Left:
                    Appearance = "<";
                    XPosition--;
                    break;
                case PlayerMoveDirection.Right:
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
