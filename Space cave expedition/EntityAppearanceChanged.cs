using System;
using System.Collections.Generic;
using System.Text;

namespace Space_cave_expedition
{
    public class EntityAppearanceChangedArgs: EventArgs
    {
        public string NewAppearance { get; private set; }
        public EntityAppearanceChangedArgs(string newAppearance)
        {
            NewAppearance = newAppearance;
        }
    }
    public delegate void EntityAppearanceChanged(object sender, EntityAppearanceChangedArgs e);
}
