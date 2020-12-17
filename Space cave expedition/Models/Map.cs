using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Interfaces;
using Space_cave_expedition.Enums;
using Space_cave_expedition.Helpers;

namespace Space_cave_expedition.Models
{
    public class Map
    {
        public int MapHeight { get; private set; }
        public int MapWidth { get; private set; }

        public List<MapTemplate> MapTemplates { get; private set; }
        public List<IEntity> ListOfEntities { get; private set; }
        public event Action<IEntity> EntityAdded;

        /// <summary>
        /// Creates a new map instance.
        /// </summary>
        /// <param name="mapDirectoryPath">Path for the directory containing the map templates.</param>
        public Map(string mapDirectoryPath)
        {
            ListOfEntities = new List<IEntity>();
            MapTemplates = new List<MapTemplate>();

            string[] files = Directory.GetFiles(mapDirectoryPath);
            foreach(string s in files)
            {
                string helper = s.Split('\\')[^1].ToLower();
                if (helper.EndsWith("template.txt"))
                {
                    helper = helper.Replace("template.txt", "");
                    switch (helper)
                    {
                        case "red":
                            MapTemplates.Add(new MapTemplate(File.ReadAllText(s), ConsoleColor.Red));
                            break;
                        case "green":
                            MapTemplates.Add(new MapTemplate(File.ReadAllText(s), ConsoleColor.Green));
                            break;
                        case "blue":
                            MapTemplates.Add(new MapTemplate(File.ReadAllText(s), ConsoleColor.Blue));
                            break;
                        case "gray":
                            MapTemplates.Add(new MapTemplate(File.ReadAllText(s), ConsoleColor.Gray));
                            break;
                    }
                }
            }

            int mapHeight = MapTemplates[0].MapHeight;
            int mapWidth = MapTemplates[0].MapWidth;
            foreach(MapTemplate mt in MapTemplates)
            {
                if (mapHeight != mt.MapHeight || mapWidth != mt.MapWidth)
                    throw new ArgumentException("Unable to make a map out of differently sized map templates.");
            }
            MapHeight = mapHeight;
            MapWidth = mapWidth;
        }
        /// <summary>
        /// Adds an entity into the map.
        /// After adding the entity is added, fires the EntityPositionChanged event so that the entity is displayed.
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(IEntity entity)
        {
            ListOfEntities.Add(entity);
            EntityAdded?.Invoke(entity);
        }
        /// <summary>
        /// Moves an entity inside a map.
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks>The entity must be inside the map, or else nothing happens.</remarks>
        public void MoveEntity(IEntity entity, EntityMoveDirection moveDirection)
        {
            bool entityIsInList = false;
            foreach(IEntity e in ListOfEntities)
            {
                if (e == entity)
                    entityIsInList = true;
            }

            if (entityIsInList)
            {
                if(CollisionDetection.DetectCollision1X1(this, moveDirection, entity.XPosition, entity.YPosition))
                    entity.Move(moveDirection);
            }
        }
    }
}
