using UnityEngine;
using System.Collections;

public class HatBallDestroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Ball") {
			GameControllerScript.gameControl.score += 100;
			other.gameObject.SetActive (false);
			BallBombInstanter.instanter.listOfAvailObj.Add (other.gameObject);
			BallBombInstanter.instanter.listOfUnavailObj.Remove (other.gameObject);		

		}
	}
}
