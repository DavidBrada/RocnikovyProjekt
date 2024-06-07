using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using TMPro;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject crosshairsHUD;
    public GameObject optionsMenu;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI sliderText;
    [SerializeField] FloatSO sensValueSO;

    NextLevel nextLevel;

    public bool paused = false;
    public bool inOptions;

    void Start()
    {
        nextLevel = FindObjectOfType<NextLevel>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !nextLevel.levelEnded)
        {
            if (!paused || inOptions)
            {
                Pause();
                inOptions = false;
            }
            else
            {
                Resume();
            }
        }
    }

    void Pause()
    {
        paused = true;
        
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        crosshairsHUD.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void Resume()
    {
        paused = false;
        
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        crosshairsHUD.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void Options()
    {
        inOptions = true;
        slider.value = sensValueSO.Value;
        sliderText.text = sensValueSO.Value.ToString("F1");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
