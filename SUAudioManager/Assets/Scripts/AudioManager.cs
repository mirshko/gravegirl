using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class AudioManager : MonoBehaviour 
{
	//Variables
	private static Hashtable emitterlist = null;
	private static Hashtable textureParam = null;
	private static Hashtable typeParam = null;
	private static FMOD_StudioSystem defaultStudioSystem = null;


	//TODO
	//Variables texture, type;

	public enum AudioType //Enumerators for sound types
	{
		footsteps,
		music,
		VO,
		IFX,
		VFX,
		ambience,
		movement,
		death
	};

	void Start () 
	{


		emitterlist = new Hashtable();
		typeParam = new Hashtable ();
		textureParam = new Hashtable ();

		defaultStudioSystem = FMOD_StudioSystem.instance;

		FMOD_StudioEventEmitter [] emitters = FindObjectsOfType (typeof(FMOD_StudioEventEmitter)) as FMOD_StudioEventEmitter[]; //Find all FMOD Emitters

		int iemittercount = 0;

		foreach (FMOD_StudioEventEmitter emitter in emitters) //for each FMOD emitter found 
		{
			if (emitter.asset != null)
			{
				//Debug.Log("found " + emitter.asset.name + " on " + emitter.gameObject.name);

	            //emitter.CacheEventInstance();
	            // add the emitter to the hash..

				string estring = (emitter.gameObject.transform.parent.gameObject.GetHashCode() + "-" + emitter.asset.name).ToString();
				
				emitterlist.Add(estring,emitter);

				FMOD.Studio.ParameterInstance Pi = null;
				try
				{
					Pi = emitter.getParameter("type");
				}
				
				catch
				{

				}
				if (Pi != null)
				{
					typeParam.Add (estring, Pi);
				}

				Pi = null;
				try
				{
					Pi = emitter.getParameter("texture");
				}
				
				catch
				{
					
				}
				if (Pi != null)
				{
					textureParam.Add (estring, Pi);
				}
	        }
   		}
	}

	public static void AddEmitter(GameObject newObject)
	{
		FMOD_StudioEventEmitter[] emitters = newObject.GetComponentsInChildren<FMOD_StudioEventEmitter> ();
		foreach (FMOD_StudioEventEmitter emitter in emitters)
		{
			if(emitter.asset != null)
			{
				string estring = (emitter.gameObject.transform.parent.gameObject.GetHashCode() + "-" + emitter.asset.name).ToString();
				
				emitterlist.Add(estring,emitter);
				
				FMOD.Studio.ParameterInstance Pi = null;
				try
				{
					Pi = emitter.getParameter("type");
				}
				
				catch
				{
					
				}
				if (Pi != null)
				{
					typeParam.Add (estring, Pi);
				}
				
				Pi = null;
				try
				{
					Pi = emitter.getParameter("texture");
				}
				
				catch
				{
					
				}
				if (Pi != null)
				{
					textureParam.Add (estring, Pi);
				}
			}
		}

	}

	public static void setType(string estring, float val)
	{


				FMOD.Studio.ParameterInstance pi = (FMOD.Studio.ParameterInstance)typeParam[estring];

				if (pi != null) 
				{
						FMOD.RESULT res = pi.setValue (val);
						
				}
	}
	public static void setTexture(string estring, float val)
	{
			
				FMOD.Studio.ParameterInstance pi = (FMOD.Studio.ParameterInstance)textureParam[estring];
				if (pi != null) 
				{
						FMOD.RESULT res = pi.setValue (val);
				}
	}


	public static bool PlayEmitter(string estring)  //Called To Play A Sound From Designated Emitter
	{
		FMOD_StudioEventEmitter EmitterToPlay = (FMOD_StudioEventEmitter) emitterlist[estring];

		EmitterToPlay.Stop(); //Stop Sound
		EmitterToPlay.Play(); //Play Sound

		return true;
	}

	void Update () 
	{


		if (Input.GetKeyDown ("4"))
		{
			DumpHash(emitterlist);
		}

	}

	void DumpHash (Hashtable HashtableHold)
	{
		HashtableHold.Clear ();
	}


	                    
}

