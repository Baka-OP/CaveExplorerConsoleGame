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
            int previousTop = Console.CursorTop;
            int previousLeft = Console.CursorLeft;
            switch (directionOfMovement)
            {
                //No need to fire events, by changing Position and Appearance properties, the event fires automatically
                case PlayerMoveDirection.Up:
                    if (TopPosition > 0)
                    {
                        Appearance = "^";
                        TopPosition--;
                    }
                    break;
                case PlayerMoveDirection.Down:
                    Appearance = "v";
                    TopPosition++;
                    break;
                case PlayerMoveDirection.Left:
                    if (LeftPosition > 0)
                    {
                        Appearance = "<";
                        LeftPosition--;
                    }
                    break;
                case PlayerMoveDirection.Right:
                    Appearance = ">";
                    LeftPosition++;
                    break;
                default:
                    throw new ArgumentException("Error. Unexpected movement direction.");
            }
            Console.SetCursorPosition(previousLeft, previousTop);
        }
        /// <summary>
        /// New instance of a player entity, doesn't mean you automatically control it though, just has the functions and appearance of one.
        /// </summary>
        /// <param name="positionLeft">Starting CursorLeft position</param>
        /// <param name="positionTop">Starting CursorTop position</param>
        public Player(int positionLeft, int positionTop)
        {
            LeftPosition = positionLeft;
            TopPosition = positionTop;
            Appearance = ">";
        }
    }
}
