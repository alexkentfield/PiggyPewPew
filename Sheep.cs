using UnityEngine;
using System.Collections;

public class Sheep : MonoBehaviour {
	private bool turn;
	private float turnTime;
	public AudioClip sheepDeathSound;
	public GameObject sheepDeath;
	// Use this for initialization
	void Start () {
		
		turnTime = 3.0f;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		if (turn)
		{
			turnAround();
			turn = false;
		}
		else
		{
			transform.position += transform.right * 0.005f;
			turnTime -= Time.deltaTime;
		}
		
		if (turnTime <= 0)
		{
			turn = true;
			turnTime = Random.Range(2.0f,3.5f);
		}
	
	}
	
	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "Enemy")
		{
			AudioSource.PlayClipAtPoint(sheepDeathSound, transform.position);
			Enemy_Manager.sheep--;
			Debug.Log(Enemy_Manager.sheep + " More Sheep!!!");
			GameObject newDeath = (GameObject)Instantiate(sheepDeath);
			newDeath.transform.position = this.gameObject.transform.position;
			AudioSource.PlayClipAtPoint(sheepDeathSound, transform.position);
			Destroy(newDeath,2.0f);
			Destroy(this.gameObject);
			Destroy(hit.gameObject);
		}
	}
	
	private void turnAround()
	{
		transform.Rotate(0,Random.Range(45,180),0);
	}
}
