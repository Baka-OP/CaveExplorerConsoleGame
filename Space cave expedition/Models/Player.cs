using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Interfaces;
using Space_cave_expedition.Models;
using Space_cave_expedition.Enums;

namespace Space_cave_expedition.Models
{
    class Player: IEntity
    {
        public ushort XPosition { get; private set; }
        public ushort YPosition { get; private set; }
        public string Appearance { get; private set; }

        public List<Coordinate> AffectedPositions(int xPosition, int yPosition)
        {
            return new List<Coordinate> { new Coordinate(xPosition, yPosition, Appearance[0]) };
        }
        public List<Coordinate> AffectedPositions()
        {
            return new List<Coordinate> { new Coordinate(XPosition, YPosition, Appearance[0]) };
        }
        /*public void InsertIntoMap(char[,] map)
        {
            //Player appearance always has only one character.
            map[XPosition, YPosition] = Appearance[0];
        }
        public void InsertIntoMap(char[,] map, int xPosition, int yPosition)
        {
            //Player appearance always has only one character.
            map[xPosition, yPosition] = Appearance[0];
        }*/
        public void Move(EntityMoveDirection moveDirection)
        {
            int previousX = XPosition;
            int previousY = YPosition;
            switch (moveDirection)
            {
                case EntityMoveDirection.Up:
                    YPosition++;
                    Appearance = "^";
                    break;
                case EntityMoveDirection.Down:
                    YPosition--;
                    Appearance = "v";
                    break;
                case EntityMoveDirection.Left:
                    XPosition--;
                    Appearance = "<";
                    break;
                case EntityMoveDirection.Right:
                    XPosition++;
                    Appearance = ">";
                    break;
                default:
                    throw new ArgumentException("Unexpected movement direction.");
            }
            OnEntityPositionChanged(previousX, previousY);
        }
        public Player(ushort xPosition, ushort yPosition)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Appearance = ">";
        }
        #region Events
        public event EntityPositionChanged EntityPositionChanged;
        private void OnEntityPositionChanged(int previousX, int previousY) => EntityPositionChanged?.Invoke(this, new EntityPositionChangedArgs(previousX, previousY, XPosition, YPosition));
        public event Action<IEntity> EntityDestroying;
        private void OnEntityDestroying() => EntityDestroying?.Invoke(this);
        #endregion
    }
}
