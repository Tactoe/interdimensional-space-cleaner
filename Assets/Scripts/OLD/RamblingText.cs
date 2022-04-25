using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RamblingText : MonoBehaviour
{
    public GameObject ramblingTextbox;
    public string[] currentRambling;

    int ramblingIndex = 0;
    TextMeshProUGUI rambling;
    bool isTalking;
    public Sprite[] sprites;
    Image buttonSprite;
    // Start is called before the first frame update
    void Start()
    {
        buttonSprite = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateRambling();
        }
    }

    public void UpdateRambling()
    {
        if (isTalking)
        {
            if (ramblingIndex + 1 < currentRambling.Length)
            {
                ramblingIndex++;
                rambling.text = currentRambling[ramblingIndex];
            }
            else
            {
                CloseRambling();
            }
        }
        else
        {
            buttonSprite.sprite = sprites[1];
            isTalking = true;
            rambling = ramblingTextbox.GetComponentInChildren<TextMeshProUGUI>();
            rambling.text = currentRambling[ramblingIndex];
            ramblingTextbox.SetActive(true);
        }
    }

    void CloseRambling()
    {
        buttonSprite.sprite = sprites[0];
        isTalking = false;
        ramblingIndex = 0;
        //currentRambling = new string[0];
        rambling.text = "";
        ramblingTextbox.SetActive(false);

    }
}
