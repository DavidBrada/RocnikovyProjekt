using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NextLevel : MonoBehaviour
{
    public GameObject levelEndUI;
    public GameObject hud;
    public bool levelEnded = false;

    TimeUI timeUi;
    Progression progressScript;

    public TextMeshProUGUI completionTime;
    public TextMeshProUGUI eliminationCount;
    public TextMeshProUGUI targetCount;

    void Start()
    {
        timeUi = FindObjectOfType<TimeUI>();
        progressScript = FindObjectOfType<Progression>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShowEndScreen();
        }
    }

    void ShowEndScreen()
    {
        Time.timeScale = 0f;
        levelEnded = true;
        hud.SetActive(false);
        levelEndUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        completionTime.text = timeUi.timerText.text;
        eliminationCount.text = Convert.ToString(progressScript.eliminationCount);
        targetCount.text = Convert.ToString(progressScript.targetCount);

    }
}
