using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public float jumpSpeed = 8.0F;
	public GUIStyle text;
	public GUIStyle frozen = new GUIStyle();
	private bool freeze;
	private bool readyToFreeze = true;
	private float freezeTime = 30.0f;
	public Transform[] pigSize = new Transform[6];
	private int pigIndex;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public static bool hasTool;
	public Transform staff;
	public GameObject bolt;
	public GameObject constructBolt;
	private int boltCount = 30;
	private float Boltregen = 3.0f;
	private int rebuildCount = 0;
	private int rebuilder;
	private float rebuildRegen = 5.0f;
	public AudioClip castSound;
	public static int WolfKills;
	private int killRaise;
	public static int finalKills;
	public float fireRate = 0.4f;
	public float fireRateChange;
	private bool readyToFire = true;
	public GameObject poofEffect;
	public GameObject fartEffect;
	public AudioClip growSound;
	public AudioClip fartSound;
    
	
	void Start()
	{
		for (int i = 0; i < 6; i++)
		{
			pigSize[i] = transform.GetChild(i);
		}
	}
	
    void Update() {

	#region Controls
    if (Input.GetKey(KeyCode.A))
            {
				Vector3 newPos = transform.position;
                newPos.z += (Time.timeScale * 0.2f);
				transform.position = newPos;
            }
       
    if (Input.GetKey(KeyCode.D))
            {
               Vector3 newPos = transform.position;
                newPos.z -= (Time.timeScale * 0.2f);
				transform.position = newPos;
            }
		
	if (Input.GetKey(KeyCode.S))
            {
               Vector3 newPos = transform.position;
                newPos.x -= (Time.timeScale * 0.2f);
				transform.position = newPos;
            }
	if (Input.GetKey(KeyCode.W))
            {
                Vector3 newPos = transform.position;
                newPos.x += (Time.timeScale * 0.2f);
				transform.position = newPos;
            }
#endregion
	#region Abilities
	if (Input.GetKey(KeyCode.F) && readyToFreeze)
	    {
			freeze = true;
			readyToFreeze = false;
			Enemy_Manager.speedScale = 0;
			StartCoroutine(changeWolfSpeed(3.0f));
	    }
	if (readyToFreeze == false)
		{
			checkFreeze();
		}
	if (Input.GetMouseButton(0) && readyToFire)
		{
			//boltCount -= 1;
			AudioSource.PlayClipAtPoint(castSound, transform.position, 0.2f);
			Vector3 staffPos = staff.transform.position;
			GameObject newShot = (GameObject)Instantiate(bolt);
			newShot.transform.position = staffPos;
			newShot.rigidbody.AddForce(transform.forward * 500);
			newShot.transform.LookAt(transform.position);
			readyToFire = false;
		}
		if (!readyToFire)
		{
			checkTime();
		}
	if (Input.GetMouseButtonDown(1) && rebuildCount >= 1)
		{
			rebuildCount -= 1;
			Vector3 staffPos = staff.transform.position;
			GameObject newShot = (GameObject)Instantiate(constructBolt);
			newShot.transform.position = staffPos;
			newShot.rigidbody.AddForce(transform.forward * 500);
			newShot.transform.LookAt(transform.position);
		}	
		#endregion
	#region Checks
		if (pigIndex == 0)
			{
				fireRateChange = 0.4f;
			}
		else if (pigIndex == 1)
			{
				fireRateChange = 0.35f;
			}
		else if (pigIndex == 2)
			{
				fireRateChange = 0.3f;
			}
		else if (pigIndex == 3)
			{
				fireRateChange = 0.25f;
			}
		else if (pigIndex == 4)
			{
				fireRateChange = 0.2f;
			}
		else if (pigIndex == 5)
			{
				fireRateChange = 0.1f;
			}
		
#endregion
		
		Vector3 upAxis = new Vector3(0,-1,0);
		Vector3 mouseScreenPosition = Input.mousePosition;
		mouseScreenPosition.y = transform.position.y;
		Vector3 mouseWorldSpace = Camera.mainCamera.ScreenToWorldPoint(Input.mousePosition);
		transform.LookAt(mouseWorldSpace, upAxis);
		transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
		
		if (WolfKills >= killRaise + 5)
		{
			if (pigIndex <= 4)
			{
				killRaise += 5;
				rebuildCount += 5;
				AudioSource.PlayClipAtPoint(growSound,transform.position);
				GameObject newPoof = (GameObject)Instantiate(poofEffect);
				newPoof.transform.position = transform.position;
				Destroy(newPoof,3.0f);
				pigSize[pigIndex].renderer.enabled = false;
				pigIndex++;
				pigSize[pigIndex].renderer.enabled = true;
				rebuilder = rebuildCount - 5;
			}
		}
		if (rebuildCount <= rebuilder)
		{
			if (pigIndex >= 1)
			{
				rebuilder -= 5;
				AudioSource.PlayClipAtPoint(fartSound,transform.position);
				GameObject newFart = (GameObject)Instantiate(fartEffect);
				newFart.transform.position = transform.position;
				Destroy(newFart,3.0f);
				pigSize[pigIndex].renderer.enabled = false;
				pigIndex--;
				pigSize[pigIndex].renderer.enabled = true;
			}
		}
		if (pigIndex == 5)
		{
			WolfKills = 0;
			killRaise = 0;
		}
		
	    if (Input.GetKey(KeyCode.Escape))
        {
			Application.Quit();
        }
    	
    }
    //Applying gravity to the controller
//   moveDirection.y -= gravity * Time.deltaTime;
//    //Making the character move
//    controller.Move(moveDirection * Time.deltaTime);
//    }
   
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            transform.localScale *= 1.2f;
            Destroy(other.gameObject);
        }
        if (other.tag == "WinTrigger")
        {
            //Debug.Log("Worked");
            Application.LoadLevel("Jovy_testGameover");
        }
    }
   
	
	void OnCollisionEnter(Collision collision)
	{
    }
	
	IEnumerator changeWolfSpeed(float delay)
	{
		yield return new WaitForSeconds(delay);
		freeze = false;
		Enemy_Manager.speedScale = 1;
	}
	void OnGUI()
	{
		if (freeze)
		{
			GUI.Label(new Rect(0,0,Screen.width,Screen.height),"",frozen);
		}
		if (readyToFreeze)
		{
			GUI.Label(new Rect(0,Screen.height / 1.2f,400,200),"FREEZE READY!...\npress 'F' to freeze",text);
		}
		GUI.Label(new Rect(Screen.width / 1.6f,Screen.height / 1.2f,400,200),"Rebuild Spells: " + rebuildCount.ToString(),text);
	}
	void OnBecameInvisible()
	{
		Application.LoadLevel("GameOver");
	}
	private void checkTime()
	{
		if (fireRate <= 0)
		{
			fireRate = fireRateChange;
			readyToFire = true;
		}
		else
		{
			fireRate -= Time.deltaTime;
		}
	}
	
	private void checkFreeze()
	{
		if (freezeTime <= 0)
		{
			readyToFreeze = true;
			freezeTime = 30.0f;
		}
		else
		{
			freezeTime -= Time.deltaTime;
		}
	}
}
