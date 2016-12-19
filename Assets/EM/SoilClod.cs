using System;

namespace EM
{
    public class SoilClod : SolidClod
    {
        public SoilClod() : base(true)
        {

        }

        public override void render(Island island, int x, int y, int z)
        {
            island.setTexture("Soil");
            island.addBoxToMesh(x, y, z, 1, 1, 1);
        }
    }
}
