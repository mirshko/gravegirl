using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Entities;
using System.Collections.Generic;
[ExecuteInEditMode()] 
public class Health : MonoBehaviour 
{
	public float health = 100f;
//	public float points = 1f;
	public float strength = 30f;
	public float speed = 1f;
//	bool indonedamage = false;
	int attacklevel = 1;
	public int maxcombo = 4;
//	float donedamagetime = 0;
	
	public float atkturnstr = .5f;
	public bool facetarget = false;
//	public GUIStyle thisFont;
	private AIRig aiRig = null;
//	private crActions dlgact = null;
	private bool dead = false;

	public GameObject target = null;
	//private Hashtable myHashtable = null;
	private bool fademusicintenstiy = false;
	private bool rampmusicintenstiy = false;
	private List<GameObject>goList;
//	private Health enemyHealth = null;
	public ParticleEmitter blood1 = null;
	public ParticleEmitter blood2 = null;
	public bool usecolisionmusic = true;
	public Transform powerup1 = null;
	public Transform powerup2 = null;
	public Transform powerup3 = null;
	private bool isPlayer = false;
	//public Rect myhealthrect;
	// Use this for initialization

	void Start () 
	{
		if(gameObject.tag == "Player")
		{
			isPlayer = true;
		}

		if(isPlayer)
		{
			goList = new List<GameObject>();
		}
		else
		{
			target = GameObject.FindGameObjectWithTag("Player");
		}
	}

	IEnumerator WaitAndDestroy(float waitTime)
	{
		if(isPlayer)
		{
			yield return new WaitForSeconds(waitTime);

			

			// now destroy all the ememies
			foreach(GameObject go in goList)
			{
				Destroy (go);
			}
			goList.Clear(); ;


		}
		else
		{
		yield return new WaitForSeconds(waitTime);
		GameObject par = null;

		if(gameObject.transform.parent != null)
			par = gameObject.transform.parent.gameObject;

		Destroy (gameObject);
		if(par != null)Destroy(par);
		}
	}

	void Update () 
	{
		if(aiRig == null)aiRig = gameObject.GetComponentInChildren<AIRig>();
		if(aiRig != null)aiRig.AI.WorkingMemory.SetItem("health", health);

		if(fademusicintenstiy && isPlayer){
			

		}
		if(rampmusicintenstiy && isPlayer){
			

		}
	}
	
	public void Damage(float damage)
	{

		health = Mathf.Clamp(health - damage, 0, health);
		if( blood1 != null && blood2 != null)
		{
			blood1.Emit();
			blood2.Emit();
		}
		if(isPlayer)target = goList[0];
		//target is what hit me..
		if(target != null)
		{
			gameObject.SendMessage("PlayHitSound",target);
			//if(!SUAudioManager.Playhit(target,this.gameObject))
			//{
			//	Debug.Log("Play hit failed for " + target.name + " hitting " + this.gameObject);
			//}
		}
		else
		{
			Debug.Log("No hit target for " + gameObject.name);
		}
	//	if(!SUAudioManager.PlayEmitter( this.gameObject.GetHashCode(),SUAudioManager.soundevent.hit ))
	//	{
	//		//	Debug.Log("PlayHit failed on " + this.gameObject.GetHashCode());
	//	}
	

			//Debug.Log("Playhit called on: " + gameObject.name); 

		if(health == 0 && !dead )
		{
				//if we are the enemy send a message to the target telling it it killed us
			if(!isPlayer)
			{
				target.SendMessage("enimyisdead",gameObject);
			}

			dead = true;
			//animation.Stop();
			//animation.Play();
			animation.CrossFade("die");
			if(isPlayer){
				Debug.Log ("stopping control and AI rig");
				//no more move..
				ThirdPersonController control = gameObject.GetComponent<ThirdPersonController>();
				control.enabled = false;
				JoystickEvent joysc = gameObject.GetComponent<JoystickEvent>();
				joysc.enabled = false;

				// no more let them attact..
				//EntityRig myAIrig = target.GetComponent<EntityRig>();
				//if(myAIrig != null)myAIrig.enabled = false;
				GameObject joy = GameObject.FindGameObjectWithTag("GameController");
				joy.SetActive(false);


			}
			else
			{
				//	Debug.Log ("died");
				if(aiRig != null)aiRig.AI.IsActive = false;
	//			if(dlgact != null)dlgact.CanAttack = false;
				//SD HERE randomize reward...
				bool drop = false;
				//how many points helps chance of getting reward
				int chance = Random.Range(0,10);
				if(chance < 8 )drop = true;

				if(drop)
				{
					//if getting reward pick strength, speed, or health
					//drop a potion..
					int sel = Random.Range(1,4);
					if(sel == 1)
					{
						if(powerup1 != null)Instantiate(powerup1, new Vector3(transform.position.x, transform.position.y+ .5f,transform.position.z), Quaternion.identity);
					}
					if(sel == 2)
					{
						if(powerup2 != null)Instantiate(powerup2, new Vector3(transform.position.x, transform.position.y+ .5f,transform.position.z), Quaternion.identity);
					}
					if(sel == 3)
					{
						if(powerup3 != null)Instantiate(powerup3, new Vector3(transform.position.x, transform.position.y+ .5f,transform.position.z), Quaternion.identity);
					}
				}
			}
//			UDEA.GameKeys.Add("skelkills",1);
			animation.CrossFade("die");
			StartCoroutine(WaitAndDestroy(4.0F));

		}
		else
		{
		
		}
	}




