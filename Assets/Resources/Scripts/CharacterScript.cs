﻿using UnityEngine;
using System.Collections;

public abstract class CharacterScript : MonoBehaviour 
{
    public float hitPoints = 100f;
    public float attackDamage = 10f;
    public float attackDelay = 1f;

    protected bool canAttack = true;

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

    protected abstract void Die();

    private IEnumerator RefreshAttack(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }
}