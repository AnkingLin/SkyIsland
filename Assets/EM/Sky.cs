using System;
using System.Collections.Generic;
using UnityEngine;

namespace EM
{
	public class Sky:MonoBehaviour
	{
		public List<Island> islands;
        public GameObject thisobj;

		public Sky ()
		{
            thisobj = new GameObject("Sky");
            islands = new List<Island> ();
		}

        public bool setClod(Clod newClod, int x, int y, int z)
        {
            int ix = (x) >> 4;
            int iz = (z) >> 4;
            int cx = (x) & 15;
            int cy = (y) & 127;
            int cz = (z) & 15;
            Island island = getIsland(ix, iz);
            if (island != null)
            {
                island.setClod(newClod, cx, cy, cz);

                if (x == 0)
                    getIsland(ix - 1, iz).createMesh();
                else if (z == 0)
                    getIsland(ix, iz - 1).createMesh();
                else if (x == 15)
                    getIsland(ix + 1, iz).createMesh();
                else if (z == 15)
                    getIsland(ix, iz + 1).createMesh();

                return true;
            }
            return false;
        }

        public Clod getClod(int x,int y,int z)
        {
            int ix = (x) >> 4;
            int iz = (z) >> 4;
            int cx = (x) & 15;
            int cy = (y) & 127;
            int cz = (z) & 15;
            Island island = getIsland(ix, iz);
            if (island != null)
            {
                return island.getClod(cx, cy, cz);
            }
            return Clod.Air;
        }

        public void updateClod(int x,int z,bool isThis)
        {
            int ix = (x) >> 4;
            int iz = (z) >> 4;

            getIsland(ix, iz).createMesh();

            if (x > 15 || z > 15 && isThis)
            {
                updateClod(x + 1, z, false);
                updateClod(x - 1, z, false);
                updateClod(x, z + 1, false);
                updateClod(x, z - 1, false);
            }
        }

        public Island addIsland(int x,int z)
        {
            if (getIsland(x, z) == null)
            {
                Island island = new Island(this, x, z);
                islands.Add(island);
                island.buildClod();
                island.createMesh();

                islands.Sort(delegate(Island a, Island b){
                    if (a.ix != b.ix)
                        return a.ix.CompareTo(b.ix);
                    else
                        return a.iz.CompareTo(b.iz);
                });

                return island;
            }
            return getIsland(x, z);
        }

        public Island getIsland(int x, int z)
        {
            for (int i = 0; i < islands.Count; i++)
            {
                int ix = islands[i].ix;
                int iz = islands[i].iz;

                if (ix == x && iz == z)
                    return islands[i];
            }
            return null;
        }
	}
}

