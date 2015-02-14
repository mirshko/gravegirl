using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class PlaySound : MonoBehaviour
{
	void PlayMyEmitter()
	{
		string fstring = gameObject.transform.parent.gameObject.GetHashCode().ToString(); //Grabs Hash Code Of Parent Object And Assigns It To The fstring Variable

		AudioManager.PlayEmitter(fstring); //Plays Emitter From Parent
	}
}