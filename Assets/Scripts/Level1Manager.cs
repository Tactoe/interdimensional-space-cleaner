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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForQuestStatus());
    }

    IEnumerator CheckForQuestStatus()
    {
        while (true)
        {
            print(ValueHandler.Instance.values[2]);
            switch (questStatus)
            {
                case 0:
                    if (ValueHandler.Instance.values[2] <= 10)
                    {
                        questStatus++;
                        FindObjectOfType<DialogueReader>().StartDialogue(Part2Dialogue);
                        FindObjectOfType<SwitchCamera>().IncreaseAnchor();
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
