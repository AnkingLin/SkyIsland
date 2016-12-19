using System;
using System.Collections.Generic;
using UnityEngine;

namespace EM
{
    public class Ling : MonoBehaviour
    {
        CharacterController cc;

        public Ling(Vector3 pos)
        {
            GameObject ling = new GameObject(this.GetType().ToString());
            ling.AddComponent(this.GetType());
            ling.transform.position = pos;
        }

        private void Start()
        {
            cc = gameObject.AddComponent<CharacterController>();
            cc.radius = 0.4f;
            cc.height = 1.6f;
            cc.stepOffset = 1.0f;
            gameObject.AddComponent<MeshRenderer>().material = Materials.ling;

            ModelPart head = new ModelPart(this, "head");
            head.setOffset(0, 16);
            head.addBoxToMesh(0, 16, 0, 8, 8, 8, 2);
            head.createMesh();

            ModelPart body = new ModelPart(this, "body");
            body.setOffset(16, 0);
            body.addBoxToMesh(0, -4, 0, 8, 12, 4, 2);
            body.createMesh();

            ModelPart leg1 = new ModelPart(this, "leg1");
            leg1.setOffset(0, 0);
            leg1.addBoxToMesh(4, -22, 8, 4, 6, 4, 2);
            leg1.setOffset(0, 0);
            leg1.addBoxToMesh(-4, -22, -8, 4, 6, 4, 2);
            leg1.createMesh();

            ModelPart leg2 = new ModelPart(this, "leg2");
            leg2.setOffset(0, 0);
            leg2.addBoxToMesh(-4, -22, 8, 4, 6, 4, 2);
            leg2.setOffset(0, 0);
            leg2.addBoxToMesh(4, -22, -8, 4, 6, 4, 2);
            leg2.createMesh();

        }
        
        private void Update()
        {
            cc.SimpleMove(Vector3.forward * 5.5f * Time.deltaTime);
        }
    }
}
