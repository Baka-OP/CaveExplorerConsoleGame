using System;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Enums;
using Space_cave_expedition.Helpers;

namespace Space_cave_expedition.Models
{
    class Map
    {
        #region Main map properties

        public ConsoleColor MapBackgroundColor { get; private set; }
        public ConsoleColor MapForegroundColor { get; private set; }
        public char[,] MapLayout { get; private set; }
        public string MapTemplate { get; private set; }

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
            string[] lines = mapTemplate.Split('\n');
            MapHeight = lines.Length;
            MapWidth = Helper.GetLongestStringLength(lines);

            FocusedEntity = focusedEntity;
            ListOfEntities = new List<ControllableEntity>();
            AddEntity(FocusedEntity);
            isStarted = false;
        }
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
        public void Start()
        {
            Console.Clear();
            DisplayMap();
            isStarted = true;
        }

        //Displaying methods
        private void DisplayMap()
        {
            string[] lines = MapTemplate.Split('\n');
            MapLayout = new char[MapWidth, MapHeight];
            for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
            {
                for (int charNumber = 0; charNumber < lines[lineNumber].Length; charNumber++)
                {
                    MapLayout[charNumber, lineNumber] = lines[lineNumber][charNumber];
                }
            }
            AddEntitiesToMapLayout();

            for(int i = 0; i < MapHeight; i++)
            {
                for(int j = 0; j < MapWidth; j++)
                {
                    Console.Write(MapLayout[j, i]);
                }
                Console.WriteLine();
            }
        }
        private void AddEntitiesToMapLayout()
        {
            foreach(ControllableEntity e in ListOfEntities)
            {
                e.InsertIntoMap(MapLayout, DisplayMethod.StartFromTopLeft);
            }
        }

        //Events
        private void EntityPositionChanged(object source, EntityPositionChangedArgs e)
        {
            Console.SetCursorPosition(0, 0);
            DisplayMap();
        }
    }
}
