using UnityEngine;
using System.Collections;

public class GameOver1 : MonoBehaviour {
	public AudioClip sound;
	public GUIStyle backGround;
	public GUIStyle text;
	// Use this for initialization
	void Start () {
		
		AudioSource.PlayClipAtPoint(sound,transform.position);
	
	}
	
	// Update is called once per frame
	void OnGUI() {
		
		GUI.Label(new Rect(0,0,Screen.width,Screen.height),"",backGround);
		GUI.Label(new Rect(100,600,1000,300),"SCORE: " + PlayerControl.finalKills.ToString(),text);
	
	}
}
