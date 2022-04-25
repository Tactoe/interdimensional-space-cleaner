using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwitchCamera : MonoBehaviour
{
    public float animDuration;
    public Ease animeEase;
    int rotation = 0;
    int spotIndex = 0;
    [SerializeField]
    Transform[] spotAnchors;
    int spotAnchorIndex = 0;
    List<Transform> spots;
    // Start is called before the first frame update
    void Start()
    {
        BuildSpotList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchCam();
        }        
    }

    public void IncreaseAnchor()
    {
        spotIndex = 0;
        spotAnchorIndex++;
        BuildSpotList();
    }

    void BuildSpotList()
    {
        spots = new List<Transform>();
        foreach (Transform cam in spotAnchors[spotAnchorIndex])
        {
            spots.Add(cam);   
        }
        SwitchCam();
    }

    public void SwitchCam()
    {
        transform.DOMove(spots[spotIndex].position, animDuration);
        transform.DORotate(spots[spotIndex].rotation.eulerAngles, animDuration, RotateMode.FastBeyond360);
        spotIndex++;
        if (spotIndex >= spots.Count)
            spotIndex = 0;

    }

    public void RotateCam()
    {
        rotation += 180;
        transform.parent.transform.DORotate(new Vector3(0, rotation, 0), animDuration).SetEase(animeEase);
        if (rotation == 360)
            rotation = 0;
    }

}
