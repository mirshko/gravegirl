using UnityEngine;
using System.Collections;
using FMOD.Studio;

enum AudioType //Enumerator 
{
	SFX,
	Footsteps,
	Music,
	VO,
	IFX,
	UI,
	Ambience,
	Misc
};

}
public class AudioManager : MonoBehaviour 
{
	//Variables
	private static Hashtable emitterlist = null;

	//TODO
	//variables texture, type;

	void Start () 
	{
		emitterlist = new Hashtable();

		FMOD_StudioEventEmitter [] emitters = FindObjectsOfType(typeof(FMOD_StudioEventEmitter)) as FMOD_StudioEventEmitter[];

		int iemittercount = 0;

		foreach (FMOD_StudioEventEmitter emitter in emitters) //Finds The FMOD Emitters?
		{
			if(emitter.asset != null)
			{
				//Debug.Log("found " + emitter.asset.name + " on " + emitter.gameObject.name);

	            //emitter.CacheEventInstance();
	            // add the emitter to the hash..

	            string estring = emitter.gameObject.transform.parent.gameObject.GetHashCode() + "-" + emitter.asset.name;

	            emitterlist.Add(estring,emitter);
	        }
   		}
	}

	void Update () 
	{
	
	}
}
