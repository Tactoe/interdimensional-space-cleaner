using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Febucci.UI;
using System;

[Serializable]
public class SpeakerAction {
    public bool MoveX;
    public float MoveValue;
    public bool Flip;
}

public class DialogueReader : MonoBehaviour
{
    Dialogue currentDialogue;
    DialogueNode currentNode;
    [SerializeField]
    Image leftCharacter, rightCharacter, bgImg;
    [SerializeField]
    TextMeshProUGUI nameText, dialogueText, narratorDialogueText;
    [SerializeField]
    CanvasGroup cg;
    [SerializeField]
    TextAnimatorPlayer textAnimator, narratorTextAnimator;
    [SerializeField]
    GameObject DialogueBox;
    [SerializeField]
    GameObject NarratorDialogueBox;
    [SerializeField]
    Dialogue DialogueToPlayOnStart;
    int dialogueIndex;
    bool textFinishedShowing;
    public bool inDialogue;
    // Start is called before the first frame update
    void Start()
    {
        if (DialogueToPlayOnStart != null)
            StartDialogue(DialogueToPlayOnStart);
        //cg.alpha = 0;
    }

    public void ActivateDialogue()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        inDialogue = true;
        textAnimator.onTextShowed.AddListener(delegate{textFinishedShowing = true;});
        narratorTextAnimator.onTextShowed.AddListener(delegate{textFinishedShowing = true;});
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || (inDialogue && Input.GetMouseButtonDown(0)))
        {
            if (!textFinishedShowing)
            {
                textAnimator.SkipTypewriter();
                narratorTextAnimator.SkipTypewriter();
                textFinishedShowing = true;
            }
            else
            {
                textFinishedShowing = false;
                if (dialogueIndex < currentDialogue.dialogue.Count)
                {
                    ReadNewNode(currentDialogue.dialogue[dialogueIndex]);
                }
                else
                {
                    CloseDialogue();
                }
            }
        }
    }

    public void StartDialogue(Dialogue newDial)
    {
        ActivateDialogue();
        currentDialogue = newDial;
        ReadNewNode(currentDialogue.dialogue[dialogueIndex]);
        cg.alpha = 1;
    }

    void ReadNewNode(DialogueNode node)
    {
        if (node.text != "")
        {
            DialogueBox.SetActive(true);
        }
        else
        {
            DialogueBox.SetActive(false);
            NarratorDialogueBox.SetActive(false);
        }

        if (node.speakerName != "")
        {
            NarratorDialogueBox.SetActive(false);
            DialogueBox.SetActive(true);
            nameText.text = node.speakerName;
            if (node.speakerName == "Maro")
            {
                nameText.transform.parent.GetComponent<Image>().color = new Color(22, 0, 27);
                leftCharacter.color = new Color(1, 1, 1);
                rightCharacter.color =  new Color(0.5f, 0.5f, 0.5f);
            }
            else if (node.speakerName == "Cubotron")
            {
                nameText.transform.parent.GetComponent<Image>().color = new Color(252, 228, 182);
                rightCharacter.color = new Color(1, 1, 1);
                leftCharacter.color = new Color(0.5f, 0.5f, 0.5f);
            }
            dialogueText.text = node.text;
        }
        else
        {
            DialogueBox.SetActive(false);
            NarratorDialogueBox.SetActive(true);
            narratorDialogueText.text = node.text;
            rightCharacter.color = new Color(0.5f, 0.5f, 0.5f);
            leftCharacter.color = new Color(0.5f, 0.5f, 0.5f);
        }
        if (node.img != null)
        {
            textFinishedShowing = true;
        }
        HandleImg(node.img, bgImg);
        HandleImg(node.leftSpeaker, leftCharacter, node.speakerActions[0]);
        HandleImg(node.rightSpeaker, rightCharacter, node.speakerActions[1]);
        dialogueIndex++;
    }

    void HandleImg(Sprite img, Image target, SpeakerAction action = null)
    {
        if (img != null)
        {
            target.sprite = img;
            target.gameObject.SetActive(true);
        }
        else
            target.gameObject.SetActive(false);
        if (action != null)
        {
            if (action.Flip)
            {
                if (Mathf.Abs(target.transform.rotation.eulerAngles.y) < 90)
                    target.transform.DOLocalRotate(Vector3.up * 180, 0.5f);
                else
                    target.transform.DOLocalRotate(Vector3.zero, 0.5f);
            }
            if (action.MoveX)
            {
                target.transform.DOLocalMoveX(target.transform.position.x + action.MoveValue, 0.5f);
            }
            // if (action.Move)
            // {
            //     target.transform.DOMoveY(target.transform.position.y - 10, 19);
            // }
        }
    }

    void CloseDialogue()
    {
        textAnimator.onTextShowed.RemoveAllListeners();
        narratorTextAnimator.onTextShowed.RemoveAllListeners();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        TransitionBlock[] tb = FindObjectsOfType<TransitionBlock>(true);
        foreach (TransitionBlock b in tb)
        {
            b.gameObject.SetActive(true);
        }
    }
}
