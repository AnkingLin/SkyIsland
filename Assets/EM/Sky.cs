using System;
using System.Collections.Generic;
using UnityEngine;

namespace EM
{
	public class Sky
	{
		public List<Island> islands;

		public Sky ()
		{
			this.islands = new List<Island> ();
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

        public Island addIsland(int x,int z)
        {
            if (getIsland(x, z) == null)
            {
                Island island = new Island(this, x, z);
                this.islands.Add(island);
                island.buildClod();
                island.createMesh();

                this.islands.Sort(delegate(Island a, Island b){
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
            for (int i = 0; i < this.islands.Count; i++)
            {
                int ix = this.islands[i].ix;
                int iz = this.islands[i].iz;

                if (ix != x || iz != z) continue;
                return this.islands[i];
            }
            return null;
        }
	}
}

