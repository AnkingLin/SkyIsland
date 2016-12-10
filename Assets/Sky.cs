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

        public bool deleteIsland(int x,int z)
        {
            if (getIsland(x, z) != null)
            {
                this.islands.RemoveAt(getIslandIndex(x, z));
                return true;
            }


            return false;
        }

        public Island getIsland(int x, int z)
        {
            if (getIslandIndex(x, z)<this.islands.Count)
            {
                if (this.islands[getIslandIndex(x, z)] != null)
                {
                    return this.islands[getIslandIndex(x, z)];
                }
            }
            return null;
        }

        //二分查找岛屿在数组中的位置
        private int getIslandIndex(int ix, int iz)
        {
            int first = 0, last = this.islands.Count - 1;
            while (first <= last)
            {
                int mid = (first + last) / 2;
                if (this.islands[mid].ix < ix || this.islands[mid].iz < iz)
                    first = mid + 1;
                else
                    last = mid - 1;
            }

            Debug.Log(first);
            return first;
        }
	}
}

