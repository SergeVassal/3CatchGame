using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallBombInstanter : MonoBehaviour {

	public int pooledAmount;
	public GameObject[] objectsToPool;
	public static BallBombInstanter instanter;

	public List<GameObject> listOfAvailObj;
	public List<GameObject> listOfUnavailObj;
	private float maxWidth;
	private float ballHalfWidth;

	void Start(){
		instanter = this;
			
		listOfAvailObj = new List<GameObject> ();
		listOfUnavailObj = new List<GameObject> ();

		for (int i = 0; i < pooledAmount; i++) {
			GameObject obj = (GameObject)Instantiate (objectsToPool [Random.Range (0, objectsToPool.Length)]);
			obj.SetActive (false);
			listOfAvailObj.Add (obj);
		}

		ballHalfWidth = listOfAvailObj [0].GetComponent<Renderer> ().bounds.extents.x;
		Vector3 screenDimen = new Vector3 (Screen.width, Screen.height, 0f);
		Vector3 screenToWorld = Camera.main.ScreenToWorldPoint (screenDimen);
		maxWidth = screenToWorld.x - ballHalfWidth;
	}

	public void InstanterSwitch(bool canInstant){
		if (canInstant) {
			StartCoroutine ("Spawn");
		} else {
			StopCoroutine ("Spawn");
		}
	}

	IEnumerator Spawn(){

		yield return new WaitForSeconds (0.5f);

		for (int i = 0; i < 100; i++) {			
			int randomInt = Random.Range (0, listOfAvailObj.Count);
			GameObject exactObj = listOfAvailObj [randomInt];
			exactObj.transform.position = new Vector3 (Random.Range (-maxWidth, maxWidth),transform.position.y, 0f);
			exactObj.transform.rotation = Quaternion.identity;
			exactObj.SetActive (true);
			listOfUnavailObj.Add(exactObj);
			listOfAvailObj.Remove (exactObj);	

			yield return new WaitForSeconds(Random.Range (0.7f, 1.7f));

		}		
	}
}
