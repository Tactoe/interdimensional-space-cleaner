using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hamster : MonoBehaviour
{
    public Vector3 C = Vector3.zero; // Sphere center
    public float R; // Sphere radius
    public Rigidbody atomRigidbody; // Rigidbody of the atom
    float radius;

    void Start()
    {
        radius = transform.parent.localScale.x;
        atomRigidbody = GetComponent<Rigidbody>();
        //radius -= radius / 10;
    }

    void FixedUpdate()
    {
        if ((C - transform.position).sqrMagnitude > radius * radius)
        {
            atomRigidbody.velocity = Vector3.Reflect(atomRigidbody.velocity, transform.position);
            // Put the atom slightly inside the sphere so that it doesn't collide right after
            atomRigidbody.position = C + (transform.position - C).normalized * R * 0.999f;
        }
    }
}
