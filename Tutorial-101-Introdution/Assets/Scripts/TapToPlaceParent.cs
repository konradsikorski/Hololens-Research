using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlaceParent : MonoBehaviour {
    bool _placing = false;

	// Use this for initialization
	void OnSelect()
    {
        Debug.Log("Scene.OnSelect");
        _placing = !_placing;

        SpatialMapping.Instance.DrawVisualMeshes = _placing;
    }
	
	// Update is called once per frame
	void Update () {
        if (!_placing) return;

        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hit;
        if(Physics.Raycast(headPosition, gazeDirection, out hit, 30f, SpatialMapping.PhysicsRaycastMask))
        {
            this.transform.parent.position = hit.point;

            var old = new Quaternion(0, Camera.main.transform.localRotation.y, 0, 0);
            var rotation = Camera.main.transform.localRotation;
            rotation.z = 0;
            rotation.x = 0;

            this.transform.parent.rotation = rotation;
        }
	}
}
