﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM
{
    public class Clod
    {
        public static Clod Air = new Clod(false);
        public static SolidClod Stone = new SolidClod(true);

        public bool isSolid;

        public Clod(bool Solid)
        {
            this.isSolid = Solid;
        }

        public virtual void render(Island island,int x,int y,int z)
        {

        }
    }
}