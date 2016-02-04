using UnityEngine;
using System.Collections;

public enum Team{
	BLUE, RED
}

public class GameManager : MonoBehaviour {
	public int total_bricks_count = 0;
	public int blueScore = 0;
	public int redScore = 0;

	// Game object
	public GameObject bluePaddlePref;
	public GameObject redPaddlePref;
	public GameObject BallPref;
	public GameObject alertLightBluePref;
	public GameObject alertLightRedPref;

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
		playerBluePaddle = Instantiate(bluePaddlePref, new Vector3(0.0f, -4.0f, 0.0f), Quaternion.identity) as GameObject;
		playerRedPaddle = Instantiate(redPaddlePref, new Vector3(0.0f,  4.0f, 0.0f), Quaternion.identity) as GameObject;
		playerBluePaddle.name = "BluePaddle";
		playerRedPaddle.name = "RedPaddle";
		ballHoldedByBluePaddle = Instantiate(BallPref, new Vector3(0.0f, -3.5f, 0.0f), Quaternion.identity) as GameObject;		
		ballHoldedByBluePaddle.GetComponent<BallController>().set_team_color(Team.BLUE);
		ballHoldedByRedPaddle = Instantiate(BallPref, new Vector3(0.0f, 3.5f, 0.0f), Quaternion.identity) as GameObject;
		ballHoldedByRedPaddle.GetComponent<BallController>().set_team_color(Team.RED);
	}

	public void release_ball(Team team){
		if (team == Team.BLUE && ballHoldedByBluePaddle != null){  
			BallController bc = ballHoldedByBluePaddle.GetComponent<BallController>();
			bc.be_released();

		}
		else if (team == Team.RED && ballHoldedByRedPaddle != null){
			BallController bc = ballHoldedByRedPaddle.GetComponent<BallController>();
			bc.be_released();
		}
	}

	public void reborn_ball(Team team){
		if (team == Team.BLUE){
			float paddle_x = playerBluePaddle.transform.position.x;
			ballHoldedByBluePaddle = Instantiate(BallPref, new Vector3(paddle_x,  -3.0f, 0.0f), Quaternion.identity) as GameObject;
			ballHoldedByBluePaddle.GetComponent<BallController>().set_team_color(Team.BLUE);
		}
		else if (team == Team.RED){
			float paddle_x = playerRedPaddle.transform.position.x;
			ballHoldedByRedPaddle = Instantiate(BallPref, new Vector3(paddle_x,  3.0f, 0.0f), Quaternion.identity) as GameObject;
			ballHoldedByRedPaddle.GetComponent<BallController>().set_team_color(Team.RED);
		}
	}

	public void loss_ball(Team team){		
		GameObject.FindWithTag("MainCamera").GetComponent<CameraEffect>().CameraShake();
		if (team == Team.BLUE)
			Instantiate(alertLightRedPref, new Vector3(0.0f, 5.7f, 0.0f), Quaternion.identity);
		else if (team == Team.RED)
			Instantiate(alertLightBluePref, new Vector3(0.0f, -5.7f, 0.0f), Quaternion.identity);
	}

	public void breakBrick(int break_score, Team team)
	{
		if (team == Team.BLUE)
			blueScore += break_score;
		else if (team == Team.RED)
			redScore += break_score;

		total_bricks_count--;
	}

}
