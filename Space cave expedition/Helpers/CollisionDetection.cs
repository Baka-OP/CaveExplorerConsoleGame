using System;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Models;
using Space_cave_expedition.Enums;

namespace Space_cave_expedition.Helpers
{
    public static class CollisionDetection
    {
        /// <summary>
        /// Detects collision for objects that are of size 1x1 and move one field at a time.
        /// </summary>
        /// <returns>Whether the Entity can move or not</returns>
        /// <param name="map">Map in which the entity is contained.</param>
        /// <param name="movementDirection">Direction to which the entity is going to move to.</param>
        /// <param name="xPosition">X coordinate of the entity</param>
        /// <param name="yPosition">Y coordinate of the entity</param>
        public static bool DetectCollision1X1(Map map, EntityMoveDirection movementDirection, int xPosition, int yPosition)
        {
            foreach(MapTemplate mt in map.MapTemplates)
            {
                switch (movementDirection)
                {
                    //First the collision detector checks whether the player is about to move out of the map,
                    case EntityMoveDirection.Up:
                        if (yPosition + 1 == mt.MapHeight || mt.Template[xPosition, mt.MapHeight - yPosition - 2] != ' ')
                            return false;
                        break;
                    case EntityMoveDirection.Down:
                        if (yPosition == 0 || mt.Template[xPosition, mt.MapHeight - yPosition] != ' ')
                            return false;
                        break;
                    case EntityMoveDirection.Left:
                        if (xPosition == 0 || mt.Template[xPosition - 1, mt.MapHeight - yPosition - 1] != ' ')
                            return false;
                        break;
                    case EntityMoveDirection.Right:
                        if (xPosition + 1 == mt.MapWidth || mt.Template[xPosition + 1, mt.MapHeight - yPosition - 1] != ' ')
                            return false;
                        break;
                }
            }
            return true;
        }
    }
}
