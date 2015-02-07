using UnityEngine;
using System.Collections;

public class HealthPotion : MonoBehaviour {
	public float HpBoost=100;
	GameObject Player;
	public GameObject Hitpart;

	// Use this for initialization
	void Start () {
		GameObject Player = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator WaitAndDestroy(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		Destroy(gameObject);
	}
	
	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
		Health hp=(Health)other.transform.GetComponent("Health");
		if(hp){
				//if(hp.health<hp.MaxHealth){		
				hp.health=Mathf.Min ( hp.health+HpBoost,100);
				//gameObject.GetComponentInChildren<txtfly>().fadestarted = true;
				Instantiate(Hitpart,transform.position,Quaternion.identity);
				//gameObject.GetComponentInChildren<ParticleEmitter>().emit = true;
				// particleSystem.particleEmitter.emit = true;
				gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
				
				//gameObject.GetComponent<MeshRenderer>().enabled = false;
				StartCoroutine(WaitAndDestroy(1f));
				//}
		}
	}
	}
}
