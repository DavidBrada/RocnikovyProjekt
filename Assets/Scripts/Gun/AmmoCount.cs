using System;
using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    Gun gunScript;
    public TextMeshProUGUI ammoLeft;

    void Start()
    {
        gunScript = GetComponent<Gun>();
    }

    void FixedUpdate()
    {
        ammoLeft.text = $"{gunScript.bulletsLeft / gunScript.bulletsPerTap}";
    }
}
