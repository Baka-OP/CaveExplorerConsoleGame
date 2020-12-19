﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Space_cave_expedition.Helpers
{
    class Helper
    {
        /// <summary>
        /// Returns the longest strings length.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int GetLongestStringLength(string[] array)
        {
            int longest = 0;
            foreach(string s in array)
            {
                if (s.Length > longest)
                    longest = s.Length;
            }
            return longest;
        }
        /// <summary>
        /// Reads a directory and returns all map names that should be readable.
        /// </summary>
        /// <param name="mapDirectoryPath"></param>
        /// <returns>The directory paths to the maps</returns>
        /// <remarks>Doesn't check the parsability of the maps. If a map file is corrupted, the method will still return it.</remarks>
        public static List<string> GetAndVerifyMaps(string mapDirectoryPath)
        {
            List<string> foundMaps = new List<string>(Directory.GetDirectories(mapDirectoryPath));
            List<string> readableMaps = new List<string>();
            foreach(string s in foundMaps)
            {
                if (VerifyMap(s))
                    readableMaps.Add(s);
            }
            return readableMaps;
        }
        public static bool VerifyMap(string mapDirectory)
        {
            List<string> files = new List<string>(Directory.GetFiles(mapDirectory));
            if (!files.Contains("Entities.txt"))
                return false;
            if (files.FindAll(x => x.ToLower().EndsWith("template.txt")).Count == 0)
                return false;
            return true;
        }
    }
}
