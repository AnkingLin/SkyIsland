using System;
using UnityEngine;

namespace SkyIsland
{
    public class StoneClod : SolidClod
    {
        public StoneClod() : base(true)
        {

        }

        public override void createMesh(Island island, int x, int y, int z)
        {
            island.setTexture("Stone");
            island.addBoxToMesh(x, y, z, 1, 1f, 1);
        }
    }
}
