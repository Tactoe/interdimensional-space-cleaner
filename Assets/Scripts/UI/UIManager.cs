using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject examineItemCanvas;
    [SerializeField]
    GameObject finalScoreUI;
    [SerializeField]
    ShowcaseItem showcase;
    [SerializeField]
    GameObject showcaseCamera;
    GameObject rtGO;
    GameObject lastObjectChecked;
    public GameObject[] glist;
    public bool inMenu = false;
    public Dictionary<string, bool> activeMenus;


    void Start()
    {
        RamblingText rt = examineItemCanvas.GetComponentInChildren<RamblingText>(true);
        if (rt != null)
            rtGO = rt.gameObject;
        //activeMenus = new Dictionary<string, bool>();
        //foreach (GameObject child in GetComponentsInChildren<GameObject>())
        //{
        //    if (child.transform.parent != null)
        //    {
        //        activeMenus.Add(child.name, child.activeInHierarchy);
        //    }
        //}
    }

    public void ActivateItemCanvas(GameObject toShowcase, ItemData data)
    {
        inMenu = true;
        showcaseCamera.SetActive(true);
        examineItemCanvas.SetActive(true);
        //rateButton.SetActive(false);
        lastObjectChecked = toShowcase;
        if (!GameManager.Instance.murderMode && data.rambling != null && data.rambling.Length > 0)
        {
            RamblingText rt =  examineItemCanvas.GetComponentInChildren<RamblingText>(true);
            rt.currentRambling = data.rambling;
            rtGO.SetActive(true);
        }

        TeleportBehindYou tp = toShowcase.GetComponent<TeleportBehindYou>();
        if (tp != null)
            tp.canTeleport = true;

        //if (!GameManager.Instance.murderMode)
        //    ValueHandler.Instance.PreviewValue(data.values);
        showcase.SetNewShowcase(toShowcase, data);
    }

    public void TurnOffItemCanvas()
    {
        inMenu = false;
        showcaseCamera.SetActive(false);
        examineItemCanvas.SetActive(false);
        rtGO.SetActive(false);
        if (!GameManager.Instance.murderMode)
        {
            TeleportBehindYou tp = null;
            GameObject tmp = GameObject.Find("Anime Block");
            ValueHandler.Instance.TogglePreview(false);
            if (tmp != null)
            {
                tp = tmp.GetComponent<TeleportBehindYou>();
                tp.tryTeleport();
            }
        }
    }

    public void DitchObject()
    {
        if (GameManager.Instance.murderMode && lastObjectChecked.name != "MURDERBLOCK DELTA")
        {
            TurnOffItemCanvas();
            return;
        }
        //ValueHandler.Instance.UpdateValues(lastObjectChecked.GetComponent<ItemData>().values);
        if (lastObjectChecked != null)
        {
            lastObjectChecked.GetComponent<ItemData>().DitchObject();
            Destroy(lastObjectChecked);
        }
        glist = GameObject.FindGameObjectsWithTag("Erasable");

        if (GameObject.FindGameObjectsWithTag("Erasable").Length - 2 <= 0)
        {
            GameObject dio = GameObject.Find("DioramaSupport");
            dio.GetComponent<ItemData>().enabled = true;
            dio.tag = "Erasable";
            
        }
        TurnOffItemCanvas();
    }

    public void ShowFinalScore()
    {
        inMenu = true;
        int score = 5 - ValueHandler.Instance.EvaluateIsland();
        finalScoreUI.SetActive(true);
        finalScoreUI.GetComponent<FinalScore>().SetFinalScore(score);
    }

    // Update is called once per frame

}
