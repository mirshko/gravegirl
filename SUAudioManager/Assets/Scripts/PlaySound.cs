using UnityEngine;
using System.Collections;
using FMOD.Studio;
using System.Collections.Generic;
using System;

public class PlaySound : MonoBehaviour
{
	public RaycastHit hit;
	public int ViewDistance = 2;
	private static Hashtable textureValue = null;
	private Vector3 rayDirection;
	private string currentTexture;
	private int textureID;



	void Start () 
	{
		
		
		textureValue = new Hashtable();


		
		string[] lines = System.IO.File.ReadAllLines(@"FILEPATH");
		string[][] lineSplit = new string[lines.Length][];

		int lineNum = 0;
		foreach (string line in lines)
		{
		lineSplit[lineNum++] = line.Split(',');
			textureValue.Add (lineSplit[lineNum++][0],Convert.ToInt32(lineSplit[lineNum++][1]));
		}
	}

	void Update()
	{
		/*
		rayDirection = transform.position + ((-transform.up) * ViewDistance);
		GetTextureName (currentTexture);

		
		Debug.DrawLine(transform.position, rayDirection, Color.green);
		*/
	
	}
	void PlayFootsteps()
	{
		string fstring = (gameObject.transform.parent.gameObject.GetHashCode() + "-" + "FOOTSTEPS").ToString();
		AudioManager.PlayEmitter (fstring);

	}
	/*
	void PlayMusic(AudioManager.AudioType enumValue)
	{
		enumValue = AudioManager.AudioType.music;
		string fstring = (gameObject.transform.parent.gameObject.GetHashCode() + "-" + "music").ToString();
		AudioManager.PlayEmitter (fstring);
	}
	*/
	void PlayVO()
	{
		string fstring = (gameObject.transform.parent.gameObject.GetHashCode() + "-" + "VO").ToString();
		AudioManager.PlayEmitter (fstring);
	}
	void PlayIFX(GameObject objHitting)
	{
		int hitObjectType = 0;
		hitObjectType = GetIDForTranslation ("hit" + gameObject.name);
		AudioManager.setTexture(gameObject.transform.parent.gameObject.GetHashCode() + "-" + "IFX", (float)hitObjectType);

		if (objHitting != null) 
		{
			hitObjectType = GetIDForTranslation("hitter" + objHitting.name);
			AudioManager.setType(gameObject.transform.parent.gameObject.GetHashCode() + "-" + "IFX", (float)hitObjectType);

			}
		string fstring = (gameObject.transform.parent.gameObject.GetHashCode() + "-" + "IFX").ToString();
		AudioManager.PlayEmitter (fstring);
	}
	void PlayVFX()
	{
		string fstring = (gameObject.transform.parent.gameObject.GetHashCode() + "-" + "VFX").ToString();
		AudioManager.PlayEmitter (fstring);
	}
/*	void PlayAmbience()
	{
		string fstring = (gameObject.transform.parent.gameObject.GetHashCode() + "-" + "ambience").ToString();
		AudioManager.PlayEmitter (fstring);
	}
	*/
	void PlayMovement()
	{
		string fstring = (gameObject.transform.parent.gameObject.GetHashCode() + "-" + "movement").ToString();
		AudioManager.PlayEmitter (fstring);
	}
	void PlayDeath()
	{
		string fstring = (gameObject.transform.parent.gameObject.GetHashCode() + "-" + "death").ToString();
		AudioManager.PlayEmitter (fstring);
	}
/*	void PlayMyEmitter()
	{
		string fstring = (gameObject.transform.parent.gameObject.GetHashCode() + "-" + currentTexture).ToString(); //Grabs Hash Code Of Parent Object And Assigns It To The fstring Variable

		AudioManager.PlayEmitter(fstring); //Plays Emitter From Parent
	}
*/

	int GetIDForTranslation(string toTranslate)
	{
		int retValue = -1;

		string sNumber = (string)textureValue [toTranslate];
		if (!int.TryParse (toTranslate, out retValue)) 
		{
			Debug.Log ("No Translation for " + toTranslate);
		}

		return retValue;
	}
	void GetTextureName (string textureName)
	{
		if (Physics.Raycast (transform.position, rayDirection, ViewDistance)) {
			Texture tempText = (hit.collider.gameObject.transform.parent.gameObject.renderer.material.GetTexture ("_MainTex"));
			textureName = (tempText.name).ToString();

		} 
		else
		{
			Debug.Log ("No Texture Found");
		}
	}
}
