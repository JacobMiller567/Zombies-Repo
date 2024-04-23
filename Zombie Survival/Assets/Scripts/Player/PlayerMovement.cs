using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    private Vector3 velocity;
    public float speed = 2.5f;
    public float runSpeed = 4.5f;
    public float maxStamina = 100f;
    public float staminaRegenRate = 12f;
    public float staminaCost = 20f;
    [SerializeField] private float exhaustedTime = 3f;
    [SerializeField] private float jumpHeight = 4f;
    private float gravity = -9.81f; 
    private float holdSpeed;
    public float currentStamina = 100f; // private
    //public bool isIdle;
    private bool isRunning;
   // private bool isCrouching;
    private bool onGround;
    private bool onCooldown = false;
    [SerializeField] private AudioSource walkAudio, runAudio, exhaustedAudio;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
       // controller = gameObject.GetComponent<CharacterController>();
        holdSpeed = speed;
        currentStamina = maxStamina;

        //walkAudio.enabled = false;
        walkAudio.mute = true;
        runAudio.mute = true;
        //RunAudio.enabled = false;
        //CrouchAudio.enabled = false;
    }

    void Update()
    {
        onGround = controller.isGrounded;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * horizontal + transform.forward * vertical).normalized;
        if (move != Vector3.zero)
        {
            controller.Move(move * speed * Time.deltaTime);
            //walkAudio.enabled = true;
            walkAudio.mute = false;

            if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && !onCooldown)
            {
                currentStamina -= staminaCost * Time.deltaTime;
                speed = runSpeed;
                runAudio.mute = false;
                walkAudio.mute = true;
            }
            else
            {
                speed = holdSpeed;
                walkAudio.mute = false;
                runAudio.mute = true;
            }

            if (currentStamina < maxStamina)//&& !onCooldown) // TEST with cooldown
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
            }

            if (currentStamina <= 0 && !onCooldown)
            {
                onCooldown = true;
                StartCoroutine(StaminaCooldown());
            }
        }
        else
        {
            //walkAudio.enabled = false;
            walkAudio.mute = true;
            runAudio.mute = true;
            if (currentStamina < maxStamina)//&& !onCooldown) // TEST with cooldown
            {
                currentStamina += (staminaRegenRate + 4f) * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
            }
        }

        if (onGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private IEnumerator StaminaCooldown()
    {
        if (!exhaustedAudio.isPlaying)
        {
            exhaustedAudio.Play();
        }
        yield return new WaitForSeconds(exhaustedTime);
        onCooldown = false;
    }

    public void IncreaseSpeed(float spd, float stamina)
    {
        holdSpeed += spd;
        runSpeed += spd;
        maxStamina += stamina;
    }


}

