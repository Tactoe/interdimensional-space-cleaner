using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehindYou : MonoBehaviour
{
    public GameObject positionParent;
    List<Transform> positions;
    public bool canTeleport;
    public Material trigred;
    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Transform>();
        Transform[] tmpPositions = positionParent.GetComponentsInChildren<Transform>(true);
        foreach (Transform p in tmpPositions)
        {
            if (p.transform.childCount == 0)
            {
                positions.Add(p);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void tryTeleport()
    {
        if (canTeleport)
        {
            ItemData data = gameObject.GetComponent<ItemData>();
            data.description = "You blinked, didn't you.";
            Trigred(data);
            int index = Random.Range(0, positions.Count);
            transform.position = positions[index].position;
        }
        else
        {
            if (GameObject.Find("Hamster") == null && GameObject.Find("Hamster Ball") == null)
                canTeleport = true;
        }

    }

    public void Trigred(ItemData data)
    {
        if (GameObject.Find("Hamster") == null && GameObject.Find("Hamster Ball") == null)
        {
            gameObject.GetComponent<Renderer>().material = trigred;
            data.itemName = "You murdered Colonel";
            data.description = "you absolute monster.";
            data.rambling = new string[0];
        }
    }
}
