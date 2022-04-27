using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    [SerializeField]
    Dialogue Part2Dialogue;
    [SerializeField]
    GameObject Part2;
    int questStatus = 0;
    [SerializeField]
    Color Jauge2NewColor;
    [SerializeField]
    Color newJaugeColor;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForQuestStatus());
    }

    IEnumerator CheckForQuestStatus()
    {
        while (true)
        {
            switch (questStatus)
            {
                case 0:
                    if (ValueHandler.Instance.values[2] <= 10)
                    {
                        questStatus++;
                        FindObjectOfType<DialogueReader>().StartDialogue(Part2Dialogue);
                        FindObjectOfType<SwitchCamera>().IncreaseAnchor();
                        ValueHandler.Instance.ChangeJauge(2, Jauge2NewColor, 50, 20);
                        ValueHandler.Instance.AddJauge(newJaugeColor, 50, 20);
                        Part2.SetActive(true);
                    }
                    break;
                case 1:
                    if (ValueHandler.Instance.values[0] <= 10)
                        questStatus++;
                    break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
