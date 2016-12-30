using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkyIsland
{
    public class Ling : MonoBehaviour
    {
        //因为这个类的构架不确定，所以暂且不打注释（明明是你懒。。。）
        public Sky sky;
        public LingScript ls;
        public int height;

        public Ling(Sky sky, Vector3 pos)
        {
            this.sky = sky;
            height = 32;
                
            GameObject ling = new GameObject(this.GetType().ToString());
            string classname = GetType().ToString() + "Script";
            ls = ling.AddComponent(Type.GetType(classname)) as LingScript;
            ls.ling = this;
            ls.sky = sky;
            ling.transform.position = pos;
        }
    }
}
