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
    public List<float> values;
    public List<float> idealValues;
    public List<Color> jaugeColors;
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

            //Rest of your Awake code

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
        for (int i = 0; i < values.Count; i++)
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
        for (int i = 0; i < values.Count; i++)
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
    
    public void AddJauge(Color newColor, float newBaseValue, float newIdealValue)
    {
        int i = jauges.Count;
        values.Add(newBaseValue);
        idealValues.Add(newIdealValue);
        GameObject tmp = Instantiate(meterPrefab, transform);
        tmp.name = "Meter " + i;
        jauges.Add(tmp.transform.Find("Jauge").GetComponent<Image>());
        positivePreview.Add(tmp.transform.Find("Positive").GetComponent<Image>());
        jauges[i].color = newColor;
        jaugeColors.Add(newColor);
        jauges[i].gameObject.name = "Jauge " + i;

        Transform negativeHolder = tmp.transform.Find("NegativeBar");
        negativePreview.Add(negativeHolder.Find("Negative").GetComponent<Image>());
        negativePreviewCover.Add(negativeHolder.Find("Cover").GetComponent<Image>());
        negativePreviewCover[i].color = jaugeColors[i];

        goalIndicators.Add(tmp.transform.Find("Indicator").GetComponent<Image>());
        goalIndicators[i].rectTransform.rotation = Quaternion.Euler(0, 0, -90 - (idealValues[i] / 100) * 360);
        SetValues();
    }

    public void ChangeJauge(int jaugeToChangeIndex, Color newColor, float newBaseValue, float newIdealValue)
    {
        jauges[jaugeToChangeIndex].color = newColor;
        jaugeColors[jaugeToChangeIndex] = newColor;
        values[jaugeToChangeIndex] = newBaseValue;
        idealValues[jaugeToChangeIndex] = newIdealValue;
        positivePreview[jaugeToChangeIndex].fillAmount = newBaseValue / 100;
        negativePreviewCover[jaugeToChangeIndex].fillAmount = newBaseValue / 100;
        negativePreview[jaugeToChangeIndex].fillAmount = newBaseValue / 100;
        goalIndicators[jaugeToChangeIndex].rectTransform.rotation = Quaternion.Euler(0, 0, -90 - (idealValues[jaugeToChangeIndex] / 100) * 360);
        SetValues();
    }

    void SetValues()
    {
        for (int i = 0; i < values.Count; i++)
        {
            jauges[i].fillAmount = values[i] / 100;
        }
    }

    public void PreviewValue(float[] newValues)
    {
        //TogglePreview(true);
        for (int i = 0; i < newValues.Length; i++)
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
        for (int i = 0; i < values.Count; i++)
        {
            surplus += Mathf.Abs(values[i] - idealValues[i]);
        }
        surplus -= surplus > 10 ? 10 : 0; 
        return Mathf.RoundToInt(surplus / (values.Count * 100 / 5));
    }
}
