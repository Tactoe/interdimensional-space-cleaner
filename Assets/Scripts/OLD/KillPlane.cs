using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Erasable"))
        {
            other.transform.position = Vector3.up * 10;
        }
    }
}
