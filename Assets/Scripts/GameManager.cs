using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public float health_point = 100.0f;
	public int total_bricks_count = 0;

	// Game object
	public GameObject bluePaddle;
	public GameObject redPaddle;
	public GameObject blueBall;
	public GameObject redBall;

	public static GameManager instance = null;

	private GameObject playerBluePaddle;
	private GameObject playerRedPaddle;
	private GameObject[] balls;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		Setup();
	}

	// Setup paddle and load level bricks 
	public void Setup ()
	{
		playerBluePaddle = Instantiate(bluePaddle, new Vector3(0.0f, -7.0f, 0.0f), Quaternion.identity) as GameObject;
		playerRedPaddle = Instantiate(redPaddle, new Vector3(0.0f,  7.0f, 0.0f), Quaternion.identity) as GameObject;
		Instantiate(blueBall, new Vector3(0.0f,  -6.0f, 0.0f), Quaternion.identity);
		Instantiate(redBall, new Vector3(0.0f,  6.0f, 0.0f), Quaternion.identity);
	}

	public void breakBrick()
	{
		total_bricks_count--;
	}

}
