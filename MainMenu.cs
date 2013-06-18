using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public float xModifier, yModifier;
	public GUISkin customSkin;
	
	void OnGUI()
	{
		GUI.skin = customSkin;
		
		GUI.BeginGroup(new Rect(Screen.width / xModifier, Screen.height / yModifier, 200, 200));
		GUI.Box(new Rect(0,0,200, 600), "Main Menu");
		
		if(GUI.Button(new Rect(100,300,100,50), "Start"))
		{
			Application.LoadLevel("Jovy_test");
		}
		if (GUI.Button(new Rect(50,120,100,50),"Exit"))
		{
			Application.Quit();
		}
		
		GUI.EndGroup();
		
	}

	/*// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}*/
}
