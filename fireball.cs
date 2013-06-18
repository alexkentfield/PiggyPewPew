using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {
	
	private Enemy wolf;
	public AudioClip fireballImpact;
	

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
			AudioSource.PlayClipAtPoint(fireballImpact, transform.position);
			wolf = other.GetComponent<Enemy>();
            wolf._health -= 20;
			Destroy(this.gameObject);
        }
    }
	
	void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
}
