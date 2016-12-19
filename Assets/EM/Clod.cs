using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM
{
    public class Clod
    {
        //不多说。。

        public static Clod Air = new Clod(false);
        public static StoneClod Stone = new StoneClod();
        public static GrassClod Grass = new GrassClod();
        public static SoilClod Soil = new SoilClod();

        public bool isSolid;

        public Clod(bool Solid)
        {
            isSolid = Solid;
        }

        public virtual void render(Island island,int x,int y,int z)
        {

        }
    }
}
