using System;

namespace EM
{
    public class GrassClod : SolidClod
    {
        public GrassClod() : base(true)
        {

        }

        public override void createMesh(Island island, int x, int y, int z)
        {
            island.setTexture("Grass_Side", "Grass_Side", "Grass_Top", "Soil", "Grass_Side", "Grass_Side");
            island.addBoxToMesh(x, y, z, 1, 1, 1);
        }
    }
}
