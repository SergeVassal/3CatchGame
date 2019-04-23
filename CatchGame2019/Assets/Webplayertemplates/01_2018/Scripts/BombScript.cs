using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombScript : MonoBehaviour {

	public GameObject exploPrefab;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Hat") {
			GameControllerScript.gameControl.score -= 100;
			BallBombInstanter.instanter.listOfAvailObj.Add (gameObject);
			BallBombInstanter.instanter.listOfUnavailObj.Remove (gameObject);	
			Instantiate (exploPrefab, new Vector3(transform.position.x,transform.position.y+1f,0f), 
				Quaternion.identity);
			gameObject.SetActive (false);		
		}
	}

	void OnCollisionEnter2D(){
		GameControllerScript.gameControl.score -= 100;
		BallBombInstanter.instanter.listOfAvailObj.Add (gameObject);
		BallBombInstanter.instanter.listOfUnavailObj.Remove (gameObject);	
		Instantiate (exploPrefab, transform.position, Quaternion.identity);
		gameObject.SetActive (false);		
	}
}
