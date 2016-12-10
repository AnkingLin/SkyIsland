using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM
{
    public class Clod
    {
        public static Clod Air = new Clod(false);
        public static SolidClod Stone = new SolidClod(true);

        public bool isSoild;

        public Clod(bool isSolid)
        {
            this.isSoild = isSoild;
        }

        public virtual void render(Island island,int x,int y,int z)
        {

        }
    }
}
