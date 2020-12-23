using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using Cave_Explorer.Helpers;

namespace Cave_Explorer.Models
{
    class MapEditor
    {
        public string entitiesText;
        public List<MapTemplate> templates;
        /// <summary>
        /// Instantiates a new editor.
        /// When loading a map, make sure it's parsable, or unpredictable things or crashes might happen.
        /// </summary>
        /// <param name="nameOrPath">If no \ is present, creates a new map, if \ is present, loads the map from the specified path.</param>
        public MapEditor(string nameOrPath)
        {
            templates = new List<MapTemplate>();
            if (nameOrPath.Contains('\\'))
            {
                string[] files = Directory.GetFiles(nameOrPath);
                foreach(string s in files)
                {
                    templates.Add(new MapTemplate(s, StringToColor.ConvertFromString(s.Split('\\')[^1].ToLower().Replace("template.txt", ""))));
                }
            }
            else
            {
                templates.Add(new MapTemplate(60, 20));
            }
        }
        public void SetMapSize(int newWidth, int newHeight)
        {
            foreach(MapTemplate t in templates)
            {
                t.ChangeSize(newWidth, newHeight);
            }
        }
    }
}
