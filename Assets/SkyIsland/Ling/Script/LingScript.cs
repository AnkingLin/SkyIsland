using UnityEngine;

namespace SkyIsland
{
    public class LingScript : MonoBehaviour
    {
        public Sky sky;
        public Ling ling;
        public CharacterController cc;

        private ModelPart head;
        private ModelPart body;
        private ModelPart arm1;
        private ModelPart arm2;
        private ModelPart leg1;
        private ModelPart leg2;

        private void Start()
        {
            cc = gameObject.AddComponent<CharacterController>();
            cc.radius = 0.4f;
            cc.height = (ling.height / 32f);
            cc.center = Vector3.up * (ling.height / 32 / 2);
            cc.stepOffset = 0.5f;

            gameObject.AddComponent<MeshRenderer>().material = Materials.ling;

            head = new ModelPart(ling, "head");
            head.setOffset(0, 16);
            head.addBoxToMesh(-4, 24, -4, 8, 8, 8, 1);
            head.createMesh();

            body = new ModelPart(ling, "body");
            body.setOffset(16, 0);
            body.addBoxToMesh(-4, 12, -2, 8, 12, 4, 1);
            body.createMesh();

            arm1 = new ModelPart(ling, "arm1");
            arm1.setOffset(40, 0);
            arm1.addBoxToMesh(4, 12, -2, 4, 12, 4, 1);
            arm1.setRotPoint(2, 22, 0);
            arm1.createMesh();

            arm2 = new ModelPart(ling, "arm2");
            arm2.setOffset(40, 0);
            arm2.addBoxToMesh(-8, 12, -2, 4, 12, 4, 1);
            arm2.setRotPoint(-4, 22, 0);
            arm2.createMesh();

            leg1 = new ModelPart(ling, "leg1");
            leg1.setOffset(0, 0);
            leg1.addBoxToMesh(0, 0, -2, 4, 12, 4, 1);
            leg1.setRotPoint(-2, 10, 0);
            leg1.createMesh();

            leg2 = new ModelPart(ling, "leg2");
            leg2.setOffset(0, 0);
            leg2.addBoxToMesh(-4, 0, -2, 4, 12, 4, 1);
            leg2.setRotPoint(-2, 10, 0);
            leg2.createMesh();
        }

        private void Update()
        {
            Vector3 fx = Vector3.right;

            cc.SimpleMove(Vector3.forward * 5.5f * Time.deltaTime);

            if (leg1.thisobj.transform.localEulerAngles.x > 40f)
            {
                fx = Vector3.left;
            }
            else
            {
                fx = Vector3.right;
            }

            leg2.thisobj.transform.RotateAround(leg2.getRotPoint(), fx, 2.5f);
            leg1.thisobj.transform.RotateAround(leg1.getRotPoint(), -fx, 2.5f);

            leg1.thisobj.transform.localEulerAngles = new Vector3(Mathf.Clamp(leg1.thisobj.transform.localEulerAngles.x, -50f, 50f), 0f, 0f);
            leg2.thisobj.transform.localEulerAngles = new Vector3(Mathf.Clamp(leg2.thisobj.transform.localEulerAngles.x, -50f, 50f), 0f, 0f);

            arm1.thisobj.transform.RotateAround(arm1.getRotPoint(), Vector3.right, 2.5f);
            arm2.thisobj.transform.RotateAround(arm2.getRotPoint(), Vector3.left, 2.5f);
        }
    }
}
