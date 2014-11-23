using UnityEngine;
using System.Collections;

public class GunmanScript : HumanScript 
{
    public bool shotgun = false;
    protected override void ReactToZombies()
    {
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
