using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UPANDUP : MonoBehaviour
{
    public float upDuration;
    public float Speed;
    public float peak;
    bool UP;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Vector3.forward * 150);
        transform.DORotate(Vector3.zero , upDuration, RotateMode.FastBeyond360);
        transform.DOMove(transform.position + Vector3.up * peak, upDuration);
    }

// Update is called once per frame
    void Update()
    {
        
    }
}
