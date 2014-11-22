using UnityEngine;
using System.Collections;

public class GunmanScript : HumanScript 
{
    public bool shotgun = false;
    protected override void ReactToZombies()
    {
        GameObject closestZombie = zombiesInSight[0].gameObject;
        float distance = (closestZombie.transform.position - transform.position).sqrMagnitude;
        foreach (Collider2D zombie in zombiesInSight)
        {
            float temp = (zombie.transform.position - transform.position).sqrMagnitude;
            if (temp < distance)
            {
                distance = temp;
                closestZombie = zombie.gameObject;
            }
        }

        walkScript.TurnTo(closestZombie);

        if(canAttack) Attack(closestZombie);
    }

    protected override void Attack(GameObject victim)
    {
        float damage = shotgun ? attackDamage / (1 + (victim.transform.position - transform.position).sqrMagnitude) : attackDamage;
        victim.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);

        base.Attack(victim);
    }
}
