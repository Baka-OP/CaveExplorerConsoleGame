using System;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Enums;
using Space_cave_expedition.Helpers;

namespace Space_cave_expedition.Models
{
    class Map
    {
        #region Map Templates
        /// <summary>
        /// The template of the map, before any additional modifications -> a one long string.
        /// </summary>
        public string MapTemplate { get; private set; }
        /// <summary>
        /// The MapTemplate, but processed into a two-dimensional char array without any entities added yet. 
        /// Preloaded on purpose because it doesn't change often and is often used, reducing rendering times.
        /// </summary>
        public char[,] MapLayoutTemplate { get; private set; }
        /// <summary>
        /// Complete MapLayout along with entities added in.
        /// </summary>
        public char[,] MapLayout { get; private set; }
        #endregion
        #region Main map properties

        public ConsoleColor MapBackgroundColor { get; private set; }
        public ConsoleColor MapForegroundColor { get; private set; }



        private ControllableEntity _FocusedEntity;
        /// <summary>
        /// The object that is going to be at the center of the screen, if the position of this entity changes focus moves.
        /// </summary>
        public ControllableEntity FocusedEntity
        {
            get { return _FocusedEntity; }
            set { _FocusedEntity = value; }
        }
        public int MapWidth { get; private set; }
        public int MapHeight { get; set; }

        #endregion
        private bool isStarted;
        List<ControllableEntity> ListOfEntities;

        /// <summary>
        /// Creates an instance of a map.
        /// </summary>
        /// <param name="mapTemplate">The map layout (walls), that the entites can't interact with directly.</param>
        /// <param name="backgroundColor">Background color of the whole map.</param>
        /// <param name="wallColor">Colour of the layout for the map.</param>
        /// <param name="FocusedEntity">Entity which will be at the center of the screen.</param>
        public Map(string mapTemplate, ConsoleColor backgroundColor, ConsoleColor wallColor, ControllableEntity focusedEntity)
        {
            MapForegroundColor = wallColor;
            MapBackgroundColor = backgroundColor;

            MapTemplate = mapTemplate;
            CreateMapLayoutTemplate();

            FocusedEntity = focusedEntity;
            ListOfEntities = new List<ControllableEntity>();
            AddEntity(FocusedEntity);

            isStarted = false;
        }
        public void Start()
        {
            Console.Clear();
            UpdateLayout();
            DisplayMap();
            isStarted = true;
        }

        #region Map layout management methods
        /// <summary>
        /// Updated the layout of the map, but doesn't display it.
        /// </summary>
        private void UpdateLayout()
        {
            MapLayout = (char[,])MapLayoutTemplate.Clone();
            AddEntitiesToMapLayout();
        }
        private void DisplayMap()
        {
            for(int i = 0; i < MapHeight; i++)
            {
                string line = "";
                for(int j = 0; j < MapWidth; j++)
                {
                    line += MapLayout[j, i];
                }
                Console.WriteLine(line);
            }
        }
        private void AddEntitiesToMapLayout()
        {
            foreach(ControllableEntity e in ListOfEntities)
            {
                e.InsertIntoMap(MapLayout, DisplayMethod.StartFromTopLeft);
            }
        }
        private void CreateMapLayoutTemplate()
        {
            string[] lines = MapTemplate.Split('\n');
            if (!isStarted)
            {
                MapHeight = lines.Length;
                MapWidth = Helper.GetLongestStringLength(lines);
            }

            MapLayoutTemplate = new char[MapWidth, MapHeight];
            for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
            {
                for (int charNumber = 0; charNumber < lines[lineNumber].Length; charNumber++)
                {
                    MapLayoutTemplate[charNumber, lineNumber] = lines[lineNumber][charNumber];
                }
            }
        }
        #endregion

        //Entity manipulation
        /// <summary>
        /// Adds an entity to the map.
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(ControllableEntity entity)
        {
            ListOfEntities.Add(entity);
            entity.PositionChanged += EntityPositionChanged;
            if (isStarted)
                entity.InsertIntoMap(MapLayout, DisplayMethod.StartFromTopLeft);
        }
        /// <summary>
        /// Moves an entity inside the map, needs to be inside the ListOfEntities list.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="movementDirection"></param>
        public void MoveEntity(ControllableEntity entity, PlayerMoveDirection movementDirection)
        {
            //Checking, whether the character is at the edge of the map.
            if (movementDirection == PlayerMoveDirection.Up && entity.YPosition == 0 ||
                movementDirection == PlayerMoveDirection.Down && entity.YPosition == MapHeight - 1 ||
                movementDirection == PlayerMoveDirection.Left && entity.XPosition == 0 ||
                movementDirection == PlayerMoveDirection.Right && entity.XPosition == MapWidth - 1)
                return;

            if(CollisionDetection.DetectCollision(MapLayout, entity, movementDirection))
            {
                entity.Move(movementDirection);
            }
        }

        //Events
        private void EntityPositionChanged(object source, EntityPositionChangedArgs e)
        {
            Console.SetCursorPosition(0, 0);
            UpdateLayout();
            DisplayMap();
        }
    }
}
