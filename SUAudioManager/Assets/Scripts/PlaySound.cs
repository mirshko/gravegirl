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
		rayDirection = transform.position + ((-transform.up) * ViewDistance);
		GetTextureName (currentTexture);

		
		Debug.DrawLine(transform.position, rayDirection, Color.green);
	
	}
	void PlayMyEmitter()
	{
		string fstring = (gameObject.transform.parent.gameObject.GetHashCode() + "-" + currentTexture).ToString(); //Grabs Hash Code Of Parent Object And Assigns It To The fstring Variable

		AudioManager.PlayEmitter(fstring); //Plays Emitter From Parent
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
