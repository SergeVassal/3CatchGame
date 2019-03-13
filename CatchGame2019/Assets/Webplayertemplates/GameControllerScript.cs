using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

	public static GameControllerScript gameControl;
	public float timeLeft;
	[HideInInspector] public int score;
	public Text timerText;
	public Text scoreText;
	public GameObject startButton;
	public GameObject restartButton;
	public bool playing;
	public HatController hatController;
	public GameObject hatSplash;

	private float timeRef;

	void Awake(){
		UpdateText ();
		timeRef = timeLeft;
	}

	void Start () {		
		if (gameControl == null) {
			gameControl = this;
			DontDestroyOnLoad (gameObject);
			return;
		} else {
			Destroy (gameObject);
		}
		restartButton.SetActive (false);
		playing = false;
		score = 0;
	
	}
	

	void FixedUpdate () {
		UpdateText ();
		if (playing) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				playing = false;
				Invoke ("HatSwitcher", 1f);
			} else if (timeLeft <= 1) {
				BallBombInstanter.instanter.InstanterSwitch (false);
			}
		} 	
	}


	void UpdateText(){
		timerText.text = "Time Left:\n" + Mathf.RoundToInt(timeLeft);
		scoreText.text = "Score:\n" + score;
	}

	public void Starter(){
		startButton.SetActive (false);
		playing = true;
		BallBombInstanter.instanter.InstanterSwitch (true);
		hatController.ToggleControl (true);
		hatSplash.SetActive (false);
	}

	public void Restarter(){
		restartButton.SetActive (false);
		score = 0;
		timeLeft = timeRef;
		playing = true;
		BallBombInstanter.instanter.InstanterSwitch (true);
		hatController.ToggleControl (true);
	}

	private void HatSwitcher(){		
		hatController.ToggleControl (false);
		restartButton.SetActive (true);
	}
}
