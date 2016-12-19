using UnityEngine;
using System.Collections;

public class LogoScript : MonoBehaviour
{

    private void Start()
    {

    }

    public int time = 0;
    private void Update()
    {
        time++;
        if (time > 250)
        {
            Application.LoadLevel("Main");
            GameObject.DestroyObject(this.gameObject);
        }
    }

    private void FixedUpdate()
    {

        if (transform.position.z >= 0f)
            transform.Translate(new Vector3(0f, 0f, -1f * Time.deltaTime));
    }
}
