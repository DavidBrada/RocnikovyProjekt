using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Sway : MonoBehaviour
{
    
    [Header("Sway")]
    public float swayMultiplier = 0.01f; // násobí input z myši
    public float maxDistance = 0.06f; // maximální vzdálenost od nuly
    Vector3 swayPos; // určení pozice zbraně
    
    Vector2 lookInput;

    [Header("Sway rotation")]
    public float rotationSwayMultiplier = 4f; // Násobi input z myši
    public float maxRotation = 5f; // Maximální rotace od nuly
    [Space]
    public float xRotMultiplier = 1f;
    public float yRotMultiplier = 1f;
    public float zRotMultiplier = 1f;
    Vector3 swayEulerRotation;

    float smooth = 10f;
    float smoothRotation = 12f;
    
    void Update()
    {
        GetInput();
        
        WeaponSway();
        SwayRotation();
        
        InitiateSway();
    }

    void GetInput()
    {
        lookInput.x = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        lookInput.y = Input.GetAxisRaw("Mouse Y") * swayMultiplier;
    }

    void WeaponSway()
    {
        Vector3 invertLook = lookInput * -swayMultiplier;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxDistance, maxDistance);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxDistance, maxDistance);

        swayPos = invertLook;
    }

    void SwayRotation()
    {
        Vector2 invertLook = lookInput * -rotationSwayMultiplier;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotation, maxRotation);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxRotation, maxRotation);

        swayEulerRotation = new Vector3(invertLook.y * xRotMultiplier, invertLook.x * yRotMultiplier, invertLook.x * zRotMultiplier); // (rotace nahoru a dolů, rotace doprava a doleva, rotace objektu okolo vlastní osy) new Vector3(invertLook.y, invertLook.x, invertLook.x * 2f)
    }

    void InitiateSway()
    {
        //pozice
        transform.localPosition = Vector3.Lerp(transform.localPosition, swayPos, smooth * Time.deltaTime);
        
        //rotace
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(swayEulerRotation), smoothRotation * Time.deltaTime);
    }
}
