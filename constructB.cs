using UnityEngine;
using System.Collections;

public class constructB : MonoBehaviour {
	private Wall_Destruct wall;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
   	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {
			wall = other.GetComponent<Wall_Destruct>();
			if (wall.transform.renderer.enabled == true)
			{
				wall.stoneOnce = false;
				wall.woodOnce = false;
				wall.strawOnce = false;
				AudioSource.PlayClipAtPoint(wall.rebuildSound, transform.position);
				GameObject newRebuild = (GameObject)Instantiate(wall.rebuild);
				newRebuild.transform.position = transform.position;
				Destroy(newRebuild,5.0f);
          	 	wall.Rebuild();
			}
			else if (wall.transform.renderer.enabled == false)
			{
				wall.WallCount++;
				wall.renderer.enabled = true;
				wall.stoneOnce = false;
				wall.woodOnce = false;
				wall.strawOnce = false;
				AudioSource.PlayClipAtPoint(wall.rebuildSound, transform.position);
				GameObject newRebuild = (GameObject)Instantiate(wall.rebuild);
				newRebuild.transform.position = transform.position;
				Destroy(newRebuild,5.0f);
				wall.Rebuild();
				
			}
			
			Destroy(this.gameObject);
        }
    }
	void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
	
}
