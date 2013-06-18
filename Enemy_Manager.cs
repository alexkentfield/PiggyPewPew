using UnityEngine;
using System.Collections;

public class Enemy_Manager : MonoBehaviour {
	private GameObject startPoint;
	private bool stop;
	protected GameObject enemy;
	private float sendTimer = 1.0f;
	private bool readyToSend;
	protected float speedMin, speedMax;
	public static int speedScale = 1;
	private float speedIncrease = 5;
	private float sendIncrease = 10.0f;
	private float sendMax;
	private	float timeIncrease; 
	public static int sheep = 8;
	// Use this for initialization
	void Awake () {
		speedMin = 0.05f;
		speedMax = 0.15f;
		sendMax = 4.5f;
		findPoints();
		enemy = (GameObject)Resources.Load("Enemy");
		
	}
	// Update is called once per frame
	void Update () {
		
		if (sheep <= 0)
		{
			Application.LoadLevel("GameOver");
		}
		
		if (timeIncrease >= speedIncrease)
		{
			Debug.Log("GUARD YOUR FUCKING CASLTE YOU STUPID FUCKING PIG!");
			speedIncrease += 5;
			speedMin += 0.02f;
			speedMax += 0.02f;
		}
		if (timeIncrease >= sendIncrease)
		{
			Debug.Log("GUARD YOUR FUCKING CASLTE YOU STUPID FUCKING PIG!");
			sendIncrease += 10.0f;
			sendMax -= 0.02f;
		}
		timeIncrease += Time.deltaTime;
		StartCoroutine(Begin());
		Vector3 newPoint = startPoint.transform.position;
		newPoint.z = Random.Range(-265,-245);
		startPoint.transform.position = newPoint;
	
	}
	protected virtual void findPoints()
	{
		startPoint = GameObject.Find("startPoint");
	
	}
	protected virtual void BeginSpawn(GameObject t_Enemy)
	{
		GameObject newEnemy = (GameObject)Instantiate(t_Enemy);
		newEnemy.transform.position = startPoint.transform.position;
	}
	
	protected virtual void CheckHealth(GameObject t_Enemy,float t_Health)
	{
		if (t_Health <= 0)
		{
			Destroy(t_Enemy);
		}
	}
	private IEnumerator Begin()
	{
		yield return new WaitForSeconds(0);
		if (readyToSend)
		{
			BeginSpawn(enemy);
			readyToSend = false;
		}
		else 
			send_Timer();	
	}
	protected virtual void CheckMovement(float s_speed,GameObject s_enemy, GameObject s_target)
	{
		s_enemy.transform.LookAt(s_target.transform);
		s_enemy.transform.position += s_enemy.transform.forward * speedScale * s_speed;
	}
	
	protected virtual void CheckAttack(float s_attack, Wall_Destruct s_wall)
	{
		s_wall.TakeDamage(s_attack);
	}
	
	private void send_Timer()
	{
		if (sendTimer <= 0)
		{
			readyToSend = true;
			sendTimer = sendMax;
		}
		else sendTimer -= Time.deltaTime;
	}
}

