                                                                                                                using UnityEngine;
using System.Collections;

public class WalkScript : MonoBehaviour 
{
    public float maxSpeed = 1f;
    private float speed;
    public float moveAccel = 1f;
    public float turnSpeed = 15f;
    private GameObject target = null;
    private bool seeking = false;

    void Start()
    {
        StartCoroutine(Walking());
    }

    void FixedUpdate()
    {

    }

    public void StopSeeking()
    {
        this.target = null;
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
        this.speed = (speed > maxSpeed) ? maxSpeed : speed;
    }

    public void TurnToTarget(GameObject target)
    {
        Vector3 toTarget = target.transform.position - transform.position;
        transform.right = Vector3.Lerp(transform.right, toTarget, Time.deltaTime * turnSpeed);
    }

    IEnumerator Walking()
    {
        while (true)
        {
            Vector2 force1 = transform.right * moveAccel * (speed - Vector2.Dot(transform.right, rigidbody2D.velocity) / maxSpeed);
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
            TurnToTarget(target);

            Walk(maxSpeed);

            yield return new WaitForFixedUpdate();

            if (this.target == null) seeking = false;
        }
    }
}