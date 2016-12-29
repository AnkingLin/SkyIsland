using System;
using System.Collections.Generic;
using UnityEngine;

namespace EM
{
	public class Sky:MonoBehaviour
	{
        //岛屿数组
		public List<Island> islands;
        //天空的游戏对象
        public GameObject thisobj;

        /// <summary>
        /// 创建一个天空
        /// </summary>
		public Sky ()
		{
            //初始化
            thisobj = new GameObject("Sky");
            islands = new List<Island> ();
		}

        /// <summary>
        /// 设置泥块
        /// </summary>
        /// <param name="newClod">新的泥块</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        /// <returns>是否可以设置</returns>
        public bool setClod(Clod newClod, int x, int y, int z)
        {
            //计算岛屿和方块在岛屿的坐标
            int ix = (x) >> 4;
            int iz = (z) >> 4;
            int cx = (x) & 15;
            int cy = (y) & 127;
            int cz = (z) & 15;

            //获取岛屿指针
            Island island = getIsland(ix, iz);
            if (island != null)
            {
                //超出限制当然要踢出！当然这不可能！
                if (cx < 0 || cx >= 16 || cy < 0 || cy >= 128 || cz < 0 || cz >= 16)
                    return false;

                //设置。。
                island.clods[cx, cy, cz] = newClod;

                //更新网格，貌似要删掉？
                island.createMesh();

                //更新岛屿网格
                if (cx == 0)
                    getIsland(ix - 1, iz).createMesh();
                else if (cx == 15)
                    getIsland(ix + 1, iz).createMesh();
                if (cz == 0)
                    getIsland(ix, iz - 1).createMesh();
                else if (cz == 15)
                    getIsland(ix, iz + 1).createMesh();

                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取泥块
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        /// <returns>指定坐标的泥块</returns>
        public Clod getClod(int x,int y,int z)
        {
            //同上QAQ
            int ix = (x) >> 4;
            int iz = (z) >> 4;
            int cx = (x) & 15;
            int cy = (y) & 127;
            int cz = (z) & 15;

            //同上QAQ
            Island island = getIsland(ix, iz);
            if (island != null)
            {
                //同上
                if (cx < 0 || cx >= 16 || cy < 0 || cy >= 128 || cz < 0 || cz >= 16)
                    return Clod.Air;
                //不多说。。
                return island.clods[cx, cy, cz];
            }

            return Clod.Air;
        }

        /// <summary>
        /// 添加一个岛屿进天空
        /// </summary>
        /// <param name="x">岛屿坐标X</param>
        /// <param name="z">岛屿坐标Z</param>
        /// <returns>岛屿指针</returns>
        public Island addIsland(int x,int z)
        {

            //如果没有这个岛屿便添加
            if (getIsland(x, z) == null)
            {
                //创建一个新的岛屿对象
                Island island = new Island(this, x, z);

                //加入数组
                islands.Add(island);

                //初始化岛屿（特别提示：千万别把Add跟这些调换，会拖慢运行速度我也不知道为什么QAQ)
                island.buildClod();
                island.createMesh();

                //使岛屿排序变得有规律，加快查找速度
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

        /// <summary>
        /// 获取指定坐标的岛屿指针
        /// </summary>
        /// <param name="x">岛屿坐标X</param>
        /// <param name="z">岛屿坐标Z</param>
        /// <returns>指定坐标的岛屿指针</returns>
        public Island getIsland(int x, int z)
        {
            //小朋友都会的东西QAQ，以后再写二分优化一下
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

