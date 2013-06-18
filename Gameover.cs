using UnityEngine;
using System.Collections;

public class Gameover : MonoBehaviour 
{
	public GUISkin customSkin;
	public float xPositionModifier;
	public float yPositionModifier;

	/*// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}*/
	
	void OnGUI()
	{
		GUI.skin = customSkin;
		//GUI.BeginGroup(new Rect(Screen.width / 10, Screen.height / 3, 200, 200));
		GUI.BeginGroup(new Rect(Screen.width / xPositionModifier, Screen.height / yPositionModifier, 200, 300));
		GUI.Box(new Rect(0,0,200,300), "Game Over");
		
		
		if(GUI.Button(new Rect(50,80,100,50), "PlayAgain"))
		{
			Application.LoadLevel("Jovy_test");	
		}
		if(GUI.Button(new Rect(50,140,100,50), "MainMenu"))
		{
			Application.LoadLevel("Jovy_testMenu");	
		}
		if(GUI.Button(new Rect(50,200,100,50), "Exit"))
		{
			Application.Quit();	
		}
		
		GUI.EndGroup();
	}
}
