using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressUI : MonoBehaviour
{
    public TextMeshProUGUI targetCounter;
    Progression progressScript;
    
    // Start is called before the first frame update
    void Start()
    {
        progressScript = FindObjectOfType<Progression>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetCounter.text = (progressScript.targetCount - 1).ToString();
        if (progressScript.targetCount == 8)
        {
            targetCounter.color = Color.green;
        }
    }
}
