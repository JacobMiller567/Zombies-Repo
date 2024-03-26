using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    private Vector3 currentRotation, targetRotation, targetPosition, currentPosition, initalGunPosition;
    public Transform cam;
    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;
    [SerializeField] private float kickBack;

    public float snappiness, returnAmount, decreasedRecoil;

    private float runTimeRecoilX, runTimeRecoilY, runTimeRecoilZ;

    private void Start()
    {
       initalGunPosition = transform.localPosition;
       runTimeRecoilX = recoilX;
       runTimeRecoilY = recoilY;
       runTimeRecoilZ = recoilZ;
    }

    private void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnAmount * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
        cam.localRotation = Quaternion.Euler(currentRotation); // TEST
        SnapBack();
    }

    public void ApplyRecoil()
    {
        runTimeRecoilX = recoilX;
        runTimeRecoilY = recoilY;
        runTimeRecoilZ = recoilZ;

        if (Shooting.Instance.isZooming) // Decrease recoil when aiming
        {
            runTimeRecoilX *= decreasedRecoil;
            runTimeRecoilY *= decreasedRecoil;
            runTimeRecoilZ *= decreasedRecoil;
        }
        
        targetPosition -= new Vector3(0, 0, kickBack);
        targetRotation += new Vector3(runTimeRecoilX, Random.Range(-runTimeRecoilY, runTimeRecoilY), Random.Range(-runTimeRecoilZ, runTimeRecoilZ));
    }
    void SnapBack()
    {
        targetPosition = Vector3.Lerp(targetPosition, initalGunPosition, Time.deltaTime * returnAmount);
        currentPosition = Vector3.Lerp(currentPosition, targetPosition, Time.fixedDeltaTime * snappiness);
        transform.localPosition = currentPosition;
    }
}
