using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour
{
    public Transform orientation;
    [SerializeField] FloatSO sliderSens;
    PauseMenu pauseMenu;
    NextLevel levelEndScript;

    public float sensX;
    public float sensY;

    float xRotation;
	float yRotation;
	
    void Start()
    {
	    sensX = sliderSens.Value;
	    sensY = sliderSens.Value;
	    // Uzamknutí a schování kurzoru
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu = FindObjectOfType<PauseMenu>();
        levelEndScript = FindObjectOfType<NextLevel>();
    }
    
    void Update()
    {
	    if (!pauseMenu.paused && !levelEndScript.levelEnded)
	    {
		    float mouseX = Input.GetAxis("Mouse X") * sensX;
		    float mouseY = Input.GetAxis("Mouse Y") * sensY;
		    
		    yRotation += mouseX;
		    xRotation -= mouseY; // Rotace nahoru a dolů

		    xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Omezení rotace

		    transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		    orientation.rotation = Quaternion.Euler(0, yRotation, 0); // Rotace pouze v rovině
	    }

	    
    }
}