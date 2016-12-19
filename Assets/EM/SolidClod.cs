using System;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EM
{
    public class SolidClod : Clod
    {
        //不多说。。

        public SolidClod(bool isSolid):base(isSolid)
        {
            
        }

        public override void render(Island island,int x, int y, int z)
        {
            island.addBoxToMesh(x, y, z, 1, 1, 1);
        }
    }
}
