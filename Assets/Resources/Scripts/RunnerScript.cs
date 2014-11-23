using UnityEngine;
using System.Collections;

public class RunnerScript : HumanScript
{
    public bool fistFight = false;
    protected override void ReactToZombies()
    {
        Vector3 toZombie = transform.position - closestZombie.transform.position;
        Vector3 toHuman = closestHuman.transform.position - transform.position;
        walkScript.TurnTo(toZombie);
        walkScript.Walk(Vector3.Dot(toZombie, toHuman));
    }
}
