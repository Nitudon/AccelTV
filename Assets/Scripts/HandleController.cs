using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using DG.Tweening;

public class HandleController : MonoBehaviour {

    [SerializeField]
    private GameObject ProgramBar;

    [SerializeField]
    private GameObject ChannelList;

    [SerializeField]
    private GameObject[] HandleNode;

    [SerializeField]
    private GameObject SwimChannels;

    [SerializeField]
    private GameObject ProgramChannels;

    public int Mode { get; set; }
    private ChannelManager manager;
    private Animator HandleAnimator;
    private List<Animator> NodeAnimators;
    private GameObject Handle;
    private int OldMode;
    private int AccelState;
    private bool AccelStart;

    // Use this for initialization
    void Start() {
        AccelStart = false;
        manager = GameObject.Find("ChannelList").GetComponent<ChannelManager>();
        HandleAnimator = transform.FindChild("Handle").GetComponent<Animator>();
        Handle = transform.FindChild("Handle").gameObject;
        NodeAnimators = new List<Animator>();
        foreach (GameObject elm in HandleNode)
        {
            NodeAnimators.Add(elm.GetComponent<Animator>());
        }
        AccelState = 0;

        Mode = 1;
        OldMode = 1;

        Handle.OnEnableAsObservable()
            .Subscribe(_ => manager.PlayMode = 3);

        Observable.EveryUpdate()
            .Subscribe(_ => AccelControll());

        Observable.EveryUpdate()
            .Where(_ => Handle.active && !manager.SetTint.active)
            .Subscribe(_ => HandleRotate());

        UIManager.OnInputTouchDownObservable()
            .Where(_ => manager.PlayMode != 2 && Input.mousePosition.y < 100f && !manager.SetTint.active)
            .Subscribe(_ =>
            {
                HandleNode[Mode].transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f);
                Handle.SetActive(true);
            });

        UIManager.OnInputTouchUpObservable()
            .Where(_ => Handle.active && !manager.SetTint.active)
            .Subscribe(_ => ModeChange());
     
	} 
	
    private void ModeChange()
    {
            switch (Mode)
            {
                case 0:
                    ProgramBar.SetActive(false);
                    ChannelList.SetActive(false);
                    ProgramChannels.SetActive(false);
                    SwimChannels.SetActive(true);
                    break;

                case 1:
                    ProgramBar.SetActive(true);
                    ChannelList.SetActive(true);
                    ProgramChannels.SetActive(false);
                    SwimChannels.SetActive(false);
                    break;

                case 2:
                    ProgramBar.SetActive(false);
                    ChannelList.SetActive(false);
                    ProgramChannels.SetActive(true);
                    SwimChannels.SetActive(false);
                    break;
            }
           
        manager.PlayMode = 0;
        Handle.SetActive(false);

    }

    private void HandleRotate()
    {
        Mode = AccelState;
        HandleNode[OldMode].transform.DOScale(new Vector3(1f,1f,1f),0.3f);
        HandleNode[Mode].transform.DOScale(new Vector3(2f, 2f, 2f), 0.3f);
        OldMode = Mode;
    }

    private void AccelControll()
    {

        if (manager.StartAccel.x - Input.acceleration.x > 0.3f)
            AccelState = 0;
        else if (manager.StartAccel.x - Input.acceleration.x < -0.3f)
            AccelState = 2;
        else
            AccelState = 1;

    }
	// Update is called once per frame
	void Update () {
        if (Mode < 0)
        {
            Mode = 2;
        }
        else if (Mode == 3)
        {
            Mode = 0;
        }
    }
}
