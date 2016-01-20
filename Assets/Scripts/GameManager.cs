using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public float health_point = 100.0f;
	public int total_bricks_count = 0;

	// Game object
	public GameObject bluePaddlePref;
	public GameObject redPaddlePref;
	public GameObject blueBallPref;
	public GameObject redBallPref;

	public static GameManager instance = null;

	private GameObject playerBluePaddle;
	private GameObject playerRedPaddle;
	private GameObject[] balls;

	private GameObject ballHoldedByBluePaddle;
	private GameObject ballHoldedByRedPaddle;

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
		playerBluePaddle = Instantiate(bluePaddlePref, new Vector3(0.0f, -7.0f, 0.0f), Quaternion.identity) as GameObject;
		playerRedPaddle = Instantiate(redPaddlePref, new Vector3(0.0f,  7.0f, 0.0f), Quaternion.identity) as GameObject;
		playerBluePaddle.name = "BluePaddle";
		playerRedPaddle.name = "RedPaddle";
		ballHoldedByBluePaddle = Instantiate(blueBallPref, new Vector3(0.0f,  -6.0f, 0.0f), Quaternion.identity) as GameObject;
		ballHoldedByRedPaddle = Instantiate(redBallPref, new Vector3(0.0f,  6.0f, 0.0f), Quaternion.identity) as GameObject;
	}

	public void release_ball(string team){
		if (team == "BLUE"){
			BallController bc = ballHoldedByBluePaddle.GetComponent<BallController>();
			bc.be_released();
		}
		else if (team == "RED"){
			BallController bc = ballHoldedByRedPaddle.GetComponent<BallController>();
			bc.be_released();
		}
	}

	public void reborn_ball(string team){
		if (team == "BLUE"){
			float paddle_x = playerBluePaddle.transform.position.x;
			ballHoldedByBluePaddle = Instantiate(blueBallPref, new Vector3(paddle_x,  -6.0f, 0.0f), Quaternion.identity) as GameObject;
		}
		else if (team == "RED"){
			float paddle_x = playerRedPaddle.transform.position.x;
			ballHoldedByRedPaddle = Instantiate(redBallPref, new Vector3(paddle_x,  6.0f, 0.0f), Quaternion.identity) as GameObject;
		}
	}

	public void breakBrick()
	{
		total_bricks_count--;
	}

}
