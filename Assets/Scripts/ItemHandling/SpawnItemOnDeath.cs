using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemOnDeath : MonoBehaviour
{
    public GameObject prefab;
    public int spawnAmount;
    public float minRadius;
    public float maxRadius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnItems()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject tmp = Instantiate(prefab);
            tmp.name = prefab.name;
            tmp.transform.position = transform.position + new Vector3(Random.Range(minRadius, maxRadius), Random.Range(minRadius, maxRadius), Random.Range(minRadius, maxRadius));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
