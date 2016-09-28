using UnityEngine;
using System.Collections;
using DG.Tweening;
using UniRx;
using UnityEngine.SceneManagement;

public class StartLogo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.DOLocalJump(new Vector3(0f, 0, 0), 200, 3, 2f).SetEase(Ease.Linear);

        Observable.Timer(System.TimeSpan.FromSeconds(2.2f))
            .Subscribe(_ => SceneManager.LoadScene("deviceTV"));

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
