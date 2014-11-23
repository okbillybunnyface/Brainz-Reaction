using UnityEngine;
using System.Collections;

public class ZombieScript : CharacterScript
{
    private GameObject target;
	public GameObject selectionCircle;
	Animator anim;

	// Use this for initialization
	void Start () 
    {
		anim = GetComponent<Animator>();
        walkScript = this.GetComponent<WalkScript>();
        target = this.gameObject;
        StartCoroutine(FindVictim(4f));
	}

    void Update()
    {
		if (hitPoints <= 0) Die();
        if (!target.activeSelf && !ded)
        {
            walkScript.StopSeeking();
            StartCoroutine(FindVictim(1f));
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
        //this.gameObject.SetActive(false);

        base.Die();
        walkScript.StopSeeking();
        rigidbody2D.isKinematic = true;
        this.gameObject.layer = 0;
        this.renderer.sortingOrder = this.renderer.sortingOrder - 1;
		anim.SetTrigger("death");

		this.gameObject.collider2D.enabled = false;
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

	void OnMouseOver()
	{
		Debug.Log("mouseover");
		if (Input.GetMouseButtonDown(0))
		{
			EnvironmentScript.showSelectedTargets(this.gameObject, target);
		}
	}

	public void setCircleActive(bool circleOn)
	{
		this.selectionCircle.SetActive(circleOn);
	}

	public void setTarget(GameObject humanTarget)
	{
		this.target = humanTarget;
        walkScript.SeekTarget(humanTarget);
	}



}
