using UnityEngine;
using System.Collections;

public class Looking : MonoBehaviour {

	private Vector3 worldPos;
	private float mouseX;
	private float mouseY;
	private float cameraDif;


	void Start () {

	    cameraDif = camera.transform.position.y - transform.position.y;

	}

	void Update () {

	    mouseX = Input.mousePosition.x;

	    mouseY = Input.mousePosition.z;

	    worldPos = camera.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cameraDif));

	    Vector3 turretLookDirection = new Vector3(worldPos.x,transform.position.y, worldPos.z);

	    transform.LookAt(turretLookDirection);

	}


}