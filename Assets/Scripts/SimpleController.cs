using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class SimpleController : MonoBehaviour {
    public float speedRotation = 3.0f;
    public float speedMove = 150.0f;
    private GameObject usingItem;

	void Start ()
    {
		
	}
	

	void Update ()
    {
		transform.Rotate(0f, Input.GetAxis("Horizontal") * Time.deltaTime * speedMove, 0f);
        transform.Translate (0f, 0f, Input.GetAxis ("Vertical") * Time.deltaTime * speedRotation);
	}

    public void Used ()
    {
        usingItem = null;
    }
}
