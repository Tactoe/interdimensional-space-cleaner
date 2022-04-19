using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwitchCamera : MonoBehaviour
{
    public float animDuration;
    public Ease animeEase;
    int rotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RotateCam()
    {
        rotation += 180;
        transform.parent.transform.DORotate(new Vector3(0, rotation, 0), animDuration).SetEase(animeEase);
        if (rotation == 360)
            rotation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
