using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switching : MonoBehaviour
{
    public GameObject weaponContainer;
    public GameObject crosshairs;
    PauseMenu pauseMenu;
    NextLevel levelEndScript;

    public int selectedWeapon = 0;
    
    void Start()
    {
        selectedWeapon = 0;
        pauseMenu = FindObjectOfType<PauseMenu>();
        levelEndScript = FindObjectOfType<NextLevel>();
    }
    
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= weaponContainer.transform.childCount -1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = weaponContainer.transform.childCount -1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
            for (int i = 0; i < crosshairs.transform.childCount; i++)
            {
                if (i == selectedWeapon)
                {
                     crosshairs.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    crosshairs.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    void SelectWeapon()
    {
        if (!pauseMenu.paused && !levelEndScript.levelEnded)
        {
            int i = 0;

            foreach (Transform weapon in weaponContainer.transform)
            {
                if (i == selectedWeapon)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }

                i++;
            }
        }
    }
}
