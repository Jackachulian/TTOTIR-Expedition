using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TTOTIR.Combat;

namespace TTOTIR.Inventory
{
    public class PlayerMovement : MonoBehaviour
{
    // cached rigidbody
    Rigidbody rb;
    // Cached TTOTIR character object.
    Character character;

    [Tooltip("Ground check for the player, to check if the player can jump.")]
    [SerializeField] GameObject groundCheck;

    [Tooltip("The layer mask for the ground.")]
    [SerializeField] LayerMask groundLayer;

    [Tooltip("Factor of acceleration/decelaration while midair.")]
    [SerializeField] float midairAccelFactor = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
    }

    // The input that the player is requesting during the current frame.
    Vector3 input;
   
    Vector3 targetVelocity;
    // The acceleration to be imparted onto the player, pointed from current velocity towards target velocity.
    Vector3 accel;

    // Update is called once per frame
    void Update()
    {
        // ---- Player movement ----
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // get camera-normalized vectors for forward and right, use to direct input
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        input = (forward * verticalInput + right * horizontalInput);
        input = Vector3.ClampMagnitude(input, 1f);

        // debugging
        Debug.DrawRay(transform.position, rb.velocity, Color.blue);
        Debug.DrawRay(transform.position, targetVelocity, Color.magenta);
        Debug.DrawRay(transform.position, accel, Color.red);

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            rb.AddForce(new Vector3(0,10,0), ForceMode.VelocityChange);
        }
    }

    void FixedUpdate()
    {
        if (input.sqrMagnitude < 0.02f) {
            targetVelocity = Vector3.zero;
        } else {
            targetVelocity = input.normalized * character.topSpeed;
        }
        
        // Point acceleration towards target velocity; achieved by subtracting current velocity
        // and setting magnitude to movement accel
        Vector3 offset = targetVelocity - rb.velocity;
        offset.y = 0;

        // only accelerate if not very close to target velocity, to prevent jittering
        if (offset.sqrMagnitude > 0.02f)
        {
            accel = offset.normalized;
            accel.y = 0;

            // use different accel/decel factor if speeding up or slowing down
            // use dot product between current velocity and target velocity to determine
            if (Vector3.Dot(rb.velocity, targetVelocity) > 0) {
                accel *= character.moveAccel;
            } else {
                accel *= character.moveDecel;
            }

            // Slow down dramatically when in midair
            if (!IsGrounded())
            {
                accel *= midairAccelFactor;
            }

            rb.AddForce(accel * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.transform.position, 0.4f, groundLayer);
    }
}

}