using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamInvertColor : MonoBehaviour
{
    public Shader invert;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.RenderWithShader(invert, "RenderType");

    }

    void Invert()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
