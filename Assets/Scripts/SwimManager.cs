using UnityEngine;
using System.Collections;

public class SwimManager : MonoBehaviour {

    public static int Channel;

    [SerializeField]
    Camera MainCamera;

    [SerializeField]
    private GameObject[] Swims;

    public void test()
    {
        Debug.Log("dsfas");
    }

	// Use this for initialization
	void Start () {
        Channel = 3;
	}
	
	// Update is called once per frame
	void Update () {
        

    }
}
