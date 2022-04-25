using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EraserLevel : MonoBehaviour
{
    public int[] Levels;
    [SerializeField]
    float fillSpeed;
    [SerializeField]
    Image progressBar;
    int currentLevelIndex = 0;
    int currentExp = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IncreaseExp(int exp)
    {
        if (currentLevelIndex > Levels.Length)
            return;
        currentExp += exp;
        if (currentExp >= Levels[currentLevelIndex])
        {
            currentExp = currentExp - Levels[currentLevelIndex];
            currentLevelIndex++;
        }
        progressBar.DOFillAmount((float)currentExp / (float)Levels[currentLevelIndex], fillSpeed);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
