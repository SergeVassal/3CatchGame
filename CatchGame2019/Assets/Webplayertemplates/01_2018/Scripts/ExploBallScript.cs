using UnityEngine;
using System.Collections;

public class ExploBallScript : MonoBehaviour {


	void Start () {
		Invoke ("Deactivation", 2f);
	
	}
	
	void Deactivation(){
		Destroy (gameObject);
	}
}
