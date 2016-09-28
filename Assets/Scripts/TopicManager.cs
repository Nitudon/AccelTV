using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
public class TopicManager : MonoBehaviour {


    private int index;
    private int oldindex;
    private bool oldstair;
    private bool stair;
    private ChannelManager manager;

    [SerializeField]
    private GameObject Uppop;

    [SerializeField]
    private GameObject Downpop;

    [SerializeField]
    private GameObject[] UpNodes;

    [SerializeField]
    private GameObject[] DownNodes;

    [SerializeField]
    private GameObject[] UpPops;

    [SerializeField]
    private GameObject[] DownPops;

    private  List<Animator> UpAnimators;

    private List<Animator> DownAnimators;

    private List<Animator> UpPopAnimators;

    private List<Animator> DownPopAnimators;
    // Use this for initialization1
    void Start () {

        UpPopAnimators = new List<Animator>();
        DownPopAnimators = new List<Animator>();
        UpAnimators = new List<Animator>();
        DownAnimators = new List<Animator>();
        for(int i = 0; i < UpNodes.Length; i++)
        {
            UpAnimators.Add(UpNodes[i].GetComponent<Animator>());
        }
        for (int i = 0; i < UpNodes.Length; i++)
        {
            DownAnimators.Add(DownNodes[i].GetComponent<Animator>());
        }
        for (int i = 0; i < UpNodes.Length; i++)
        {
            UpPopAnimators.Add(UpPops[i].GetComponent<Animator>());
        }
        for (int i = 0; i < UpNodes.Length; i++)
        {
            DownPopAnimators.Add(DownPops[i].GetComponent<Animator>());
        }
        index = 0;
        oldstair = true;
        oldindex = 0;
        stair = true;
        manager = GameObject.Find("ChannelList").GetComponent<ChannelManager>();
        UpAnimators[0].SetTrigger("MoveTrigger");
        Observable.Interval(System.TimeSpan.FromSeconds(0.45f))
            .Subscribe(_ => IndexState());
	}
	
    private void IndexState()
    {
        if (manager.StartAccel.x - Input.acceleration.x > 0.3f)
        {
            if (index > 0)
                index--;

        }

        if (manager.StartAccel.x - Input.acceleration.x < -0.3f)
        {
            if (index < UpNodes.Length-1)
            index++;
        }

        if (manager.StartAccel.y - Input.acceleration.y > 0.18f)
        {
            stair = false;
        }

        if (manager.StartAccel.y - Input.acceleration.y < -0.18f)
        {
            stair = true;
        }
    }

	// Update is called once per frame
	void Update () {
        if(oldindex != index)
        {
            if (stair) {
                UpAnimators[index].SetTrigger("MoveTrigger");
               
                UpPopAnimators[index].SetTrigger("MoveTrigger");
                if (oldstair != stair)
                {
                    DownAnimators[oldindex].SetTrigger("MoveTrigger");
                    DownPopAnimators[oldindex].SetTrigger("BackTrigger");
                }
                
                else
                {
                    UpAnimators[oldindex].SetTrigger("MoveTrigger");
                    UpPopAnimators[oldindex].SetTrigger("BackTrigger");
                }


            }
            else
            {
                DownAnimators[index].SetTrigger("MoveTrigger");
                
                DownPopAnimators[index].SetTrigger("MoveTrigger");
                if (oldstair != stair)
                {
                    UpAnimators[oldindex].SetTrigger("MoveTrigger");
                    UpPopAnimators[oldindex].SetTrigger("BackTrigger");
                }
                else
                {
                    DownAnimators[oldindex].SetTrigger("MoveTrigger");
                    DownPopAnimators[oldindex].SetTrigger("BackTrigger");
                }
            }

            oldindex = index;
            oldstair = stair;
        }

       else if(oldstair != stair)
        {
            if (stair)
            {
                DownPopAnimators[index].SetTrigger("BackTrigger");
                UpPopAnimators[index].SetTrigger("MoveTrigger");
            }
            else
            {
                DownPopAnimators[index].SetTrigger("MoveTrigger");
                UpPopAnimators[index].SetTrigger("BackTrigger");
            }
            DownAnimators[index].SetTrigger("MoveTrigger");
            UpAnimators[index].SetTrigger("MoveTrigger");
            oldstair = stair;
            oldindex = index;

        }
	    
    }
}
