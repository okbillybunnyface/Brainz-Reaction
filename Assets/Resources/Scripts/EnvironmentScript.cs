using System;
using UnityEngine;
using System.Collections;

public class EnvironmentScript : MonoBehaviour {

	public bool paused = false;
	public static bool gameStarted = false, youWin = false;
	public float pauseTime = 5f, playTime = 10f;
	public static GameObject currentZombie, currentTarget;
	private static bool zombieSelectedFirst = false, firstZombieSpawned = false;
    public static System.Random random = new System.Random();
	public GameObject camera;
	public AudioClip actionMusic, pauseMusic;
	public static int howManyHumansLeft;

	// Use this for initialization
	void Start () {
		currentTarget = null;
		currentZombie = null;
		howManyHumansLeft = (GameObject.FindGameObjectsWithTag("Human").Length) - 1;
		gameStarted = false;
		youWin = false;
		audio.Stop();
		audio.PlayOneShot(pauseMusic);
		StartCoroutine(waitForGameStart());
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static void OneLessHuman()
	{
		howManyHumansLeft -= 1; 
		Debug.Log("Humans left = " + howManyHumansLeft);
		if (howManyHumansLeft < 1) 
		{
			//win conditions method
			Debug.Log("Win conditions!");
			youWin = true;
		}
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
			if(currentTarget != null)currentTarget.SendMessage("setCircleActive", true, SendMessageOptions.DontRequireReceiver);
	}

	//Highlights the selected human
	public static void setHumanMarked(GameObject human)
	{
		if(!firstZombieSpawned)
		{
			if(human != null) human.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
			firstZombieSpawned = true;
			gameStarted = true;
		}
		else
		{
			if (zombieSelectedFirst)
			{
				if(currentTarget != null)currentTarget.SendMessage("setCircleActive", false, SendMessageOptions.DontRequireReceiver); //turn off old human
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


	IEnumerator waitForGameStart()
	{
		Time.timeScale = .000001f;
		while(!gameStarted)
		{
			yield return null;
		}
		Time.timeScale = 1f;
		StartCoroutine(pauseInterval());
	}
	 IEnumerator pauseInterval()
	{
		while(true)
		{
			//running
			Time.timeScale = 1;
			paused = !paused;
			audio.Stop();
			audio.PlayOneShot(actionMusic);
			yield return new WaitForSeconds(playTime * Time.timeScale);

			//paused
			Time.timeScale = .00001f;
			paused = !paused;
			audio.Stop();
			audio.PlayOneShot(pauseMusic);
			yield return new WaitForSeconds(pauseTime * Time.timeScale);
		}

	}

	void OnGUI()
	{
		if(youWin)
		{
			Time.timeScale = .00001f;
			if (GUI.Button(new Rect((Screen.width /2)-100, Screen.height/2, 100, 30), "You Win!"))
			{
				Debug.Log("Clicked the button with text");
				Application.LoadLevel(Application.loadedLevel);
			}

		}
	}
	
}
