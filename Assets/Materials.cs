using System;
using System.Collections.Generic;
using UnityEngine;

namespace EM
{
    public class Materials
    {
        public static Material mutou;

        public static void initMaterials()
        {
            mutou = Resources.Load(@"Materials/mutou") as Material;
        }
    }
}
