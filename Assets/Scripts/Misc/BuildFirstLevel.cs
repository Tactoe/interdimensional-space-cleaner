using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuildFirstLevel : MonoBehaviour
{
    List<Vector3> originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = new List<Vector3>();

        foreach (Transform child in transform)
        {
            originalScale.Add(child.localScale);
            child.localScale = Vector3.zero;
        }

        StartCoroutine(Pop());
        
    }

    IEnumerator Pop()
    {
        int i = 0;
        foreach (Transform child in transform)
        {
            child.DOScale(originalScale[i], 1).SetEase(Ease.InElastic).OnComplete(delegate {

                //print("wo");
            });
            //yield return new WaitForSeconds(0.01f);
            i++;
        }
            yield return new WaitForSeconds(0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
