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
	
	// Update is called once per frame
	void Update () {
	
	}
}
