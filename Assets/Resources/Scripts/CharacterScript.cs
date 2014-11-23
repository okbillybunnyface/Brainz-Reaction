using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WalkScript))]
public abstract class CharacterScript : MonoBehaviour 
{
    public float hitPoints = 100f;
    public float attackDamage = 10f;
    public float attackDelay = 1f;

    protected float level = 1;

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

    public bool Damage(float amount)
    {
        hitPoints -= amount;

        particleSystem.Emit(50);

        if (hitPoints <= 0)
        {
            Die();
            return true;
        }
        else return false;
    }

    protected virtual bool Attack(GameObject victim, float damage)
    {
        if (canAttack)
        {
            CharacterScript script = victim.GetComponent<CharacterScript>();
            if (script.Damage(damage)) LevelUp();

            canAttack = false;
            StartCoroutine(RefreshAttack(attackDelay));
            return true;
        }
        else return false;
    }

    protected abstract void NewVictim();

    protected abstract void LevelUp();

    protected virtual void Die()
    {
        walkScript.StopSeeking();
        rigidbody2D.isKinematic = true;
        this.gameObject.layer = 0;
        this.renderer.sortingOrder = this.renderer.sortingOrder - 1;
        this.collider2D.enabled = false;
        ded = true;
        canAttack = false;
    }

    private IEnumerator RefreshAttack(float time)
    {
        yield return new WaitForSeconds(time + (float)(EnvironmentScript.random.NextDouble() * 0.4f * time - 0.2f * time));
        if(!ded)canAttack = true;
    }
}
