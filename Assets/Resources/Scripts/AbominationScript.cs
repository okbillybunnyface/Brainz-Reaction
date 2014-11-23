using UnityEngine;
using System.Collections;

public class AbominationScript : ZombieScript {

    public float explosionRadius = 0.5f;
    public float explosionForce = 1f;
    private LayerMask humanLayer;

    public void Initialize(float hitpoints, float attackDamage, float level)
    {
        this.hitPoints = hitPoints;
        this.attackDamage = attackDamage;
        this.level = level;
        humanLayer = 1 << 8 | 1 << 9;
    }

    protected override void Die()
    {
        Collider2D[] hoomens = Physics2D.OverlapCircleAll(transform.position, explosionRadius, humanLayer);
        foreach (Collider2D fools in hoomens)
        {
            Vector3 toFool = (fools.transform.position - transform.position);
            toFool = toFool / toFool.sqrMagnitude;
            //fools.gameObject.GetComponent<WalkScript>().Stun();
            fools.gameObject.rigidbody2D.AddForce(toFool * explosionForce, ForceMode2D.Impulse);
            fools.GetComponent<CharacterScript>().Damage(toFool.magnitude * attackDamage);
        }
        particleSystem.startSpeed = 2f;
        particleSystem.startLifetime = 1f;
        particleSystem.Emit(500);

        base.Die();
    }
}
