using System;
using System.Collections.Generic;
using UnityEngine;

namespace EM
{
    public class Materials
    {
        public static Material terrain;
        public static Material ling;
        public static int clodTextureWidth = 1024;
        public static int clodTextureHeight = 1024;
        public static int clodWidth = 32;
        public static int clodHeight = 32;

        public static void initMaterials()
        {
            terrain = Resources.Load(@"Materials/Terrain") as Material;
            ling = Resources.Load(@"Materials/Ling") as Material;
        }

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
