using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlSpawner : MonoBehaviour
{
    public GameObject anime;
    public int spawnLimit;
    public float spawnDelay;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnGirls());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnGirls()
    {
        int i = 0;
        while (i++ < spawnLimit)
        {
            GameObject tmp = Instantiate(anime);
            tmp.name = anime.name;
            Vector3 pos = transform.position;
            pos += new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
            tmp.transform.position = pos;
            if (spawnDelay > 0.01f)
                spawnDelay = spawnDelay * 0.9f;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
