using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ValueHandler : MonoBehaviour
{
    [SerializeField]
    GameObject meterPrefab;
    public static ValueHandler Instance;
    public float scaleUpValue = 1.2f;
    public float scaleUpTime = 0.1f;
    public float[] values;
    public float[] idealValues;
    public Color[] jaugeColors;
    List<Image> positivePreview;
    List<Image> negativePreview;
    List<Image> negativePreviewCover;
    List<Image> jauges;
    List<Image> goalIndicators;
    AudioSource[] audioSrc;


    void Awake()
    {

        if (Instance == null)
        {

            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        audioSrc = GetComponents<AudioSource>();
        positivePreview = new List<Image>();
        negativePreview = new List<Image>();
        negativePreviewCover = new List<Image>();
        jauges = new List<Image>();
        goalIndicators = new List<Image>();

        SetupLevel();
        TogglePreview(false);
    }

    void SetupLevel()
    {
        for (int i = 0; i < values.Length; i++)
        {
            GameObject tmp = Instantiate(meterPrefab, transform);
            tmp.name = "Meter " + i;
            jauges.Add(tmp.transform.Find("Jauge").GetComponent<Image>());
            positivePreview.Add(tmp.transform.Find("Positive").GetComponent<Image>());
            jauges[i].color = jaugeColors[i];
            jauges[i].gameObject.name = "Jauge " + i;

            Transform negativeHolder = tmp.transform.Find("NegativeBar");
            negativePreview.Add(negativeHolder.Find("Negative").GetComponent<Image>());
            negativePreviewCover.Add(negativeHolder.Find("Cover").GetComponent<Image>());
            negativePreviewCover[i].color = jaugeColors[i];

            goalIndicators.Add(tmp.transform.Find("Indicator").GetComponent<Image>());
            goalIndicators[i].rectTransform.rotation = Quaternion.Euler(0, 0, -90 - (idealValues[i] / 100) * 360);
        }
        SetValues();
    }

    public void UpdateValues(float[] newValues)
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (newValues[i] != 0)
            {
                jauges[i].transform.parent.transform.DORewind();
                jauges[i].transform.parent.transform.DOPunchScale(Vector3.one * scaleUpValue, scaleUpTime, 10);
                if (newValues[i] > 0)
                    audioSrc[0].Play();
                else
                    audioSrc[1].Play();
            }
            values[i] = Mathf.Clamp(values[i] + newValues[i], 0, 100);
            if (newValues[i] > 0)
                positivePreview[i].fillAmount += newValues[i] / 100;
            else
            {
                negativePreviewCover[i].fillAmount += newValues[i] / 100;
                negativePreview[i].fillAmount += newValues[i] / 100;
            }
        }
        SetValues();
    }

    void SetValues()
    {
        for (int i = 0; i < values.Length; i++)
        {
            jauges[i].fillAmount = values[i] / 100;
        }
    }

    public void PreviewValue(float[] newValues)
    {
        //TogglePreview(true);
        for (int i = 0; i < values.Length; i++)
        {
            if (newValues[i] > 0)
            {
                positivePreview[i].gameObject.SetActive(true);
                positivePreview[i].fillAmount = (values[i] + newValues[i]) / 100;
            }
            else if (newValues[i] < 0)
            {
                negativePreview[i].gameObject.SetActive(true);
                negativePreview[i].fillAmount = (values[i]) / 100;
                negativePreviewCover[i].gameObject.SetActive(true);
                negativePreviewCover[i].fillAmount = (values[i] + newValues[i]) / 100;
            }
        }
    }

    public void TogglePreview(bool status)
    {
        for (int i = 0; i < positivePreview.Count; i++)
        {
            positivePreview[i].gameObject.SetActive(status);
            negativePreview[i].gameObject.SetActive(status);
            negativePreviewCover[i].gameObject.SetActive(status);
        }
    }

    public int EvaluateIsland()
    {
        float surplus = 0;
        for (int i = 0; i < values.Length; i++)
        {
            surplus += Mathf.Abs(values[i] - idealValues[i]);
        }
        surplus -= surplus > 10 ? 10 : 0; 
        return Mathf.RoundToInt(surplus / (values.Length * 100 / 5));
    }
}
