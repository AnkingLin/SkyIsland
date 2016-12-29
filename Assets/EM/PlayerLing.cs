using UnityEngine;

namespace EM
{
    public class PlayerLing : Ling
    {
        //一大堆看得懂的变量（构架问题不注释）
        private float rotationY = 0F;
        private bool isJump = false;
        private float jumpY = 0.00f;
        private Clod putClod = Clod.Stone;
        private GameObject cameraObj;

        public PlayerLing(Sky sky, Vector3 pos) : base(sky, pos)
        {
            
        }

        private void Start()
        {
            gameObject.transform.Translate(Vector3.up * 100);

            height = 52;

            cc = gameObject.AddComponent<CharacterController>();
            cc.radius = 0.35f;
            cc.height = (height / 32f);
            cc.center = Vector3.up * (height / 32 / 2);
            cc.stepOffset = 0.5f;

            cameraObj = GameObject.Find("Main Camera");
            cameraObj.transform.position = gameObject.transform.position + Vector3.up * 1.5f;
            cameraObj.transform.parent = gameObject.transform;
        }

        private void Update()
        {
            float rotationX = gameObject.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 8f;

            rotationY += Input.GetAxis("Mouse Y") * 8f;
            rotationY = Mathf.Clamp(rotationY, -80f, 80f);

            gameObject.transform.localEulerAngles = new Vector3(0, rotationX, 0);
            cameraObj.transform.localEulerAngles = new Vector3(-rotationY, gameObject.transform.localEulerAngles.y, 0);

            Vector3 move = new Vector3(0f, 0f, 0f);

            if (Input.GetKey(KeyCode.W))
            {
                move += (Vector3.forward * Time.deltaTime) * (cc.isGrounded ? 4.5f : 3.5f);
            }

            if (Input.GetKey(KeyCode.S))
            {
                move += (Vector3.back * Time.deltaTime) * (cc.isGrounded ? 4.5f : 3.5f);
            }

            if (Input.GetKey(KeyCode.A))
            {
                move += (Vector3.left * Time.deltaTime) * (cc.isGrounded ? 4.5f : 3.5f);
            }

            if (Input.GetKey(KeyCode.D))
            {
                move += (Vector3.right * Time.deltaTime) * (cc.isGrounded ? 4.5f : 3.5f);
            }

            if (Input.GetKey(KeyCode.Space) && cc.isGrounded)
            {
                isJump = true;
            }

            if (isJump && (jumpY <= 1.5f))
            {
                move += (Vector3.up * 10.0f * Time.deltaTime);
                jumpY += 5.5f * Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpY = 0.00f;
            }

            move += (Vector3.down * 4.5f * Time.deltaTime);
            move = gameObject.transform.rotation * move;

            cc.Move(gameObject.transform.TransformDirection(move));

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                RaycastHit hit;
                Ray ray = cameraObj.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
                if (Physics.Raycast(ray, out hit, 10f))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 p = hit.point;
                        p -= hit.normal / 4f;
                        sky.setClod(Clod.Air, Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y), Mathf.RoundToInt(p.z));
                    }


                    if (Input.GetMouseButtonDown(1))
                    {
                        Vector3 p = hit.point;
                        p += hit.normal / 4f;
                        sky.setClod(putClod, Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y), Mathf.RoundToInt(p.z));
                    }

                    if (Input.GetMouseButtonDown(2))
                    {
                        Vector3 p = hit.point;
                        p -= hit.normal / 4f;
                        putClod = sky.getClod(Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y), Mathf.RoundToInt(p.z));
                    }

                }
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                new Ling(sky, gameObject.transform.position + new Vector3(0f, 2f, 0f));
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                transform.parent.Translate(Vector3.up * 100f);
            }
        }


        private void OnGUI()
        {
            float xMin = (Screen.width / 2) - (Materials.crosshair.width / 2);
            float yMin = (Screen.height / 2) - (Materials.crosshair.height / 2);
            GUI.DrawTexture(new Rect(xMin, yMin, Materials.crosshair.width, Materials.crosshair.height), Materials.crosshair);
        }
    }
}
