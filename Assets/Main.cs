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
        sky.addLing(new PlayerLing(sky),new Vector3(0, 10, 0));
    }

    void Update()
    {

    }
}

