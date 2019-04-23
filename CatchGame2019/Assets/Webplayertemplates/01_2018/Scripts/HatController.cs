using UnityEngine;
using System.Collections;

public class HatController : MonoBehaviour {

	Renderer hatRenderer;
	Rigidbody2D rBody;
	private float maxWidth;

	public Camera cam;
	private bool canControl;


	void Start () {

		if (cam == null) {
			cam = Camera.main;
		}

		hatRenderer = GetComponent<Renderer> ();	
		rBody = GetComponent<Rigidbody2D> ();
		Vector3 screenDimen = new Vector3(Screen.width, Screen.height, 0f);
		Vector3 screenToWorld = cam.ScreenToWorldPoint (screenDimen);
		float hatHalfWidth = hatRenderer.bounds.extents.x;
		maxWidth = screenToWorld.x - hatHalfWidth;	
		canControl = false;
	}
	

	void FixedUpdate () {
		if (canControl) {

			Vector3 mouseToWorldRaw = cam.ScreenToWorldPoint (Input.mousePosition);
			Vector3 mouseToWorldXOnly = new Vector3 (mouseToWorldRaw.x, transform.position.y, 0f);
			float finalXPosition = Mathf.Clamp (mouseToWorldXOnly.x, -maxWidth, maxWidth);
			mouseToWorldXOnly = new Vector3 (finalXPosition, transform.position.y, 0f);
			rBody.MovePosition (mouseToWorldXOnly);
		}	
	}

	public void ToggleControl(bool controlSwitch){
		canControl = controlSwitch;
	}
}
