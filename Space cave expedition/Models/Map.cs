using System;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Enums;

namespace Space_cave_expedition.Models
{
    class Map
    {
        #region Main map properties

        public ConsoleColor MapBackgroundColor { get; private set; }
        public ConsoleColor MapForegroundColor { get; private set; }
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
        public Map(string mapTemplate, ConsoleColor backgroundColor, ConsoleColor wallColor, ControllableEntity FocusedEntity)
        {
            MapForegroundColor = wallColor;
            MapBackgroundColor = backgroundColor;
            MapTemplate = mapTemplate;
            this.FocusedEntity = FocusedEntity;
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
                entity.Display(entity.LeftPosition, entity.TopPosition, DisplayMethod.StartFromTopLeft);
        }
        public void Start()
        {
            Console.Clear();
            Console.WriteLine(MapTemplate);
            DisplayAllEntities();
            isStarted = true;
        }
        private void DisplayAllEntities()
        {
            foreach(ControllableEntity e in ListOfEntities)
            {
                e.Display(e.LeftPosition, e.TopPosition, DisplayMethod.StartFromTopLeft);
            }
        }

        private void EntityPositionChanged(object source, EntityPositionChangedArgs e)
        {
            ControllableEntity sender = source as ControllableEntity;
            Console.SetCursorPosition(e.PreviousPositionLeft, e.PreviousPositionTop);

            for(int i = 0; i < sender.EntityHeight; i++)
            {
                Console.WriteLine("".PadRight(sender.EntityWidth));
                Console.SetCursorPosition(e.PreviousPositionLeft, Console.CursorTop);
            }

            Console.SetCursorPosition(e.NewPositionLeft, e.NewPositionTop);
            sender.Display(e.NewPositionLeft, e.NewPositionTop, DisplayMethod.StartFromTopLeft);
        }
    }
}
