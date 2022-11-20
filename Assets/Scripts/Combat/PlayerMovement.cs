using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class PlayerMovement : MonoBehaviour
{
    // cache rigidbody
    Rigidbody rb;
    // cahce character
    Character character;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
    }

    Vector3 input;
    Vector3 targetVelocity;
    Vector3 accel;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // GetComponent<Renderer>().material.color = new Color(horizontalInput*0.5f + 0.5f, 0, verticalInput*0.5f + 0.5f, 1);

        // get camera-normalized vectors for forward and right
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        input = (forward * verticalInput + right * horizontalInput);

        // debugging
        Debug.DrawRay(transform.position, rb.velocity, Color.blue);
        Debug.DrawRay(transform.position, targetVelocity, Color.magenta);
        Debug.DrawRay(transform.position, accel, Color.red);
    }

    void FixedUpdate()
    {
        if (input.sqrMagnitude < 0.01f) {
            targetVelocity = Vector3.zero;
        } else {
            targetVelocity = input.normalized * character.topSpeed;
        }
        

        // Point acceleration towards target velocity; achieved by subtracting current velocity
        // and setting magnitude to movement accel
        Vector3 offset = targetVelocity - rb.velocity;
        offset.y = 0;

        // only accelerate if not very close to target velocity, to prevent jittering
        if (offset.sqrMagnitude > 0.01f)
        {
            // use different accel/decel value if speeding up or slowing down
            // use dot product between current velocity and target velocity to determine
            if (Vector3.Dot(rb.velocity, targetVelocity) > 0) {
                accel = offset.normalized * character.moveAccel;
            } else {
                accel = offset.normalized * character.moveDecel;
            }
            accel.y = 0;

            rb.AddForce(accel * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
