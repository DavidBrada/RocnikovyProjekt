using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider slider;
    [SerializeField] TextMeshProUGUI sliderText;
    [SerializeField] FloatSO sliderSens;

    void Start()
    {
        slider.value = sliderSens.Value;
        sliderText.text = sliderSens.Value.ToString("F1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
