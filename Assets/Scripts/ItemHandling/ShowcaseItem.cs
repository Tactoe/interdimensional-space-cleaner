using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowcaseItem : MonoBehaviour
{
    public Transform spawnLocation;
    [SerializeField]
    TextMeshProUGUI nameText;
    [SerializeField]
    TextMeshProUGUI ramblingNameText;
    [SerializeField]
    TextMeshProUGUI descriptionText;
    [SerializeField]
    Camera uiCam;

    GameObject currentShowcase;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame


    public void SetNewShowcase(GameObject newShowcase, ItemData data)
    {
        if (currentShowcase != null)
            Destroy(currentShowcase);
        nameText.text = data.itemName;
        ramblingNameText.text = data.itemName;
        if (data.sizeModifier != 0)
            uiCam.orthographicSize = data.sizeModifier;
        else
            setSize(newShowcase.transform);
        if (GameManager.Instance.murderMode)
        {
            StartCoroutine(MoreIsMore());
        }
        else
            descriptionText.text = data.description;
        currentShowcase = setupObjectShowcase(newShowcase);
    }

    IEnumerator MoreIsMore()
    {
        string more = " IS MORE";
        int i = 0;
        nameText.text = "N E V E R  E N D I N G";

        descriptionText.color = Color.red;
        descriptionText.text = "MORE";
        while (true)
        {
            descriptionText.text += more[i];
            i++;
            if (i == more.Length)
                i = 0;
            yield return new WaitForSeconds(0.04f);
        }
    }

    GameObject setupObjectShowcase(GameObject obj)
    {
        GameObject ret = Instantiate(obj, spawnLocation);
        SetAllChildUI(ret);
        ret.layer = 5;
        ret.tag = "Untagged";
        ret.AddComponent<RotateItem>();
        ret.transform.position = spawnLocation.transform.position;
        Transform pivot = ret.transform.Find("Pivot");
        if (pivot != null)
        {
            ret.transform.position = ret.transform.position - pivot.localPosition;
        }
        ret.transform.rotation = Quaternion.Euler(Vector3.left * 10);
        return ret;
    }

    void SetAllChildUI(GameObject ret)
    {
        for (int i = 0; i < ret.transform.childCount; i++)
        {
            ret.transform.GetChild(i).gameObject.layer = 5;
            if (ret.transform.childCount > 0)
            {
                SetAllChildUI(ret.transform.GetChild(i).gameObject);
            }
        }
    }

    void setSize(Transform tf)
    {
        Vector3 scale = tf.localScale;
        uiCam.orthographicSize = Mathf.Max(scale.x, scale.y, scale.z);
        
        for (int i = 0; i < tf.childCount; i++)
        {
            scale = tf.GetChild(i).transform.localScale;
            uiCam.orthographicSize = Mathf.Max(uiCam.orthographicSize, scale.x, scale.y, scale.z);
        }
        if (scale.x == scale.y && scale.x == scale.z)
            uiCam.orthographicSize += 0.08f;
    }
}
