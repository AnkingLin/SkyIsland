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
        sky.addLing(new PlayerLing(),new Vector3(0, 10, 0));
    }

    void Update()
    {

    }
}

