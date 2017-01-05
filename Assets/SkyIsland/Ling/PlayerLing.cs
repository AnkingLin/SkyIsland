using UnityEngine;

namespace SkyIsland
{
    public class PlayerLing : Ling
    {
        public int ix, iz;

        public PlayerLing(Sky sky) : base(sky)
        {
            
        }

        public void updateIsland()
        {
            for (int x = ix - 4; x < 4; x++)
            {
                for (int z = iz - 4; z < 4; z++)
                {
                    sky.addIsland(x, z);
                }
            }

            ix = (int)ls.gameObject.transform.position.x >> 4;
            iz = (int)ls.gameObject.transform.position.z >> 4;
        }
    }
}
