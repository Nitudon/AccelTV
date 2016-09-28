using UnityEngine;
using System.Collections;

public class SoundList : MonoBehaviour {

    [SerializeField]
    private AudioClip[] AudioClips;

    public static AudioSource audiosource;
    public static AudioClip[] audioclips;

    public static float[] AudioTimes;

    public static void AudioPlay(int index, float time)
    {
        audiosource.clip = audioclips[index];
        audiosource.time = time;
        audiosource.Play();
    }

    public static void AudioPause(int index)
    {
        audiosource.Stop();
        AudioTimes[index] = audiosource.time;
    }

	// Use this for initialization
	void Start () {
        AudioTimes = new float[5];
        audiosource = GetComponent<AudioSource>();
        audioclips = AudioClips;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
