using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class ChangeValue : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI sliderText;
    [SerializeField] FloatSO sensValueSO;
    PlayerLook playerLook;

    void Start()
    {
        playerLook = FindObjectOfType<PlayerLook>();
        slider.onValueChanged.AddListener((v) =>
        {
            sliderText.text = v.ToString("F1");
            sensValueSO.Value = v;

            playerLook.sensX = sensValueSO.Value;
            playerLook.sensY = sensValueSO.Value;
        });

        if (sensValueSO.Value == 0f)
        {
            sensValueSO.Value = 10f;
        }
    }
}
