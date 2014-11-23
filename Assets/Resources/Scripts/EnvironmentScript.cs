using System;
using UnityEngine;
using System.Collections;

public class EnvironmentScript : MonoBehaviour {

	public bool paused = false;
	public float pauseTime = 5f, playTime = 10f;
	public static GameObject currentZombie, currentTarget;
	private static bool zombieSelectedFirst = false, firstZombieSpawned = false;
    public static System.Random random = new System.Random();
	public GameObject camera;
	public AudioClip actionMusic, pauseMusic;

	// Use this for initialization
	void Start () {
		currentTarget = null;
		currentZombie = null;
		StartCoroutine(pauseInterval());
	}
	
	// Update is called once per frame
	void Update () {
	}

	//Highlights the zombie and his current target
	public static void showSelectedTargets(GameObject zombie, GameObject target)
	{

			if (currentZombie != null) currentZombie.SendMessage("setCircleActive", false, SendMessageOptions.DontRequireReceiver);
			if (currentTarget != null) currentTarget.SendMessage("setCircleActive", false, SendMessageOptions.DontRequireReceiver);

			currentZombie = zombie;
			currentTarget = target;
			zombieSelectedFirst = true; //verifying a human can't be selected w/o a zombie being first

			currentZombie.SendMessage("setCircleActive", true, SendMessageOptions.DontRequireReceiver);
				target.SendMessage("setCircleActive", true, SendMessageOptions.DontRequireReceiver);
	}

	//Highlights the selected human
	public static void setHumanMarked(GameObject human)
	{
		if(!firstZombieSpawned)
		{
			if(human != null) human.SendMessage("Zombify", SendMessageOptions.DontRequireReceiver);
			firstZombieSpawned = true;
		}
		else
		{
			if (zombieSelectedFirst)
			{
				currentTarget.SendMessage("setCircleActive", false, SendMessageOptions.DontRequireReceiver); //turn off old human
				human.SendMessage("setCircleActive", true, SendMessageOptions.DontRequireReceiver); //turn on new human
				setZombieTarget(human);
			}
			else Debug.Log("You must pick a zombie first!");
		}
	}

	//Sets the zombies target
	public static void setZombieTarget(GameObject human)
	{
		currentTarget = human;
		currentZombie.SendMessage("setTarget", currentTarget, SendMessageOptions.DontRequireReceiver);
	}

	IEnumerator pauseInterval()
	{
//		yield return new WaitForSeconds(pauseTime);
//		if (paused)
//		{
//			Time.timeScale = 0.0000001f;
//		}else
//			Time.timeScale = 1f;
//		paused = !paused;

		while(true)
		{
			//paused
			Time.timeScale = .00001f;
			paused = !paused;
			audio.Stop();
			audio.PlayOneShot(pauseMusic);
			yield return new WaitForSeconds(pauseTime * Time.timeScale);

			//running
			Time.timeScale = 1;
			paused = !paused;
			audio.Stop();
			audio.PlayOneShot(actionMusic);
			yield return new WaitForSeconds(playTime * Time.timeScale);

		}




	}

}
