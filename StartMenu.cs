using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	public GUIStyle menu = new GUIStyle();
	public GUIStyle button = new GUIStyle();
	public AudioClip menuSound;
	public AudioClip startSound;
	
	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint(menuSound, transform.position);
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(0,0,Screen.width,Screen.height),"",menu);
		
		if (GUI.Button(new Rect(Screen.width / 12.8f,Screen.height / 2.88f,Screen.width / 4.26f,Screen.height/2.4f),"",button))
		{
			AudioSource.PlayClipAtPoint(startSound, transform.position);
			Application.LoadLevel(1);
		}
	}
}
