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
                    TopPosition++;
                    Appearance = "^";
                    break;
                case PlayerMoveDirection.Down:
                    TopPosition--;
                    Appearance = "v";
                    break;
                case PlayerMoveDirection.Left:
                    LeftPosition--;
                    Appearance = "<";
                    break;
                case PlayerMoveDirection.Right:
                    LeftPosition++;
                    Appearance = ">";
                    break;
                default:
                    throw new ArgumentException("Error. Unexpected movement direction.");
            }
        }
        /// <summary>
        /// 
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
