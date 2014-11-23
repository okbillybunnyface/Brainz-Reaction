using UnityEngine;
using System.Collections;

public class BoundsScript : MonoBehaviour {

    public static Camera mainCamera;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

    void Update()
    {
        this.transform.position = mainCamera.transform.position;
        this.transform.localScale = new Vector3(2 * mainCamera.orthographicSize * mainCamera.aspect, 2 * mainCamera.orthographicSize, 1f);
    }
    
}
