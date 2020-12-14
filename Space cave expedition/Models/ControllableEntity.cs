using System;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Enums;

namespace Space_cave_expedition.Models
{
    /// <summary>
    /// A bot, player, or an object displayable on the map. Can only be inherited.
    /// </summary>
    public abstract class ControllableEntity
    {
        public int EntityHeight { get; private set; }
        public int EntityWidth { get; private set; }

        #region Position methods and properties
        public event EntityPositionChanged PositionChanged;
        protected int _XPosition;
        /// <summary>
        /// XPosition position of the entity, if the entity is multiple characters long, the leftmost position is returned, even if it might be a whitespace.
        /// </summary>
        public int XPosition
        {
            get
            {
                return _XPosition;
            }
            protected set
            {
                int previousLeft = _XPosition;
                _XPosition = value;
                PositionChanged?.Invoke(this, new EntityPositionChangedArgs(previousLeft, _YPosition, _XPosition, _YPosition));
            }
        }
        protected int _YPosition;
        /// <summary>
        /// YPosition of the entity, if the entity is multiple characters long, the topmost position is chosen.
        /// </summary>
        public int YPosition
        {
            get
            {
                return _YPosition;
            }
            protected set
            {
                int previousTop = _YPosition;
                _YPosition = value;
                PositionChanged?.Invoke(this, new EntityPositionChangedArgs(XPosition, previousTop, _XPosition, _YPosition));
            }
        }
        /// <summary>
        /// Changes the entites TopPosition and LeftPosition based on the direction
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
        /// Puts the entity inside the map.
        /// </summary>
        /// <param name="LeftPosition">Console.CursorLeft value, from which to start displaying.</param>
        /// <param name="TopPosition">Console.CursorTop value, from which to start displaying.</param>
        /// <param name="displayMethod">From where to start displaying the entity. If it is StartFromCenter, then xPosition and yPosition is the center of the entity.</param>
        public virtual void InsertIntoMap(char[,] mapLayout, int XPosition, int YPosition, DisplayMethod displayMethod)
        {
            if(displayMethod == DisplayMethod.StartFromTopLeft)
            {
                string[] lines = Appearance.Split('\n');
                for (int line = 0; line < lines.Length; line++)
                {
                    for(int character = 0; character < lines[line].Length; character++)
                    {
                        if (YPosition - line >= 0)
                        {
                            mapLayout[YPosition - line, XPosition + character] = lines[line][character];
                        }
                    }
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
        /// <summary>
        /// Puts the entity inside the map, position based on the position of the entity.
        /// </summary>
        /// <param name="mapLayout"></param>
        /// <param name="displayMethod"></param>
        public virtual void InsertIntoMap(char[,] mapLayout, DisplayMethod displayMethod)
        {
            if (displayMethod == DisplayMethod.StartFromTopLeft)
            {
                string[] lines = Appearance.Split('\n');
                for (int line = 0; line < lines.Length; line++)
                {
                    for (int character = 0; character < lines[line].Length; character++)
                    {
                        if (YPosition - line >= 0)
                        {
                            //You need to subtract line, because the character is displayed from top to bottom.
                            mapLayout[XPosition + character, YPosition - line] = lines[line][character];
                        }
                    }
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
