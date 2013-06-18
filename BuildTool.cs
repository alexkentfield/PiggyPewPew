using UnityEngine;
using System.Collections;

public class BuildTool : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider hit) {
		Destroy(this.gameObject);
		PlayerControl.hasTool = true;
	}
}
