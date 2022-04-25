using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionHolder : MonoBehaviour
{
    CanvasGroup cg;
    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
