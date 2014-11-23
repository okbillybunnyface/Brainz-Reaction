using UnityEngine;
using System.Collections;

public class GunmanScript : HumanScript 
{
    public bool shotgun = false;
	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

    protected override void ReactToZombies()
    {
        if (closestZombie != null)
        {
            walkScript.TurnTo(closestZombie);

            if (canAttack) Attack(closestZombie);
        }
    }

    protected override void Attack(GameObject victim)
    {
        float damage = shotgun ? attackDamage / (1 + (victim.transform.position - transform.position).sqrMagnitude) : attackDamage;
        victim.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
		if(shotgun)anim.SetTrigger("shotgunAttack");
		else anim.SetTrigger("pistolAttack");

        base.Attack(victim);
    }
}
