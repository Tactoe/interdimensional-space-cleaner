using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwitchCamera : MonoBehaviour
{
    public float animDuration;
    public Ease animeEase;
    int rotation = 0;
    int index = 0;
    [SerializeField]
    Transform spotAnchor;
    List<Transform> spots;
    // Start is called before the first frame update
    void Start()
    {
        spots = new List<Transform>();
        foreach (Transform cam in spotAnchor)
        {
            spots.Add(cam);   
        }
        SwitchCam();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchCam();
        }        
    }

    public void SwitchCam()
    {
        transform.DOMove(spots[index].position, animDuration);
        transform.DORotate(spots[index].rotation.eulerAngles, animDuration, RotateMode.FastBeyond360);
        index++;
        if (index >= spots.Count)
            index = 0;

    }

    public void RotateCam()
    {
        rotation += 180;
        transform.parent.transform.DORotate(new Vector3(0, rotation, 0), animDuration).SetEase(animeEase);
        if (rotation == 360)
            rotation = 0;
    }

}
