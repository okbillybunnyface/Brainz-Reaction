using UnityEngine;
using System.Collections;

public abstract class HumanScript : CharacterScript 
{
    public float sightRadius = 5f;
    public int zombieCountThreshold = 3;
    private bool infected = false;
    protected Collider2D[] zombiesInSight;
    private LayerMask zombieLayer = 1 << 9;
    private bool reacting = false;
	private GameObject selectionCircle;

    void OnEnable()
    {
        infected = false;
        StartCoroutine(DetectZombies());
		selectionCircle = this.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (zombiesInSight.Length > zombieCountThreshold || reacting)
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

    protected override void Die()
    {
        if (infected)
        {
            GameObject zombie = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Zombie"));
            zombie.transform.position = transform.position;
            this.gameObject.SetActive(false);
        }
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

    IEnumerator DetectZombies()
    {
        while (true)
        {
            zombiesInSight = Physics2D.OverlapCircleAll(transform.position, sightRadius, zombieLayer);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
