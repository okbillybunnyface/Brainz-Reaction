using UnityEngine;
using System.Collections;

public class ZombieScript : CharacterScript
{
    private GameObject target;

	// Use this for initialization
	void Start () 
    {
        walkScript = this.GetComponent<WalkScript>();
        target = this.gameObject;
        StartCoroutine(FindVictim(4f));
	}

    void Update()
    {
        if (!target.activeSelf)
        {
            walkScript.StopSeeking();
            StartCoroutine(FindVictim(2f));
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Human")
        {
            /*
            walkScript.StopSeeking();

            Infect(collision.gameObject);

            StartCoroutine(FindVictim());
             */

            Attack(collision.gameObject);
        }
    }

    protected override void Attack(GameObject victim)
    {
        if (canAttack)
        {
            victim.SendMessage("Damage", attackDamage, SendMessageOptions.DontRequireReceiver);
            victim.SendMessage("Infect", SendMessageOptions.DontRequireReceiver);
            base.Attack(victim);
        }
    }

    protected override void Die()
    {
        
    }

    private IEnumerator FindVictim(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject[] potentials = GameObject.FindGameObjectsWithTag("Human");
        GameObject currentVictim = potentials[0];
        float currentSqrDistance = (currentVictim.transform.position - transform.position).sqrMagnitude;
        foreach (GameObject victim in potentials)
        {
            float sqrDistance = (victim.transform.position - transform.position).sqrMagnitude;
            if (sqrDistance < currentSqrDistance)
            {
                currentSqrDistance = sqrDistance;
                currentVictim = victim;
            }
        }

        target = currentVictim;
        walkScript.SeekTarget(target);
    }
}
