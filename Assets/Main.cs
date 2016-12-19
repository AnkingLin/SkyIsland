using UnityEngine;
using System.Collections;
using EM;

public class Main : MonoBehaviour
{
    //一下全是垃圾代码QAQ
    
    CharacterController cc;
    Sky sky;

    // Use this for initialization
    void Start()
    {
        Materials.initMaterials();
        sky = new Sky();
        for (int x = -4; x < 4; x++)
        {
            for (int z = -4; z < 4; z++)
            {
                sky.addIsland(x, z);
            }
        }

        cc = transform.parent.gameObject.GetComponent<CharacterController>();
    }

    float rotationY = 0F;

    bool isJump = false;
    float jumpY = 0.00f;

    Clod putClod = Clod.Stone;
    void Update()
    {

        float rotationX = transform.parent.localEulerAngles.y + Input.GetAxis("Mouse X") * 8f;

        rotationY += Input.GetAxis("Mouse Y") * 8f;
        rotationY = Mathf.Clamp(rotationY, -80f, 80f);

        gameObject.transform.parent.localEulerAngles = new Vector3(0, rotationX, 0);
        gameObject.transform.localEulerAngles = new Vector3(-rotationY, gameObject.transform.localEulerAngles.y, 0);


        Vector3 move = new Vector3(0f, 0f, 0f);

        if (Input.GetKey(KeyCode.W))
        {
            move += (cc.gameObject.transform.rotation * Vector3.forward * Time.deltaTime) * (cc.isGrounded ? 4.5f : 3.5f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            move += (cc.gameObject.transform.rotation * Vector3.back * Time.deltaTime) * (cc.isGrounded ? 4.5f : 3.5f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            move += (cc.gameObject.transform.rotation * Vector3.left * Time.deltaTime) * (cc.isGrounded ? 4.5f : 3.5f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            move += (cc.gameObject.transform.rotation * Vector3.right * Time.deltaTime) * (cc.isGrounded ? 4.5f : 3.5f);
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

        cc.Move(move);

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
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
            new Ling(gameObject.transform.position + new Vector3(0f, 2f, 0f));
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            transform.parent.Translate(Vector3.up * 100f);
        }
    }

    public Texture2D crosshairImage;

    void OnGUI()
    {
        float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
        float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
    }
}

