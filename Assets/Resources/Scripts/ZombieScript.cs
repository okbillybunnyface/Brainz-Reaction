using UnityEngine;
using System.Collections;

[RequireComponent(typeof (WalkScript))]
public class ZombieScript : MonoBehaviour 
{
    private WalkScript walkScript;
    private GameObject target;

	// Use this for initialization
	void Start () 
    {
        walkScript = this.GetComponent<WalkScript>();
        target = GameObject.FindGameObjectWithTag("Human");
        walkScript.SeekTarget(target);
	}

    void Update()
    {
        if (!target.activeSelf) StartCoroutine(FindVictim());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Human")
        {
            walkScript.StopSeeking();

            Infect(collision.gameObject);

            StartCoroutine(FindVictim());
        }
    }

    void Infect(GameObject victim)
    {
        victim.SetActive(false);
        GameObject zombie = (GameObject)GameObject.Instantiate(this.gameObject);
        zombie.transform.position = victim.transform.position;
    }

    IEnumerator FindVictim()
    {
        yield return new WaitForSeconds(2f);


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
