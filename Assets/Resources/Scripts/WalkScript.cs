using UnityEngine;
using System.Collections;

public class WalkScript : MonoBehaviour 
{
    public float speed = 1f;
    public float turnSpeed = 15f;
    private GameObject target;
    private bool seeking = false;


    void FixedUpdate()
    {

    }

    public void SeekTarget(GameObject target)
    {
        this.target = target;
        if (!seeking)
        {
            seeking = true;
            StartCoroutine(Seek());
        }
    }

    private void Walk(float speed)
    {
        transform.position = transform.position + transform.right * Time.deltaTime * speed;
    }

    IEnumerator Seek()
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