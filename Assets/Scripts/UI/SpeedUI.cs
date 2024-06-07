using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedUI : MonoBehaviour
{
    PlayerMove playerMoveScript;
    public TextMeshProUGUI speedDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMoveScript = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speedDisplay.text = $"Speed: {playerMoveScript.flatVel.magnitude:F0}";
    }
}
