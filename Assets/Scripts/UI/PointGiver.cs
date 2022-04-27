using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointGiver : MonoBehaviour
{
    public RectTransform target;
    public float animDuration;
    public float shrinkDuration;
    GameObject targetGO;
    float speed = 400;
    public Ease animEase;
    int targetIndex;
    bool isNegative;
    Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    public void SetupGiver(int n, bool _isNegative)
    {
        img = GetComponent<Image>();

        targetIndex = n;
        isNegative = _isNegative;

        targetGO = GameObject.Find("Jauge " + n);
        if (targetGO != null)
        {
            target = targetGO.GetComponent<RectTransform>();
            transform.localScale = Vector3.zero;
            transform.SetParent(targetGO.transform.parent.parent.parent);
            transform.DOScale(1, 0.2f).OnComplete(Move);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (img.color == Color.white)
        {
            img.color = ValueHandler.Instance.jaugeColors[targetIndex];
            float add = 25;
            img.color = new Color(img.color.r + add, img.color.g + add, img.color.b + add);
        }
    }

    [ContextMenu("Setcol")]
    public void SetColor()
    {
        img.color = Color.red;

    }

    void Move()
    {
        transform.DOMove(target.position, animDuration).SetEase(animEase).OnComplete(Shrink);
    }

    void Shrink()
    {
        transform.DOScale(0, shrinkDuration).SetEase(animEase).OnComplete(KillMe);
    }

    void KillMe()
    {
        float[] num = new float[ValueHandler.Instance.values.Count];
        for (int i = 0; i < num.Length; i++)
            num[i] = 0;
        num[targetIndex] = isNegative ? -1 : 1;
        ValueHandler.Instance.UpdateValues(num);
        transform.DOKill();
        Destroy(gameObject);
    }
}
