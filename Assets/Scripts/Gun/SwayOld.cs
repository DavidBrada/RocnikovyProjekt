using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayOld : MonoBehaviour
{
    public float smooth;
    public float swayMultiplier;

    float mouseX;
    float mouseY;
    
    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;
        
    }

    void LateUpdate()
    {
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);
        
        Quaternion targetRotation = rotationX * rotationY;
        
        
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
