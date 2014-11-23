using UnityEngine;
using System.Collections;

public abstract class HumanScript : CharacterScript 
{
    public float sightRadius = 5f;
    public int zombieCountThreshold = 3;
    private bool infected = false;
    protected Collider2D[] zombiesInSight, humansInSight;
    private LayerMask zombieLayer = 1 << 10, humanLayer = 1 << 8;
    private bool reacting = false;
	private GameObject selectionCircle;
    protected GameObject closestZombie, closestHuman;

    void OnEnable()
    {
        infected = false;
        StartCoroutine(DetectZombies());
        StartCoroutine(DetectPeople());
		selectionCircle = this.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (zombiesInSight.Length >= zombieCountThreshold || reacting)
        {
            if (!reacting) StartCoroutine(ReactionResetTimer(5f));
            reacting = true;
            if (zombiesInSight.Length > 0) ReactToZombies();
        }
    }

    protected abstract void ReactToZombies();

    public void Infect()
    {
        infected = true;
    }

    protected override void NewVictim()
    {

    }

    protected override void Die()
    {
        base.Die();
        StartCoroutine(Zombify());
    }

    protected override void LevelUp()
    {
        attackDamage += 10f;
        hitPoints += 10f;
        level++;
        transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, transform.localScale.z);
    }

    IEnumerator Zombify()
    {
        yield return new WaitForSeconds(1f);
        GameObject zombie = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Zombie"));
        zombie.transform.position = transform.position;
        GameObject.Destroy(this.gameObject);
    }

	public void setCircleActive(bool circleOn)
	{
		this.selectionCircle.SetActive(circleOn);
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			EnvironmentScript.setHumanMarked(this.gameObject);
		}
	}

    IEnumerator ReactionResetTimer(float time)
    {
        yield return new WaitForSeconds(time);

        reacting = false;
    }

    IEnumerator DetectPeople()
    {
        while (true)
        {
            humansInSight = Physics2D.OverlapCircleAll(transform.position, sightRadius * 10, humanLayer);

            if (humansInSight.Length > 0)
            {
                closestHuman = humansInSight[0].gameObject;
                foreach (Collider2D human in humansInSight)
                {
                    if ((human.transform.position - transform.position).sqrMagnitude < (closestHuman.transform.position - transform.position).sqrMagnitude)
                    {
                        closestHuman = human.gameObject;
                    }
                }
            }
            else closestHuman = null;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator DetectZombies()
    {
        while (true)
        {
            zombiesInSight = Physics2D.OverlapCircleAll(transform.position, sightRadius, zombieLayer);

            if (zombiesInSight.Length > 0)
            {
                closestZombie = zombiesInSight[0].gameObject;
                foreach (Collider2D zombie in zombiesInSight)
                {
                    if ((zombie.transform.position - transform.position).sqrMagnitude < (closestZombie.transform.position - transform.position).sqrMagnitude)
                    {
                        closestZombie = zombie.gameObject;
                    }
                }
            }
            else closestZombie = null;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
