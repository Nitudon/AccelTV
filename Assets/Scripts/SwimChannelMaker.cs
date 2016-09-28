using UnityEngine;
using System.Collections;
using UniRx;

public class SwimChannelMaker : MonoBehaviour {

    [SerializeField]
    private GameObject _preChannel;

    private int ChannelIndex;

    private void ChannelCreate()
    {
        GameObject channel = Instantiate(_preChannel) as GameObject;
        float RandX = Random.Range(0f,1f);
        channel.transform.position = new Vector3(RandX,channel.transform.position.y,channel.transform.position.z);
    }

	// Use this for initialization
	void Start () {
        ChannelIndex = 0;

        var TimeIntervalCreateObservable =
            Observable.Interval(System.TimeSpan.FromSeconds(8))
            .Subscribe(_ => ChannelCreate());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
