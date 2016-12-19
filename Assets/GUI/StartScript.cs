using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {

    public UILabel thisobj;
    private void Start ()
    {
        thisobj = gameObject.GetComponent<UILabel>();
        thisobj.text = "加载中。。。";
    }

    private void Update()
    {
        GameObject.DestroyObject(this.gameObject.transform.parent.gameObject);
    }
}
