using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreTxt;
    [SerializeField]
    TextMeshProUGUI scoreCommentTxt;
    public bool killedWife;
    public bool killedHamster;
    public bool killedDad;
    public bool additionalComment;
    //Dictionary<string, bool> killIndex;

    string[] finalQuotes;
    // Start is called before the first frame update
    void Start()
    {
        finalQuotes = new string[5];
        finalQuotes[0] = "You're just here to see me suffer right\n";
        finalQuotes[1] = "I don't think that was really helpful. ";
        finalQuotes[2] = "I'm not really sure what you were on about here. ";
        finalQuotes[3] = "Clearly, you seem to know what you're doing! ";
        finalQuotes[4] = "You're a natural-born space cleaner! ";
    }

    public void TriggerFinal(bool isRetrying)
    {
        GameManager.Instance.HandleEndScene(isRetrying);
    }

    public void SetFinalScore(int score)
    {
        finalQuotes = new string[5];
        finalQuotes[0] = "You're just here to see me suffer right\n";
        finalQuotes[1] = "I don't think that was really helpful. ";
        finalQuotes[2] = "I'm not really sure what you were on about here. ";
        finalQuotes[3] = "Clearly, you seem to know what you're doing! ";
        finalQuotes[4] = "You're a natural-born space cleaner! ";

        scoreTxt.text = "Final score: " + score + "/5";

        string finalQuote = "";
        killedWife = GameObject.Find("Anime Block") == null;
        killedHamster = GameObject.Find("Hamster") == null && GameObject.Find("Hamster Ball") == null;
        killedDad = GameObject.Find("CuboStatue") == null;
        if (killedWife)
            finalQuote += "You killed my wife... ";
        if (killedHamster)
        {
            if (killedWife)
            {
                finalQuote += "And her hamster (although I won't miss him much)...";
                GameManager.Instance.murderMode = true;
            }
            else
                finalQuote += "You killed my wife's hamster (although I won't miss him much)...";
        }
        if (killedDad)
            finalQuote += "You desacrated an intemporal memorial to my glorious father...";
        if (finalQuote.Length > 0)
        {
            additionalComment = true;
            if (score >= 4)
                finalQuote += "But you know what, ";
            else
                finalQuote += "And on top of that ";
            finalQuote += finalQuotes[score - 1].ToLower();
        }
        else
            finalQuote += finalQuotes[score - 1];


        if (additionalComment)
        {
            if (score >= 4)
                finalQuote += "I'm so glad you were here to help me see what really mattered!!";
            else
                finalQuote += "Please leave and never come back.";
        }
        else
        {
            if (score >= 4)
                finalQuote += "I must admit I'm pretty impressed that you managed to do all this without removing any item that really mattered to me! Heavens knows what would have happened if you did!";
            else
                finalQuote += "Maybe try again and remove more stuff this time?";
        }
        scoreCommentTxt.text = finalQuote;
    }
}
