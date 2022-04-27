using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraLerpAndZoom : MonoBehaviour
{
    [SerializeField]
    float zoomSpeed, lerpSpeed;
    [SerializeField]
    float minFoV, maxFoV;
    float currentZoom;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        currentZoom = cam.fieldOfView;
        
    }

    // Update is called once per frame
    void Update()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, currentZoom, Time.deltaTime * lerpSpeed);
        currentZoom += Input.mouseScrollDelta.y * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minFoV, maxFoV);
        
    }
}
