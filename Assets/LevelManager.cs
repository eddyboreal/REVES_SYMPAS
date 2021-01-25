using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Canvas LevelScore = default;
    public int nbEmennemiesKilled = 0;

    public int TotalEnnemies = default;
    public Canvas ScoreCanvas = default;

    private GameObject PlayerUI = default;


    void Start()
    {
        PlayerUI = GameObject.FindGameObjectWithTag("PlayerUI");
    }

    void Update() 
    {
        if (nbEmennemiesKilled == TotalEnnemies) 
        {
            PlayerUI.GetComponent<Animator>().SetBool("displayOut", true);
            PlayerUI.GetComponent<Animator>().speed = PlayerUI.GetComponent<Animator>().speed * (1f / Time.timeScale);
            ScoreCanvas.gameObject.SetActive(true);
            Debug.Log("Display score");
            TotalEnnemies = 0; 
        }
    }
}