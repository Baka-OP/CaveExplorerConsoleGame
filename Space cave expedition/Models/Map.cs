using System;
using System.Collections.Generic;
using System.Text;

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
        /// The object that is going to be at the center of the screen, if the position of this entity changes, the camera moves.
        /// </summary>
        public ControllableEntity FocusedEntity
        {
            get { return _FocusedEntity; }
            set { _FocusedEntity = value; }
        }
        public int MapWidth { get; private set; }
        public int MapHeight { get; set; }

        #endregion
        /// <summary>
        /// Instance of a map.
        /// </summary>
        /// <param name="mapTemplate">The map layout (walls), that the entites can't interact with directly.</param>
        /// <param name="backgroundColor">Background color of the whole map.</param>
        /// <param name="wallColor">Colour of the layout for the map.</param>
        public Map(string mapTemplate, ConsoleColor backgroundColor, ConsoleColor wallColor)
        {
            MapForegroundColor = wallColor;
            MapBackgroundColor = backgroundColor;
            MapTemplate = mapTemplate;
        }

    }
}
