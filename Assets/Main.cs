using UnityEngine;
using System.Collections;
using EM;

public class Main : MonoBehaviour
{
    CharacterController cc;
    Sky sky;

    // Use this for initialization
    void Start ()
	{
        Materials.initMaterials();
        sky = new Sky();
        for(int x = -4; x < 4; x++)
        {
            for(int z = -4; z < 4; z++)
            {
                sky.addIsland(x, z);
            }
        }
        //搞两个Island

        cc = transform.parent.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(0f, 0f, 0f);
        if (Input.GetKey(KeyCode.W))
        {
            move += (transform.rotation * Vector3.forward * Time.deltaTime * 4.5f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            move += (transform.rotation * Vector3.back * Time.deltaTime * 4.5f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            move += (transform.rotation * Vector3.left * Time.deltaTime * 4.5f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            move += (transform.rotation * Vector3.right * Time.deltaTime * 4.5f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cc.gameObject.transform.Rotate(Vector3.down);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            cc.gameObject.transform.Rotate(Vector3.up);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.Rotate(Vector3.left);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.Rotate(Vector3.right);
        }

        cc.Move(move);


        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.transform.TransformDirection(Vector3.forward), out hit, 10f))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 p = hit.point;
                    p -= hit.normal / 4;
                    sky.setClod(Clod.Air, (int)p.x, (int)p.y, (int)p.z);
                }


                if (Input.GetMouseButtonDown(1))
                {
                    Vector3 p = hit.point;
                    p += hit.normal / 4;
                    sky.setClod(Clod.Stone, (int)p.x, (int)p.y, (int)p.z);
                }

            }
        }
    }

    void FixedUpdate()
    {

        if (!cc.isGrounded)
            cc.Move(new Vector3(0f, -0.233f * Time.deltaTime * 15f, 0f));

    }
}

