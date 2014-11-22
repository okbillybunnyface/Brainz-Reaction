using UnityEngine;
using System.Collections;

public abstract class HumanScript : CharacterScript 
{
    public float sightRadius = 5f;
    public int zombieCountThreshold = 3;
    private bool infected = false;
    protected Collider2D[] zombiesInSight;
    private LayerMask zombieLayer = 1 << 9;

    void OnEnable()
    {
        infected = false;
        StartCoroutine(DetectZombies());
    }

    void Update()
    {
        if (zombiesInSight.Length > zombieCountThreshold)
        {
            ReactToZombies();
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

    IEnumerator DetectZombies()
    {
        while (true)
        {
            zombiesInSight = Physics2D.OverlapCircleAll(transform.position, sightRadius, zombieLayer);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
