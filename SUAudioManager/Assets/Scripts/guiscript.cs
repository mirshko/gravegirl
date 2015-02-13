using UnityEngine;
using System.Collections;

public class guiscript : MonoBehaviour {
//	int curtype = 1;
//	int curtexture = 1;
	public Rect mixerrect = new Rect(10,10,40,200);
	public Rect healthhrect = new Rect(10,10,40,200);
	public Texture mixerbg;
	private int testwidth = 960;
	private int testheight = 480;
	bool showgui = false;
	GameObject theplayer = null;
	GameObject joy = null;
	Health playerhealth = null;
	public GUIStyle healthstyle = null;
	public Camera camg = null;
	public Camera camcs = null;


	// Use this for initialization
	IEnumerator WaitandStartMusic(float waitTime) 
	{
		//call like this:
		//StartCoroutine(WaitandStartMusic(2.0F));
		
		yield return new WaitForSeconds(waitTime);
		//play music here..

		if(theplayer != null)
		{
		
			//playerhealth = theplayer.GetComponent<Health>();

		//	GameObject joy = GameObject.FindGameObjectWithTag("GameController");
		//	if(joy != null)joy.SetActive(true);

		}
		else
		{
			Debug.Log("Cant get player in guiscript");
		}
	}
	IEnumerator WaitandShowui(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		playerhealth = theplayer.GetComponent<Health>();
		if(camg != null)camg.enabled = true;
		if(camcs != null)camcs.enabled = false;
	//	if(joy != null)joy.SetActive(true);
	//	else
	//	{
	//		Debug.Log("nothin tagged gamecontroller");
	//	}

	}


	IEnumerator Waitforsound()
	{
		bool played = false;
		bool done = true;
		print ("checking the sound in  Waitforsound at:" + Time.time);
		yield return new WaitForSeconds(1000);


		if(camg != null)camg.enabled = true;
		if(camcs != null)camcs.enabled = false;

		playerhealth = theplayer.GetComponent<Health>();
		
		if(joy != null)joy.SetActive(true);
		else
		{
			Debug.Log("nothin tagged gamecontroller");
		}
	}


	Rect ResizeGUI(Rect _rect)
	{
		float FilScreenWidth = _rect.width / testwidth;
		float rectWidth = FilScreenWidth * Screen.width;
		float FilScreenHeight = _rect.height / testheight;
		float rectHeight = FilScreenHeight * Screen.height;
		float rectX = (_rect.x / testwidth) * Screen.width;
		float rectY = (_rect.y / testheight) * Screen.height;
		
		return new Rect(rectX,rectY,rectWidth,rectHeight);
	}

	void Start ()
	{
		RenderSettings.skybox.SetFloat("_Blend", 1f);
		theplayer = GameObject.FindWithTag("Player");
		joy = GameObject.FindGameObjectWithTag("GameController");
	//	if(joy != null)joy.SetActive(false);
		StartCoroutine(WaitandStartMusic(1.0F));
		StartCoroutine(WaitandShowui(10.0F));
	//	StartCoroutine(Waitforsound());
		if(camg != null)camg.enabled = false;
		if(camcs != null)camcs.enabled = true;
	}

	void OnDestroy()
	{

	}
	void OnGUI()
	{
		if(playerhealth != null)
		{
			GUI.BeginGroup(ResizeGUI(healthhrect));
			GUI.Label(healthhrect,"Health :" + (int)playerhealth.health,healthstyle );
			GUI.EndGroup();
		}

	}
	
	// Update is called once per frame
	void Update()
	{
		//Debug.Log ("mouse at " +Input.mousePosition.x + " " +Input.mousePosition.y);
		if( Input.GetMouseButtonDown(0) )
		{

		}
		if( Input.GetMouseButtonUp(1) )
		{

		}
		if( Input.GetMouseButtonUp(2) )
		{
			showgui = !showgui;
		}
	}

}
