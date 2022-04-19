using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAndSkip : MonoBehaviour
{
    public GameObject quad;
    public float quadDelay;
    public float skipDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Invoke("ShowQuad", quadDelay);
    }

    void ShowQuad()
    {
        quad.SetActive(true);
        Invoke("NextScene", skipDelay);
    }

    void NextScene()
    {
        GameManager.Instance.NextScene();
    }
}
