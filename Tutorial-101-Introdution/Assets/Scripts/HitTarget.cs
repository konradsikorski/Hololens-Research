using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : MonoBehaviour {
    public GameObject Underworld;
    public GameObject ObjectToHide;

    private void OnCollisionEnter(Collision collision)
    {
        ObjectToHide.SetActive(false);
        Underworld.SetActive(true);

        SpatialMapping.Instance.MappingEnabled = false;
    }
}
