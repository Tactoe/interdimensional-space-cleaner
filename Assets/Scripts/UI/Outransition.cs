using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Outransition : MonoBehaviour
{
    public float animSpeed;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(0, animSpeed).SetAutoKill();
    }

}
