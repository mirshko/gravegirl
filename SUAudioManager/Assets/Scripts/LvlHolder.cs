using UnityEngine;
using System.Collections;

public class LvlHolder : MonoBehaviour {

	// Use this for initialization
	public int lvl = 0;
	public int enemyperlvl = 0;
	public GameObject enemy1 = null;

	private float minx = -30;
	private float maxx = 34;
	private float minz = 10;
	private float maxz = 40;


	private GameObject theplayer = null;
	public float distancetospawnfromplayer = 10;
	GameObject navmesh = null;

	void Awake() {
	
		if(!PlayerPrefs.HasKey("lvl"))
		{
			PlayerPrefs.SetInt("lvl",1);
		}
		lvl = PlayerPrefs.GetInt("lvl");


		navmesh = GameObject.FindGameObjectWithTag("nav");
		theplayer = GameObject.FindGameObjectWithTag("Player");
		
		float xoff = (navmesh.transform.localScale.x ) *.5f;
		float zoff = (navmesh.transform.localScale.z ) *.5f;

		minx = navmesh.transform.position.x - xoff;
		maxx = navmesh.transform.position.x + xoff;
		minz = navmesh.transform.position.z - zoff;
		maxz = navmesh.transform.position.z + zoff;
		//Debug.Log ("availible navmesh xmin = "+ minx +" xmax = " + maxx + " minz = " + minz + " maxz = " + maxz);

		//need to create the enemies here b4 anything else starts..
		for (int y = 0; y < lvl * enemyperlvl; y++)
		{
				//randomly pic a spot.. but make it least distto start away..
			int xpos = (int)Random.Range(minx,maxx);
			int zpos = (int)Random.Range(minz,maxz);

			Vector3 pos = new Vector3(xpos,0,zpos);
			float dist = Vector3.Distance(pos,theplayer.transform.position);
			int tries = 0;
			while( dist < distancetospawnfromplayer)
			{
				xpos = (int)Random.Range(minx,maxx);
				zpos = (int)Random.Range(minz,maxz);
				pos = new Vector3(xpos,0,zpos);
				dist = Vector3.Distance(pos,theplayer.transform.position);
				tries++;
				if(tries > 100)
				{
					Debug.LogError("OVER 100 TRIES!!!");
				}
			}


			if(enemy1 != null)Instantiate(enemy1, new Vector3(xpos, 0, zpos), Quaternion.identity);
		}
	}

	void Start () 
	{
	
	}


	public void AddLevel()
	{
		lvl = PlayerPrefs.GetInt("lvl") + 1;
		PlayerPrefs.SetInt("lvl",lvl);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
