using UnityEngine;
using System.Collections;

public class AbominationScript : ZombieScript {

    public float explosionRadius = 0.5f;
    private LayerMask zombieLayer = 1 << 10, humanLayer = 1 << 8;

    public void Initialize(float hitpoints, float attackDamage, float level)
    {
        this.hitPoints = hitPoints;
        this.attackDamage = attackDamage;
        this.level = level;
    }

    protected override void Die()
    {
        base.Die();
        Physics2D.OverlapCircleAll(transform.position, explosionRadius, humanLayer);
    }
}
