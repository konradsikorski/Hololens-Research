using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCommands : MonoBehaviour {

	// Use this for initialization
	void OnSelect()
    {
        Debug.Log("Sphere.OnSelect");

        if(GetComponent<Rigidbody>() == null)
        {
            var body = this.gameObject.AddComponent<Rigidbody>();
            body.mass = 0.5f;
        }
    }
}
