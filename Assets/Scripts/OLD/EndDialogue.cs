using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndDialogue : MonoBehaviour
{
    public float fadeDuration;
    CanvasGroup cg;
    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        Invoke("ShowFinalText", 20f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShowFinalText()
    {
        cg.DOFade(1, fadeDuration);
    }
}
