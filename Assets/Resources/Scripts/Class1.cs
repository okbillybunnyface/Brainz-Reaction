using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    public static float maxSpeed = 60;
    public float moveSpeed = 5, moveAccel = 50, jumpForce = 2, jumpingAccel = 5;
    protected float probeLength, groundAngle;
    public bool jumping = false;
    private bool grounded;
    private Vector3 groundTangent, normal;
    private GameObject ground, collided;
    protected int originalLayer;
    public Transform probeEnd;

    public virtual void Start()
    {
        originalLayer = gameObject.layer;
        //jumping = false;
    }

    //Makes the character translate left and right relative to the characters current rotation.
    public void Walk(Vector3 input, float delay)
    {
        if (input.sqrMagnitude > 1)
        {
            input.Normalize();
        }

        else if (!jumping)
        {
            //groundAngle is the angle between the normal vector to ground and the transform.up vector
            float maxVelocity = Mathf.Cos(groundAngle * Mathf.PI / 180) * moveSpeed;
            //groundTagent is the vector parallel to the ground
            Vector3 force = 100 * delay * input.normalized * moveAccel * (input.magnitude - Vector3.Dot(groundTangent, rigidbody.velocity) / maxVelocity);
            rigidbody.AddForce(force, ForceMode.Acceleration);

            Debug.DrawRay(this.transform.position, force / 4, Color.blue);
        }
        else
        {
            Vector3 force = 100 * delay * transform.right * jumpingAccel * Vector3.Dot(input, transform.right);
            if (rigidbody.velocity.sqrMagnitude < moveSpeed * moveSpeed || Vector3.Dot(rigidbody.velocity, force) < 0)
            {
                rigidbody.AddForce(force, ForceMode.Acceleration);

                Debug.DrawRay(this.transform.position, force / 4, Color.blue);
            }
        }
    }
}

