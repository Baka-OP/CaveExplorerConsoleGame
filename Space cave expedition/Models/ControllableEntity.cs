using System;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Enums;

namespace Space_cave_expedition.Models
{
    /// <summary>
    /// A bot, player, or an object displayable on the map. Can only be inherited.
    /// </summary>
    abstract class ControllableEntity
    {
        public int EntityHeight { get; private set; }
        public int EntityWidth { get; private set; }

        #region Position methods and properties
        public event EntityPositionChanged PositionChanged;
        protected int _LeftPosition;
        /// <summary>
        /// CursorLeft position of the entity, if the entity is multiple characters long, the topmost, leftmost, even if it is a whitespace, is returned
        /// </summary>
        public int LeftPosition
        {
            get
            {
                return _LeftPosition;
            }
            protected set
            {
                int previousLeft = _LeftPosition;
                _LeftPosition = value;
                PositionChanged?.Invoke(this, new EntityPositionChangedArgs(previousLeft, _TopPosition, _LeftPosition, _TopPosition));
            }
        }
        protected int _TopPosition;
        /// <summary>
        /// CursorTop position of the entity, if the entity is multiple characters long, the topmost characters are chosen.
        /// </summary>
        public int TopPosition
        {
            get
            {
                return _TopPosition;
            }
            protected set
            {
                int previousTop = _TopPosition;
                _TopPosition = value;
                PositionChanged?.Invoke(this, new EntityPositionChangedArgs(LeftPosition, previousTop, _LeftPosition, _TopPosition));
            }
        }
        /// <summary>
        /// Moves the entity in a specific direction.
        /// </summary>
        /// <param name="directionOfMovement">Direction of the movement.</param>
        public abstract void Move(PlayerMoveDirection directionOfMovement);
        #endregion
        #region Appearance methods and properties
        public event EntityAppearanceChanged AppearanceChanged;
        protected string _Appearance;
        /// <summary>
        /// String containing the entities appearance.
        /// Can be stretched upon multiple lines. And is always a rectangle or a square.
        /// </summary>
        public string Appearance
        {
            get
            {
                return _Appearance;
            }
            protected set
            {
                string[] lines = value.Split('\n');
                int entityWidth = lines[0].Length;
                foreach(string s in lines)
                {
                    if (entityWidth != s.Length)
                        throw new Exception("Entity is not a rectangle or a square, please add whitespaces to make all lines equally long.");
                }
                EntityHeight = lines.Length;
                EntityWidth = entityWidth;
                _Appearance = value;
                AppearanceChanged?.Invoke(this, new EntityAppearanceChangedArgs(value));
            }
        }
        /// <summary>
        /// Displays the player inside a map.
        /// </summary>
        /// <param name="LeftPosition">Console.CursorLeft value, from which to start displaying.</param>
        /// <param name="TopPosition">Console.CursorTop value, from which to start displaying.</param>
        /// <param name="displayMethod">From where to start displaying the entity. If it is StartFromCenter, then xPosition and yPosition is the center of the entity.</param>
        public virtual void Display(int LeftPosition, int TopPosition, DisplayMethod displayMethod)
        {
            Console.SetCursorPosition(LeftPosition, TopPosition);
            if(displayMethod == DisplayMethod.StartFromTopLeft)
            {
                string[] lines = Appearance.Split('\n');
                foreach(string s in lines)
                {
                    Console.Write(s);
                    Console.SetCursorPosition(LeftPosition, Console.CursorTop + 1);
                }
            }
            else if (displayMethod == DisplayMethod.StartFromCenter)
            {
                //Starting from center is a bit harder to do, so I won't be making it for now. I don't even need it.
                throw new NotImplementedException("Error. DisplayMethod has been set to StartFromCenter, but StartFromCenter has not yet been implemented.");
            }
            else
            {
                throw new ArgumentException("Error, displayMethod seems to have a value that has not been put into consideration yet.");
            }
        }
        #endregion
    }
}
