using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WalkScript))]
public class CharacterScript : MonoBehaviour 
{
    public float hitPoints = 100f;
    public float attackDamage = 10f;
    public float attackDelay = 1f;

    protected bool ded = false;

    protected WalkScript walkScript;

    protected bool canAttack = true;

    void Awake()
    {
        walkScript = this.gameObject.GetComponent<WalkScript>();
    }

    protected virtual void OnEnable()
    {
        ded = false;
        canAttack = true;
    }

    public void Damage(float amount)
    {
        hitPoints -= amount;

        if (hitPoints <= 0) Die();
    }

    protected virtual void Attack(GameObject victim)
    {
        canAttack = false;
        StartCoroutine(RefreshAttack(attackDelay));
    }

    protected virtual void Die()
    {
        ded = true;
        canAttack = false;
    }

    private IEnumerator RefreshAttack(float time)
    {
        yield return new WaitForSeconds(time + (float)(EnvironmentScript.random.NextDouble() * 0.4f * time - 0.2f * time));
        if(!ded)canAttack = true;
    }
}
