using UnityEngine;
using System.Collections;

public class Enemy : Enemy_Manager {
	public float _health;
	public float _speed;
	protected float _attack;
	public GameObject _wall;
	public GameObject _pathNode;
	public GameObject _sheep;
	private GameObject target;
	private Wall_Destruct wall;
	private bool _moving, _attacking, _sheepattack;
	private int random;
	public GameObject death;
	public AudioClip deathSound;
	
	
	// Use this for initialization
	void Start () {
		
		random = Random.Range(1,9);
		
		
		_health = 50;
		_attack = 20;
		_moving = true;
		_pathNode = GameObject.Find("PathNode_" + random);
		_sheep = GameObject.Find("Sheep_" + random);
		_wall = GameObject.Find("wall" + random);
		wall = _wall.gameObject.GetComponent<Wall_Destruct>();
		_speed = Random.Range(speedMin,speedMax);
		target = _pathNode.gameObject;
		
		if (wall.WallCount < 8)
		{
			do 
			{
				_wall = GameObject.Find("wall" + Random.Range(1,9));
				
			}while (_wall.renderer.enabled);
			
			do 
			{
				_sheep = GameObject.Find("Sheep_" + Random.Range(1,9));
			}while (!_sheep);
			
		}
	}
	void Update () {
		
		if (_moving)
		{
			CheckMovement(_speed,this.gameObject,target);
		}
		
		if (_attacking)
		{
			CheckAttack(_attack,wall);
		}
		
		if (_sheepattack)
		{
			if (!_sheep)
			{
				_sheep = GameObject.Find("Sheep_" + Random.Range(1,9));
			}
			CheckMovement(_speed,this.gameObject,_sheep);
			
		}
		
		if (_health <= 0)
		{
			PlayerControl.WolfKills++;
			PlayerControl.finalKills++;
			GameObject newDeath = (GameObject)Instantiate(death);
			newDeath.transform.position = this.gameObject.transform.position;
			AudioSource.PlayClipAtPoint(deathSound, transform.position);
			Destroy(newDeath,3.0f);
			Destroy(this.gameObject);
		}
		
	}
	
	protected override void CheckMovement (float s_speed, GameObject s_enemy, GameObject s_target)
	{
		base.CheckMovement (s_speed, s_enemy, s_target);
	}
	
	protected override void CheckAttack (float s_attack, Wall_Destruct s_wall)
	{
		base.CheckAttack (s_attack, s_wall);
	}
	
	void OnTriggerStay(Collider hit)
	{
		if (hit.gameObject.name == _pathNode.name)
		{
			target = _wall.gameObject;
		}
		if (hit.gameObject.name == _wall.name && _wall.renderer.enabled == false)
		{
			Debug.Log("GET THE SHEEP");
			_sheepattack = true;
			_attacking = false;
			_moving = false;
		}
		
		if (hit.gameObject.name == _wall.name)
		{
			Debug.Log("Attacking");
			_attacking = true;
			_moving = false;
		}
		if (hit.gameObject.name == "Player")
		{
			_health -= 5;
			CheckHealth(this.gameObject,_health);
		}
		
	}
}
