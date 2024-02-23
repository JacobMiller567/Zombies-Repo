using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Reference: https://gist.github.com/pppoe252110/068db8a845f817570d9dde87c173d930
public class GunSway : MonoBehaviour
{

    public static GunSway Instance;
    public Transform weaponTransform;

    [Header("Sway Properties")]
    [SerializeField] private float swayAmount = 0.01f;
    [SerializeField] public float maxSwayAmount = 0.5f;
    [SerializeField] public float swaySmooth = 50f;
    [SerializeField] public AnimationCurve swayCurve;

    [Range(0f, 1f)]
    [SerializeField] public float swaySmoothCounteraction = 1f;

    [Header("Rotation")]
    [SerializeField] public float rotationSwayMultiplier = -1f;

    [Header("Position")]
    [SerializeField] public float positionSwayMultiplier = 9f;


    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector2 sway;
    Quaternion lastRot;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Reset()
    {
        Keyframe[] ks = new Keyframe[] { new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 0, 0) };
        swayCurve = new AnimationCurve(ks);
    }

    private void Start()
    {
        if (!weaponTransform)
            weaponTransform = transform;
        lastRot = transform.localRotation;
        initialPosition = weaponTransform.localPosition;
        initialRotation = weaponTransform.localRotation;
    }

    private void Update()
    {
        var angularVelocity = Quaternion.Inverse(lastRot) * transform.rotation;

        float mouseX = FixAngle(angularVelocity.eulerAngles.y) * swayAmount;
        float mouseY = -FixAngle(angularVelocity.eulerAngles.x) * swayAmount;

        lastRot = transform.rotation;

        sway = Vector2.MoveTowards(sway, Vector2.zero, swayCurve.Evaluate(Time.deltaTime * swaySmoothCounteraction * sway.magnitude * swaySmooth));
        sway = Vector2.ClampMagnitude(new Vector2(mouseX, mouseY) + sway, maxSwayAmount);

        weaponTransform.localPosition = Vector3.Lerp(weaponTransform.localPosition, new Vector3(sway.x, sway.y, 0) * positionSwayMultiplier * Mathf.Deg2Rad + initialPosition, swayCurve.Evaluate(Time.deltaTime * swaySmooth));
        weaponTransform.localRotation = Quaternion.Slerp(weaponTransform.localRotation, initialRotation * Quaternion.Euler(Mathf.Rad2Deg * rotationSwayMultiplier * new Vector3(-sway.y, sway.x, 0)), swayCurve.Evaluate(Time.deltaTime * swaySmooth));
    }

    private float FixAngle(float angle)
    {
        return Mathf.Repeat(angle + 180f, 360f) - 180f;
    }
}
