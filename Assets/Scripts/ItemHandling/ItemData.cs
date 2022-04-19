using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public string itemName;
    [TextArea(5, 6)]
    public string description;
    public float[] values;
    [TextArea(5, 10)]
    public string[] rambling;
    public GameObject deathParticles;
    
    public float sizeModifier = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Erasable";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DitchObject()
    {
        SpawnItemOnDeath deathSpawner = gameObject.GetComponent<SpawnItemOnDeath>();
        if (deathSpawner != null)
            deathSpawner.SpawnItems();
        int i = 0;
        foreach (float value in values)
        {
            if (value != 0)
            {
                ParticleSystem ps = deathParticles.GetComponent<ParticleSystem>();
                deathParticles.GetComponent<Particulator>().targetJauge = i;
                deathParticles.GetComponent<Particulator>().isNegative = (value < 0);

                var main = ps.main;
                var emission = ps.emission;
                emission.rateOverTime = Mathf.Abs(value) + 1;
                var trail = ps.GetTrails();
                //var sub = ps.subEmitters.GetSubEmitterSystem(0);
                //var subMain = sub.main;
                main.startColor = ValueHandler.Instance.jaugeColors[i];
                //subMain.startColor = ValueHandler.Instance.jaugeColors[i];

                GameObject tmp = Instantiate(deathParticles);
                tmp.transform.position = transform.position;
                tmp.GetComponent<ParticleSystem>().Play();
            }
            i++;
        }
    }
}