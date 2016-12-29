using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM
{
    public class Clod
    {
        // 静态指针，方便调用
        public static Clod Air = new Clod(false);
        public static StoneClod Stone = new StoneClod();
        public static GrassClod Grass = new GrassClod();
        public static SoilClod Soil = new SoilClod();

        /// <summary>
        /// 是不是正常的方块
        /// </summary>
        public bool isNormal;


        /// <summary>
        /// 创建一个泥块
        /// </summary>
        /// <param name="isNormal">是不是正常的呢？</param>
        public Clod(bool isNormal)
        {
            this.isNormal = isNormal;
        }

        /// <summary>
        /// 泥块的渲染函数（为何不是Create？写gl习惯了。。）
        /// </summary>
        /// <param name="island">岛屿指针</param>
        /// <param name="x">坐标X</param>
        /// <param name="y">坐标Y</param>
        /// <param name="z">坐标Z</param>
        public virtual void createMesh(Island island,int x,int y,int z)
        {

        }
    }
}
