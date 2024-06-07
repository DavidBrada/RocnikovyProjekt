using Unity.VisualScripting;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    [Header("Reference")]
    public Transform recoilPosition;
    public Transform rotationPoint;

    [Header("Speed settings")]
    public float positionRecoilSpeed;
    public float rotationRecoilSpeed;
    [Space]
    public float positionReturnSpeed;
    public float rotationReturnSpeed;

    [Header("Amount settings")]
    public Vector3 recoilRotation = new Vector3(10, 5, 7);
    public Vector3 recoilKickBack = new Vector3(0.015f, 0f, -0.2f);

    [Space]

    Vector3 rotationRecoil;
    Vector3 positionRecoil;
    Vector3 rotation;

    void FixedUpdate()
    {
        rotationRecoil = Vector3.Lerp(rotationRecoil, Vector3.zero, rotationReturnSpeed * Time.deltaTime);
        positionRecoil = Vector3.Lerp(positionRecoil, Vector3.zero, positionReturnSpeed * Time.deltaTime);
        
        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionRecoil, positionRecoilSpeed * Time.deltaTime);
        rotation = Vector3.Slerp(rotation, rotationRecoil, rotationRecoilSpeed * Time.fixedDeltaTime);
        rotationPoint.localRotation = Quaternion.Euler(rotation);
    }

    public void StartGunRecoil()
    {
        rotationRecoil += new Vector3(-recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), Random.Range(-recoilRotation.z, recoilRotation.z));
        positionRecoil += new Vector3(Random.Range(-recoilKickBack.x, recoilKickBack.x), Random.Range(-recoilKickBack.y, recoilKickBack.y), Random.Range(-1.5f, recoilKickBack.z));
    }
    
    
}
