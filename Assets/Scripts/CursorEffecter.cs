using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CursorEffecter : MonoBehaviour {
	public float Uplimit;
	public float Downlimit;
	public float speed;
	private Color CursorColor;
	private bool ColorTrigger = false;
	private Color alpha;
	// Use this for initialization
	void Start () {
		CursorColor = this.GetComponent<Image> ().color;
		alpha =  new Color (0f, 0f, 0f, speed);
	}
	
	// Update is called once per frame
	void Update () {
	
	if (ColorTrigger == false) {
			CursorColor = this.GetComponent<Image> ().color;
			CursorColor -= alpha;
			this.GetComponent<Image> ().color = CursorColor;
			if(CursorColor.a < Uplimit)
				ColorTrigger = true;
		}

		if (ColorTrigger == true) {
			CursorColor = this.GetComponent<Image> ().color;
			CursorColor += alpha;
			this.GetComponent<Image> ().color = CursorColor;
			if(CursorColor.a >Downlimit)
				ColorTrigger = false;
		}
	}
	
}