	void LateUpdate () {

		//if(facetarget && target != null)
		//{
		//	Quaternion targetRotation = Quaternion.LookRotation (target.transform.position - transform.position);
		//	float str = Mathf.Min (atkturnstr * Time.deltaTime, 2);
		//	transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, str);
		//}
	}
	int atk = 1;
	public void playerattacks()
	{

		if (atk > maxcombo) atk = 1;
		if( atk < 1) atk = 1;

		if(!animation.IsPlaying("attack1") 
		        && !animation.IsPlaying("attack2") 
		        && !animation.IsPlaying("attack3")&& !animation.IsPlaying("attack4")&& !animation.IsPlaying("attack5"))
		{
			facetarget = true;
			animation.Stop();
			animation.Play("attack" + atk);
			animation.CrossFadeQueued("idle");
			atk++;
		}
	}

	public void enimydoesdamage()
	{
		if(target != null)	target.SendMessage("Damage",strength * attacklevel);
		else Debug.Log("enemy has no target!");
	}

	public void enimyisdead(GameObject enemy)
	{
		foreach(GameObject go in goList)
		{
			if(go.GetHashCode() == enemy.GetHashCode())
			{
				goList.Remove(enemy) ;
				facetarget = false;
				break;
			}
		}
		if(goList.Count <= 0 && usecolisionmusic)
		{
			fademusicintenstiy = true;
			facetarget = false;
			//Debug.Log ("Fading intesnsity");
			//SUAudioManager.SetEmitterTypeParam(gameObject.GetHashCode(),SUAudioManager.soundevent.music,0);
		}
		//Debug.Log("enimyisdead " + enemy.name + " removed. still " + goList.Count +  "enemies in golist");

	}


	public void playerdoesdamage()
	{
		if(!isPlayer)
		{
			Debug.Log("player does damage called from not the player");
			return;
		}
	
		if(goList.Count > 0)
		{
			target = goList[0];
			if(target != null)
			{
				target.SendMessage("Damage",strength * attacklevel);
			}
			else
			{
				goList.Remove(target) ;
				playerdoesdamage();
			}
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if(!isPlayer)
		{
			return;
		}
		if(other.name.Contains("musiczone"))
		{


			return;
		}
			bool addit = true;
	
			foreach(GameObject go in goList)
			{
				if(go.GetHashCode() == other.gameObject.GetHashCode())
				{
					addit = false;
					break;
				}
			}
			Health he = other.GetComponentInChildren<Health>();//other.gameObject.GetComponent<Health>();

			if(he == null)
			{
				addit = false;
			}
			if(addit)
			{
			if(usecolisionmusic && isPlayer)
			{
				rampmusicintenstiy = true;
			
			}
			goList.Add(other.gameObject);
			//Debug.Log("OnTriggerEnter :" + other.name + " added." );
			}
	}
	void OnTriggerExit(Collider other)
	{
		if(!isPlayer)
		{
			return;
		}
		if(other.name.Contains("musiczone"))
		{
		
			return;
		}
		foreach(GameObject go in goList){
			if(go.GetHashCode() == other.gameObject.GetHashCode())
			{
				goList.Remove(go) ;
				break;
			}
		}
		if(goList.Count <= 0 )
		{
			if(usecolisionmusic)fademusicintenstiy = true;
			facetarget = false;
		}
		//Debug.Log("OnTriggerExit " + other.name + " removed." );
	}
}
