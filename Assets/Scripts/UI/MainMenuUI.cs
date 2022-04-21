using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    public float waitBeforeFade;
    public float fadeSpeed;
    public float scaleOffset;
    [SerializeField]
    Animator cameraAnim;
    [SerializeField]
    Animator objectiveAnim;
    Animator canvasAnim;
    [SerializeField]
    CanvasGroup cg;

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        canvasAnim = GetComponent<Animator>();
        //transform.localScale = 
        cg.alpha = 0;
        
        StartCoroutine(fadeIn());
    }

    IEnumerator fadeIn()
    {
        yield return new WaitForSeconds(waitBeforeFade);
        transform.DOScale(Vector3.one, fadeSpeed + scaleOffset).SetEase(Ease.InOutSine);
        cg.DOFade(1, fadeSpeed).SetEase(Ease.InOutSine);
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        cg.interactable = false;
        cg.blocksRaycasts = false;
        canvasAnim.SetTrigger("startGame");
        objectiveAnim.SetTrigger("startGame");
        cameraAnim.SetTrigger("startGame");
    }
}
