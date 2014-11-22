using UnityEngine;
using System.Collections;

public class EnvironmentScript : MonoBehaviour {

	public bool paused = false;
	public float pauseTime = 5f;

	// Use this for initialization
	void Start () {
		StartCoroutine(pauseInterval());
	}
	
	// Update is called once per frame
	void Update () {

		if (paused)
		{
			Time.timeScale = 0.0000001f;
		}else
			Time.timeScale = 1f;
	
	}

	IEnumerator pauseInterval()
	{
		yield return new WaitForSeconds(pauseTime);
		paused = !paused;
	}

}
