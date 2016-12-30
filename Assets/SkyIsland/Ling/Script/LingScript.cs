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

        Vector3 fx = Vector3.right;

        private float armAmplitude = 60;//手臂摆动幅度

        private int rightArmDirection;//右手摆动方向 1向前摆，2向后摆
        private int rightArmAngles;//右手摆动角度

        private int leftArmDirection;//左手摆动方向 1向前摆，2向后摆
        private int leftArmAngles;//左手摆动角度



        private void Start()
        {
            height = 32;
            cc = gameObject.AddComponent<CharacterController>();
            cc.radius = 0.4f;
            cc.height = (height / 32f);
            cc.center = Vector3.up * (height / 32 / 2);
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

            rightArmDirection = 1;
            leftArmDirection = 2;
        }

        private void Update()
        {

            //cc.SimpleMove(Vector3.forward * 5.5f * Time.deltaTime);

            if (leg1.thisobj.transform.localEulerAngles.x > 40f)
            {
                fx = Vector3.left;
            }
            else
            {
                fx = Vector3.right;
            }

            // leg2.thisobj.transform.RotateAround(leg2.getRotPoint(), fx, 2.5f);
            //leg1.thisobj.transform.RotateAround(leg1.getRotPoint(), -fx, 2.5f);

            leg1.thisobj.transform.localEulerAngles = new Vector3(Mathf.Clamp(leg1.thisobj.transform.localEulerAngles.x, -50f, 50f), 0f, 0f);
            leg2.thisobj.transform.localEulerAngles = new Vector3(Mathf.Clamp(leg2.thisobj.transform.localEulerAngles.x, -50f, 50f), 0f, 0f);

            //手臂摆动
            rightArmAngles = (int)arm1.thisobj.transform.eulerAngles.x;
            RightSwing();

            leftArmAngles = (int)arm2.thisobj.transform.eulerAngles.x;
            LeftSwing();
            print("右手旋转的角度:" + rightArmAngles + "左手旋转的角度：" + leftArmAngles);
            //arm2.thisobj.transform.RotateAround(arm2.getRotPoint(), Vector3.right, 1f);
        }
        /// <summary>
        /// 右手摆动
        /// </summary>
        private void RightSwing()
        {
            //判断手臂摆动方向
            if (rightArmAngles == 30)
            {
                rightArmDirection = 2;
            }
            else
                if (rightArmAngles == 330)
            {
                rightArmDirection = 1;
            }
            //向前摆
            if (rightArmDirection == 1)
            {
                arm1.thisobj.transform.RotateAround(arm1.getRotPoint(), Vector3.right, 1f);
            }
            //向后摆
            if (rightArmDirection == 2)
            {
                arm1.thisobj.transform.RotateAround(arm1.getRotPoint(), Vector3.left, 1f);
            }
        }

        /// <summary>
        /// 左手摆动
        /// </summary>
        private void LeftSwing()
        {
            //判断手臂摆动方向
            if (leftArmAngles == 150)
            {
                leftArmDirection = 2;
            }
            else
                if (leftArmAngles == 210)
            {
                leftArmDirection = 1;
            }
            //向前摆
            if (leftArmDirection == 1)
            {
                arm2.thisobj.transform.RotateAround(arm2.getRotPoint(), Vector3.left, 1f);
            }
            //向后摆
            if (leftArmDirection == 2)
            {
                arm2.thisobj.transform.RotateAround(arm2.getRotPoint(), Vector3.right, 1f);
            }
            print("方向：" + (leftArmDirection == 1 ? "向前" : "向后"));
        }
    }
}
