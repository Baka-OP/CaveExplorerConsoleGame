using System;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Models;
using Space_cave_expedition.Enums;

namespace Space_cave_expedition.Interfaces
{
    public interface IEntity
    {
        /// <summary>
        /// x position of the entity, starting from left to right.
        /// </summary>
        public ushort XPosition { get; }
        /// <summary>
        /// y position of the entity, starting from bottom to top.
        /// </summary>
        public ushort YPosition { get; }
        /// <summary>
        /// Returns how an entity looks in a string form. If it stretches across multiple lines, use \n
        /// </summary>
        public string Appearance { get;}

        /// <summary>
        /// Moves an entity in a specific direction.
        /// </summary>
        /// <param name="directionOfMovement"></param>
        /// <param name="mapHeight">How many fields are in the Y position. Used so that the player doesn't escape the map.</param>
        /// <param name="mapWidth">How many fields are in the X position. Used so that the player doesn't escape the map.</param>
        /// <remarks>Doesn't implement collision detection. If you want collision detection, check whether the player can move first before moving it.</remarks>
        public void Move(EntityMoveDirection directionOfMovement);
        /// <summary>
        /// Returns a ReadOnlyCollection of coordinates which the entity occupies in the current position.
        /// </summary>
        /// <returns></returns>
        public List<Coordinate> AffectedPositions();
        /// <summary>
        /// Returns a ReadOnlyCollection of coordinates which the entity would occupy in a specific position.
        /// </summary>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <returns></returns>
        public List<Coordinate> AffectedPositions(int xPosition, int yPosition);
        /// <summary>
        /// Activates when an entities position changes.
        /// </summary>
        public event EntityPositionChanged EntityPositionChanged;
        /// <summary>
        /// Fired when the destroying process of an entity is started.
        /// </summary>
        public event Action<IEntity> EntityDestroying;
    }
}
