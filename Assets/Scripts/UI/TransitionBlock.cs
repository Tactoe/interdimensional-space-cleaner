using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransitionBlock : MonoBehaviour
{
    public float animSpeed;
    RectTransform rt;
    public int spawnLimit;
    public int index = 1;
    public bool goingUp;
    public bool goingLeft;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        transform.localScale = Vector3.zero;
        transform.DOScale(1, animSpeed).OnComplete(SpawnNew);
    }

    void SpawnNew()
    {
        if (index < spawnLimit)
        {
            GameObject tmp = Instantiate(gameObject, transform.parent);
            if (index % 6 != 0 || index == 0)
            {
                tmp.GetComponent<RectTransform>().anchoredPosition = rt.anchoredPosition + rt.sizeDelta.y * (goingUp ? Vector2.up : Vector2.down);
            }
            else
            {
                goingUp = !goingUp;
                tmp.GetComponent<RectTransform>().anchoredPosition = rt.anchoredPosition + rt.sizeDelta.x * (goingLeft ? Vector2.right : Vector2.left);
            }
            tmp.GetComponent<TransitionBlock>().index = index + 1;
            tmp.GetComponent<TransitionBlock>().goingUp = goingUp;

        }
        else
        {
            if (goingLeft)
                GameManager.Instance.NextScene();
        }
    }

}
