using UnityEngine;
using System.Collections;

public class ZombieScript : CharacterScript
{
    private GameObject target;
	public GameObject selectionCircle;

	// Use this for initialization
	void Start () 
    {
        walkScript = this.GetComponent<WalkScript>();
        target = this.gameObject;
        StartCoroutine(FindVictim(1f));
	}

    void Update()
    {
		if (hitPoints <= 0) Die();
        
        if (target == null && !ded)
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

            Attack(collision.gameObject, attackDamage);
        }
    }

    protected override bool Attack(GameObject victim, float attackDamage)
    {
        if (base.Attack(victim, attackDamage))
        {
            transform.right = victim.transform.position - transform.position;
            rigidbody2D.AddForce((victim.transform.position - transform.position) * 2f, ForceMode2D.Impulse);
            victim.SendMessage("Infect", SendMessageOptions.DontRequireReceiver);
            return true;
        }
        else return false;
    }

    protected override void NewVictim()
    {
        StartCoroutine(FindVictim(1f));
    }

    protected override void Die()
    {
        //this.gameObject.SetActive(false);

        base.Die();
        
		anim.SetTrigger("death");
    }

    protected override void LevelUp()
    {
        level++;
        if (level < 10)
        {
            attackDamage += 25f;
            hitPoints += 25f;
            if (level == 4) Evolve(this.hitPoints, this.attackDamage, this.level);
            transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, transform.localScale.z);
        }
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

    private void Evolve(float hitpoints, float attackDamage, float level)
    {
        GameObject zombie = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Abomination"));
        zombie.transform.position = transform.position;
        AbominationScript abomscript = zombie.GetComponent<AbominationScript>();
        abomscript.Initialize(hitpoints, attackDamage, level);
        GameObject.Destroy(this.gameObject);
    }

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			EnvironmentScript.showSelectedTargets(this.gameObject, target);
            walkScript.SeekTarget(target);
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
