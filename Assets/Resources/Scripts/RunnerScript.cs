using UnityEngine;
using System.Collections;

public class RunnerScript : HumanScript
{
    public bool fistFight = false;
    protected override void ReactToZombies()
    {
        Vector3 toZombie = (closestZombie != null) ? transform.position - closestZombie.transform.position : Vector3.zero;
        Vector3 toHuman = (closestHuman != null) ? closestHuman.transform.position - transform.position : Vector3.zero;
        walkScript.TurnTo(toZombie);
        walkScript.Walk(Vector3.Dot(toZombie, toHuman));
    }
}
