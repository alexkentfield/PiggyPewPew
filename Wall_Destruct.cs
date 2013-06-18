using UnityEngine;
using System.Collections;

public class Wall_Destruct : MonoBehaviour {
	public Transform[] Wall = new Transform[3];
	public float Health;
	public int WallCount = 8;
	private bool wait;
	private float waitTime;
	public GameObject stoneDeath;
	public GameObject woodDeath;
	public GameObject strawDeath;
	public GameObject rebuild;
	public AudioClip attackSound;
	public AudioClip rebuildSound;
	
	public bool stoneOnce;
	public bool woodOnce;
	public bool strawOnce;
	// Use this for initialization
	void Start () {
		
		wait = false;
		Wall[0] = this.transform.GetChild(0);
		Wall[1] = this.transform.GetChild(1);
		Wall[2] = this.transform.GetChild(2);
		Health = 150;
		waitTime = 3.0f;
	
	}

	public void TakeDamage(float f_attack)
	{
		if (!wait)
		{
			CheckHealth();
			Health -= f_attack;
			AudioSource.PlayClipAtPoint(attackSound, transform.position);
			wait = true;
		}
		else
		{
			Waiting();
		}
	}
	public void Rebuild()
	{
		CheckHealth();
		Health += 10;
	}
	private void CheckHealth()
	{
		if (Health >= 150)
		{
			Health = 150;
		}
		if (Health > 100)
		{
			Wall[0].renderer.enabled = true;
			Wall[1].renderer.enabled = false;
			Wall[2].renderer.enabled = false;
		}
		if (Health <= 100)
		{
			Wall[0].renderer.enabled = false;
			Wall[1].renderer.enabled = false;
			Wall[2].renderer.enabled = true;
			if (!stoneOnce)
			{
				GameObject newDeath = (GameObject)Instantiate(stoneDeath);
				Vector3 newPos = transform.position;
				newPos.z += 1.0f;
				newDeath.transform.position = newPos;
				Destroy(newDeath,2.0f);
				stoneOnce = true;
			}
			
		}
		if (Health <= 50)
		{
			Wall[0].renderer.enabled = false;
			Wall[1].renderer.enabled = true;
			Wall[2].renderer.enabled = false;
			if (!woodOnce)
			{
				GameObject newDeath = (GameObject)Instantiate(woodDeath);
				Vector3 newPos = transform.position;
				newPos.z += 1.0f;
				newDeath.transform.position = newPos;
				Destroy(newDeath,2.0f);
				woodOnce = true;
			}
			
		}
		if (Health <= 0)
		{
			Wall[0].renderer.enabled = false;
			Wall[1].renderer.enabled = false;
			Wall[2].renderer.enabled = false;
			if (!strawOnce)
			{
				GameObject newDeath = (GameObject)Instantiate(strawDeath);
				Vector3 newPos = transform.position;
				newPos.z += 1.0f;
				newDeath.transform.position = newPos;
				Destroy(newDeath,2.0f);
				strawOnce = true;
			}
			this.renderer.enabled = false;
			WallCount -= 1;
		}
		
		
		
	}
	private void Waiting()
	{
		if (waitTime <= 0)
		{
			waitTime = 3.0f;
			wait = false;
		}
		else
			waitTime -= Time.deltaTime;
	}
	

	
}
