using System;
using System.Collections.Generic;

namespace EM
{
	public class Sky
	{
		public List<Island> islands;

		public Sky ()
		{
			this.islands = new List<Island> ();
		}

		//二分查找岛屿在数组中的位置
		private int getIslandIndex(int ix,int iz){
			int first = 0, last = this.islands.Count;
			while (first <= last) {
				int mid = (first + last) / 2;
				if (islands [mid].ix < ix || iz < iz)
					first = mid + 1;
				else
					last = mid - 1;
			}
			return first;
		}
	}
}

