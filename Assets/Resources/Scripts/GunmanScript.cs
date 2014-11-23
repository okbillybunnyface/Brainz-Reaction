using UnityEngine;
using System.Collections;

public class GunmanScript : HumanScript 
{
    public bool shotgun = false;
	public AudioClip shotgunFire, pistolFire;
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

            if (canAttack) Attack(closestZombie, attackDamage);
        }
    }

    protected override void NewVictim()
    {
        closestZombie = null;
    }

    protected override bool Attack(GameObject victim, float attackDamage)
    {
        float damage = shotgun ? attackDamage / (1 + (victim.transform.position - transform.position).sqrMagnitude) : attackDamage;

        if (base.Attack(victim, damage))
        {
            if (shotgun)
            {
                anim.SetTrigger("shotgunAttack");
                audio.PlayOneShot(shotgunFire);
            }
            else
            {
                anim.SetTrigger("pistolAttack");
                audio.PlayOneShot(pistolFire);
            }
            return true;
        }
        else return false;
    }

    
}
