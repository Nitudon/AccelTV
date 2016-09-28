using UnityEngine;
using System.Collections;
using UniRx;

public class HandleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIManager.OnInputTouchDownObservable()
            .Subscribe(_ => transform.localEulerAngles += new Vector3(0f,0f,90f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
