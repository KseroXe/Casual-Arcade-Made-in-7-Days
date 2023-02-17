using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefs : MonoBehaviour
{
    public int level;
    public float currentExp;
    public float expForNextLevel;
    public AudioClip enemyDeath;

    private AudioSource audioSource;
    private UIManager uiManager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        uiManager = GetComponent<UIManager>();

        level = 1;
        currentExp = 0;
        expForNextLevel = 20;
    }
    public void GainExp(float gain)
    {
        audioSource.PlayOneShot(enemyDeath);
        currentExp += gain;
        if (currentExp >= expForNextLevel)
        {
            currentExp = 0;
            level++;
            expForNextLevel += 10;
            uiManager.ShowUpgrades();
        }
        uiManager.UpdateExp();
    }
}
