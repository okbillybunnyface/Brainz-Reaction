using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class WalkScript : MonoBehaviour 
{
    public float maxSpeed = 1f;
    private float speed;
    public float moveAccel = 1f;
    public float turnSpeed = 15f;
    private GameObject target;
    private bool seeking = false;

    void Start()
    {
        StartCoroutine(Walking());
    }

    void FixedUpdate()
    {

    }

    public void SeekTarget(GameObject target)
    {
        this.target = target;
        if (!seeking)
        {
            seeking = true;
            StartCoroutine(Seeking());
        }
    }

    private void Walk(float speed)
    {
        this.speed = speed;
        if (speed > maxSpeed) speed = maxSpeed;
    }

    IEnumerator Walking()
    {
        while (true)
        {
            Vector2 force1 = speed * transform.right * moveAccel * (1 - Vector2.Dot(transform.right, rigidbody2D.velocity) / maxSpeed);
            Vector2 force2 = transform.up * moveAccel * (0 - Vector2.Dot(transform.up, rigidbody2D.velocity) / maxSpeed);
            rigidbody2D.AddForce(force1 + force2);

            speed = 0;

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator Seeking()
    {
        while (seeking)
        {
            Vector3 toTarget = target.transform.position - transform.position;
            transform.right = Vector3.Lerp(transform.right, toTarget, Time.deltaTime * turnSpeed);

            Walk(speed);

            yield return new WaitForFixedUpdate();
        }
    }
}