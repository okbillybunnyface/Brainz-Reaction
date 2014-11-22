using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WalkScript))]
public abstract class CharacterScript : MonoBehaviour 
{
    public float hitPoints = 100f;
    public float attackDamage = 10f;
    public float attackDelay = 1f;

    protected WalkScript walkScript;

    protected bool canAttack = true;

    void Awake()
    {
        walkScript = this.gameObject.GetComponent<WalkScript>();
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

    protected abstract void Die();

    private IEnumerator RefreshAttack(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }
}
