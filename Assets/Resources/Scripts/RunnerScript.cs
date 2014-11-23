using UnityEngine;
using System.Collections;

public class RunnerScript : HumanScript
{
    protected override void ReactToZombies()
    {
        GameObject closestZombie = zombiesInSight[0].gameObject;

        Vector3 direction = transform.position - closestZombie.transform.position;
        walkScript.TurnTo(direction);
        walkScript.Walk(walkScript.maxSpeed);
    }
}
