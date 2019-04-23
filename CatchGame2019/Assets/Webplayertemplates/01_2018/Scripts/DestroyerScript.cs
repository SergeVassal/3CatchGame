using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

	public BallBombInstanter instanter;

	void OnTriggerEnter2D(Collider2D other){
		other.gameObject.SetActive (false);
		BallBombInstanter.instanter.listOfAvailObj.Add (other.gameObject);
		BallBombInstanter.instanter.listOfUnavailObj.Remove (other.gameObject);	
	}
}
