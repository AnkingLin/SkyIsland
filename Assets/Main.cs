using UnityEngine;
using System.Collections;
using SkyIsland;

public class Main : MonoBehaviour
{
    Sky sky;
    
    void Start()
    {
        Materials.initMaterials();
        Materials.initTextures();
        sky = new Sky();
        for (int x = -4; x < 4; x++)
        {
            for (int z = -4; z < 4; z++)
            {
                sky.addIsland(x, z);
            }
        }
        new PlayerLing(sky, Vector3.zero);
    }

    void Update()
    {

    }
}

