using UnityEngine;
using System.Collections;
using UniRx;

public class SwimChannelMover : MonoBehaviour {

    public int MyChannel { get; set; }
    private MovieTexture[] Movies;
    private void MovieControll()
    {
        transform.position = new Vector3(transform.position.x,-0.15f,transform.position.z);
        gameObject.GetComponent<GUITexture>().texture = Movies[SwimManager.Channel%Movies.Length];
        MyChannel = SwimManager.Channel % Movies.Length;
        SwimManager.Channel++;
    }

	// Use this for initialization
	void Start () {
        MyChannel = 0;
        Movies = GameObject.Find("ChannelList").GetComponent<MovieList>().movieTexture;

        Observable.EveryUpdate()
            .Where(_ => transform.position.y > 1.3f)
            .Subscribe(_ => MovieControll());
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
