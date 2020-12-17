using System;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Interfaces;
using Space_cave_expedition.Models;

namespace Space_cave_expedition.Graphic_Components
{
    public class Camera
    {
        public static Camera CameraInstance;
        /// <summary>
        /// Whether an instance of a camera has been made.
        /// </summary>
        private static bool isInstantiated;

        #region Entity and map properties

        private IEntity _FocusedEntity;
        /// <summary>
        /// Entity that will be at the center of the screen. Must be inside the monitored map!
        /// </summary>
        public IEntity FocusedEntity
        {
            get { return _FocusedEntity; }
            set { _FocusedEntity = value; if(IsStarted) DisplayMap(); }
        }

        private Map _Map;
        /// <summary>
        /// Map that will be displayed on the screen.
        /// </summary>
        public Map Map
        {
            get { return _Map; }
            private set { _Map = value; OnMapChanged(); }
        }
        #endregion
        #region Camera properties
        /// <summary>
        /// Indicates whether the camera has been started, and thus is displaying the map into the console.
        /// </summary>
        private bool IsStarted;

        private int _StartingCursorLeft;
        /// <summary>
        /// CursorLeft value where the leftmost part of the map will be displayed.
        /// </summary>
        public int StartingCursorLeft
        {
            get { return _StartingCursorLeft; }
            set { _StartingCursorLeft = value; }
        }

        private int _StartingCursorTop;
        /// <summary>
        /// CursorTop value where the topmost parts of the map will be displayed.
        /// </summary>
        public int StartingCursorTop
        {
            get { return _StartingCursorTop; }
            set { _StartingCursorTop = value; }
        }


        #endregion
        #region Event handling methods

        private void OnMapChanged()
        {
            Map.EntityAdded += OnNewEntityAdded;
        }

        private void OnNewEntityAdded(IEntity obj)
        {
            obj.EntityDestroying += OnEntityDestroying;
            obj.EntityPositionChanged += OnEntityPositionChanged;
        }

        private void OnEntityPositionChanged(object sender, EntityPositionChangedArgs e)
        {
            int bottomMost = StartingCursorTop + Map.MapHeight - 1;
            //TODO: Make camera be able to handle two entities in the same position (bug during placement or anything), instead of just putting a whitespace
            IEntity entity = sender as IEntity;
            List<Coordinate> previousCoordinates = entity.AffectedPositions(e.PreviousX, e.PreviousY);
            foreach(Coordinate c in previousCoordinates)
            {
                Console.SetCursorPosition(c.X + StartingCursorLeft, bottomMost - c.Y);
                Console.Write(' ');
            }

            List<Coordinate> newCoordinates = entity.AffectedPositions();
            foreach(Coordinate c in newCoordinates)
            {
                Console.SetCursorPosition(c.X + StartingCursorLeft, bottomMost - c.Y);
                Console.Write(c.Content);
            }
        }

        private void OnEntityDestroying(IEntity obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Creates a new map instance. If an instance of a camera has already been made, throws an exception.
        /// </summary>
        /// <param name="startingCursorLeft">The leftmost CursorLeft position, from which the camera starts displaying the map</param>
        /// <param name="startingCursorTop">The topmost CursorTop position, from which the camera starts displaying the map</param>
        /// <param name="monitoredMap">Map the camera monitors</param>
        /// <exception cref="Exception"></exception>
        public Camera(int startingCursorLeft, int startingCursorTop, Map monitoredMap)
        {
            IsStarted = false;
            if (isInstantiated)
                throw new Exception("Error, a camera has already been instantiated.");
            StartingCursorLeft = startingCursorLeft;
            StartingCursorTop = startingCursorTop;

            Map = monitoredMap;
            isInstantiated = true;
        }
        ~Camera()
        {
            isInstantiated = false;
        }

        /// <summary>
        /// Completely displays the whole map. If the camera isn't started, starts it automatically.
        /// </summary>
        /// <remarks>Do not use this for displaying a lot of times at once, takes a lot of time to display (80-120ms).</remarks>
        public void DisplayMap()
        {
            if (!IsStarted)
                Start();

            //Indicates the bottom most cursorTop, to which the camera can write.
            int bottomMost = StartingCursorTop + Map.MapHeight -1;
            
            //Display all templates with their specific colours.
            foreach(MapTemplate mp in Map.MapTemplates)
            {
                Console.ForegroundColor = mp.Color;
                for(int i = 0; i < mp.MapHeight; i++)
                {
                    for(int j = 0; j < mp.MapWidth; j++)
                    {
                        if(mp.Template[j, i] != ' ')
                        {
                            Console.SetCursorPosition(j + StartingCursorLeft, i + StartingCursorTop);
                            Console.Write(mp.Template[j, i]);
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            //Display all of the entities
            foreach(IEntity e in Map.ListOfEntities)
            {
                List<Coordinate> positions = e.AffectedPositions();
                foreach(Coordinate position in positions)
                {
                    Console.SetCursorPosition(position.X + StartingCursorLeft, bottomMost - position.Y);
                    Console.Write(position.Content);
                }
            }
        }
        /// <summary>
        /// Makes the camera listen to entity events and starts displaying the map.
        /// </summary>
        public void Start()
        {
            IsStarted = true;
            DisplayMap();
        }
        /// <summary>
        /// Stops the camera and clears the console.
        /// </summary>
        public void Stop()
        {
            IsStarted = false;
            Console.Clear();
        }
    }
}
