using UnityEngine;
using System.Collections;

public class HumanScript : CharacterScript 
{
    private bool infected = false;

    void OnEnable()
    {
        infected = false;
    }

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
}
