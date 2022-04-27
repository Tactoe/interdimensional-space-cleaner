using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Particulator : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.Particle[] particles;
    public GameObject toInstantiate;
    public GameObject canvasGO;
    Canvas canvas;
    RectTransform canvasRect;
    public int targetJauge = 0;
    public bool isNegative;
    // Start is called before the first frame update
    void Start()
    {
        canvasGO = FindObjectOfType<Canvas>().gameObject;
        canvas = canvasGO.GetComponent<Canvas>();
        canvasRect = canvasGO.GetComponent<RectTransform>();
        ps = GetComponent<ParticleSystem>();
        if (particles == null || particles.Length < ps.main.maxParticles)
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
        StartCoroutine("checkParticles", 1f);

    }

    private void Update()
    {
    }

    IEnumerator checkParticles()
    {
        while (true)
        {
            int numParticlesAlive = ps.GetParticles(particles);
            for (int i = 0; i < numParticlesAlive; i++)
            {
                if (particles[i].remainingLifetime <= 0.1f)
                {
                    EmitUIBit(particles[i].position);
                }
            }
            yield return new WaitForSeconds(0.098f);

        }

    }

    void EmitUIBit(Vector3 pos)
    {
        Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector2 uiPos = mainCam.WorldToScreenPoint(pos);
        GameObject tmp = Instantiate(toInstantiate, canvas.transform);

        tmp.transform.position = uiPos;
        tmp.GetComponent<Image>().color = ValueHandler.Instance.jaugeColors[targetJauge];
        tmp.GetComponent<PointGiver>().SetupGiver(targetJauge, isNegative);
        //Destroy(gameObject);
    }

}
