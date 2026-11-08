using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -10f;
    public float stepInterval = 0.5f;

    private float stepTimer = 0f;
    private CharacterController controller;
    private Vector3 velocity;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                AudioManager.Instance.PlaySFX("jump");
            }
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        velocity.y += gravity * Time.deltaTime;
        Vector3 finalMovement = (move * speed) + velocity;
        controller.Move(finalMovement * Time.deltaTime);

        if (move.magnitude > 0.1 && controller.isGrounded)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer < 0f)
            {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX("walking");
                }
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}
