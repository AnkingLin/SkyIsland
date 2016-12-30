using UnityEngine;

namespace SkyIsland
{
    public class LingScript : MonoBehaviour
    {
        public Sky sky;
        public Ling ling;
        public CharacterController cc;
        public int height;
        
        private ModelPart head;
        private ModelPart body;
        private ModelPart arm1;
        private ModelPart arm2;
        private ModelPart leg1;
        private ModelPart leg2;

        Vector3 armDirection;//手臂摆动方向
        Vector3 legDirection;//腿摆动方向

        private int rightArmAngles;//右手摆动角度
        private int leftArmAngles;//左手摆动角度
        
        private int rightLegAngles;//右手摆动角度
        private int leftLegAngles;//左手摆动角度

        private void Start()
        {
            height = 32;
            cc = gameObject.AddComponent<CharacterController>();
            cc.radius = 0.4f;
            cc.height = (height / 32f);
            cc.center = Vector3.up * (height / 32 / 2f);
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

            armDirection = Vector3.right;
            legDirection = Vector3.left;
        }

        private void Update()
        {
            cc.SimpleMove(Vector3.forward * 5.5f * Time.deltaTime);

            //腿摆动
            rightLegAngles = (int)leg1.thisobj.transform.eulerAngles.x;
            leftLegAngles = (int)leg2.thisobj.transform.eulerAngles.x;

            if (rightLegAngles == 30)
            {
                legDirection = Vector3.left;
            }
            else
                if (rightLegAngles == 330)
            {
                legDirection = Vector3.right;
            }

            leg2.thisobj.transform.RotateAround(leg2.getRotPoint(), legDirection, -1f);
            leg1.thisobj.transform.RotateAround(leg1.getRotPoint(), legDirection, 1f);
            
            //手臂摆动
            rightArmAngles = (int)arm1.thisobj.transform.eulerAngles.x;
            leftArmAngles = (int)arm2.thisobj.transform.eulerAngles.x;

            if (rightArmAngles == 30)
            {
                armDirection = Vector3.left;
            }
            else
                if (rightArmAngles == 330)
            {
                armDirection = Vector3.right;
            }

            arm1.thisobj.transform.RotateAround(arm1.getRotPoint(), armDirection, 1f);
            arm2.thisobj.transform.RotateAround(arm2.getRotPoint(), armDirection, -1f);
        }
    }
}
