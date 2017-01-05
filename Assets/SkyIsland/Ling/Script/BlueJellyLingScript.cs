using UnityEngine;

namespace SkyIsland
{
    public class BlueJellyLingScript : LingScript
    {
        private ModelPart head;
        private ModelPart leg1;

        void Start()
        {
            height = 28;
            cc = gameObject.AddComponent<CharacterController>();
            cc.radius = 0.4f;
            cc.height = (height / 32f);
            cc.center = Vector3.up * (height / 32f / 2);
            cc.stepOffset = 0.5f;


            gameObject.AddComponent<MeshRenderer>().material = Materials.ling;
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = Materials.blueJellyLingTexture;

            head = new ModelPart(ling, "head");
            head.setOffset(0, 16);
            head.addBoxToMesh(-4, 12, -4, 8, 8, 8, 1);
            head.createMesh();

            leg1 = new ModelPart(ling, "leg1");
            leg1.setOffset(0, 0);
            leg1.addBoxToMesh(0, 0, 0, 4, 12, 4, 1);
            leg1.createMesh();
        }

        void Update()
        {
            cc.SimpleMove(Vector3.forward);
        }
    }
}
