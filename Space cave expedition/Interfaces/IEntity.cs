using System;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Models;
using Space_cave_expedition.Enums;

namespace Space_cave_expedition.Interfaces
{
    interface IEntity
    {
        /// <summary>
        /// x position of the entity, starting from left to right.
        /// </summary>
        public ushort XPosition { get; protected set; }
        /// <summary>
        /// y position of the entity, starting from bottom to top.
        /// </summary>
        public ushort YPosition { get; protected set; }
        /// <summary>
        /// Returns how an entity looks in a string form.
        /// Example: 
        /// ^ -\n
        /// | >\n
        /// v -\n
        /// </summary>
        public string Appearance { get; protected set; }

        /// <summary>
        /// Moves an entity in a specific direction.
        /// </summary>
        /// <param name="directionOfMovement"></param>
        public void Move(EntityMoveDirection directionOfMovement);
        /// <summary>
        /// Inserts the entity into a 2-dimensional char array.
        /// </summary>
        public void InsertIntoMap();
        /// <summary>
        /// Returns an array of all x and y coordinates which the entity occupies in the current position.
        /// </summary>
        /// <returns></returns>
        public int[][] AffectedPositions();
        /// <summary>
        /// Returns an array of all x and y coordinates which the entity would occupy in a specific position.
        /// </summary>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <returns></returns>
        public int[][] AffectedPositions(int xPosition, int yPosition);
        /// <summary>
        /// Inserts an entity into a 2-dimensional char array according to the xPosition and yPosition properties.
        /// </summary>
        /// <param name="map">Map to insert the entity into</param>
        public void InsertIntoMap(char[,] map);
        /// <summary>
        /// Inserts an entity into a 2-dimensional char array according to specified coordinates.
        /// </summary>
        /// <param name="map">Map to insert the entity into</param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        public void InsertIntoMap(char[,] map, int xPosition, int yPosition);
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
