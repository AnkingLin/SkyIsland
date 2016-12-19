using System;
using UnityEngine;

namespace EM
{
    public class StoneClod : SolidClod
    {
        public StoneClod() : base(true)
        {

        }

        public override void render(Island island, int x, int y, int z)
        {
            island.setTexture("Stone");
            island.addBoxToMesh(x, y, z, 1, 1f, 1);
        }
    }
}
