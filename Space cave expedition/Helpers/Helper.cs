using System;
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
    }
}
