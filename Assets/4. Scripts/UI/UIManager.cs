using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
	public IntervalCounting interval_counting;
	
	public static UIManager instance = null;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	public void setIntervalCounting(){
		interval_counting.SetStartCounting ();
	}
}
