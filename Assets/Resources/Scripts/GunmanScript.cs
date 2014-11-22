using UnityEngine;
using System.Collections;

public class Gunman : HumanScript 
{
    GameObject closestZombie;

    protected override void ReactToZombies()
    {
        closestZombie = zombiesInSight[0].gameObject;
        float distance = (closestZombie.transform.position - transform.position).sqrMagnitude;
        foreach (Collider2D zombie in zombiesInSight)
        {
            float temp = (zombie.transform.position - transform.position).sqrMagnitude;
            if (temp < distance) distance = temp;
            closestZombie = zombie.gameObject;
        }

        Attack(closestZombie);
    }

    protected override void Attack(GameObject victim)
    {
        walkScript.TurnToTarget(victim);

        float distance = (victim.transform.position - transform.position).sqrMagnitude;
        victim.SendMessage("Damage", attackDamage / (1 + distance), SendMessageOptions.DontRequireReceiver);

        base.Attack(victim);
    }
}
