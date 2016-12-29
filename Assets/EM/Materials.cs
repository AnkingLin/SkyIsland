using System;
using System.Collections.Generic;
using UnityEngine;

namespace EM
{
    public class Materials
    {
        //一些材质
        public static Material terrain;
        public static Material ling;
        //泥块的贴图参数（单位：像素）
        public static int clodTextureWidth = 1024;
        public static int clodTextureHeight = 1024;
        public static int clodWidth = 32;
        public static int clodHeight = 32;
        //准星材质（你改个画线的可好？）
        public static Texture crosshair;

        /// <summary>
        /// 加载材质
        /// </summary>
        public static void initMaterials()
        {
            terrain = Resources.Load(@"Materials/Terrain") as Material;
            ling = Resources.Load(@"Materials/Ling") as Material;
        }

        /// <summary>
        /// 加载贴图
        /// </summary>
        public static void initTextures()
        {
            crosshair= Resources.Load(@"crosshairalpha") as Texture;
        }

        /// <summary>
        /// 获取泥块材质的X偏移值
        /// </summary>
        /// <param name="textureName">材质名字</param>
        /// <returns></returns>
        public static float getClodTextureOffsetX(string textureName)
        {
            switch (textureName)
            {
                case "Stone":
                    return ((clodWidth * 0f) / clodTextureWidth);

                case "Soil":
                    return ((clodWidth * 1f) / clodTextureWidth);

                case "Grass_Top":
                    return ((clodWidth * 2f) / clodTextureWidth);

                case "Grass_Side":
                    return ((clodWidth * 3f) / clodTextureWidth);
            }

            return 0f;
        }

        /// <summary>
        /// 获取泥块材质的Y偏移值
        /// </summary>
        /// <param name="textureName">材质名字</param>
        /// <returns></returns>
        public static float getClodTextureOffsetY(string textureName)
        {
            switch (textureName)
            {
                case "Stone":
                    return ((clodHeight * 31f) / clodTextureHeight);

                case "Soil":
                    return ((clodHeight * 31f) / clodTextureHeight);

                case "Grass_Top":
                    return ((clodHeight * 31f) / clodTextureHeight);

                case "Grass_Side":
                    return ((clodHeight * 31f) / clodTextureHeight);
            }

            return 0f;
        }
    }
}
