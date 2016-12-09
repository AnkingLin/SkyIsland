using UnityEngine;
using System.Collections;
using EM;

public class Main : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Island i = new Island (0, 0);
		i.addBoxToMesh (0, 0, 0, 0.5f, 0.5f, 0.5f);
		i.addBoxToMesh (1, 0, 0, 1, 1, 1);
		i.createMesh ();

		//搞两个盒子
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

