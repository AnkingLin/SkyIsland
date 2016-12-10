using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM
{
    public class SolidClod : Clod
    {
        public SolidClod(bool isSolid):base(isSolid)
        {
            
        }

        public override void render(Island island,int x, int y, int z)
        {
            island.addBoxToMesh(new UnityEngine.Vector3(x+0, y+0, z+0), new UnityEngine.Vector3(1, 1, 1));
        }
    }
}
