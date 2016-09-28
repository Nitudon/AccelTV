using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ChannelManager : InputEventHandler
{

    [SerializeField]
    private MovieTexture[] Movies;

    [SerializeField]
    private GameObject[] Channels;

    [SerializeField]
    private int ChannelIndex;

    [SerializeField]
    private GameObject ChannelMenu;

    [SerializeField]
    private GameObject InfoBar;

    [SerializeField]
    private GameObject PlayMovie;

    [SerializeField, Range(0f,1.0f)]
    private float Speed;

    [SerializeField]
    private GameObject KeyIcon;

    [SerializeField]
    private GameObject PlayInfoBar;

    [SerializeField]
    private GameObject InfoCanvas;

    public Vector3 StartAccel { get; set; }
    public int PlayMode { get; set;}
    public int Channel { get; set; }
    public int ChannelLength { get; set; }
    public GameObject SetTint;
    private int lastindex, firstindex;
    private const float SelectBorder = 0.3f;
    private int[] InputAccel;
    private List<GUITexture> ChannelTextures;
    private HandleController handle;
    enum Changemode :int{channel,program,play,change};
    enum Accelmode :int {x,y,z};

    private IEnumerator ChannelFade()
    {
        if (!PlayMovie.active)
        {
            InfoBar.SetActive(false);
            ChannelMenu.SetActive(false);
            PlayMode = (int)Changemode.play;
            PlayMovie.SetActive(true);
            PlayMovie.transform.DOScale(new Vector3(1f, 1.2f, 1f), 0.3f);
            foreach (GameObject elm in Channels)
            {
                elm.SetActive(false);
            }
        }
        else
        {
            ChannelMenu.SetActive(true);
            InfoBar.SetActive(true);
            PlayMode = (int)Changemode.channel;
            PlayMovie.GetComponent<RawImage>().texture = Movies[Channel];
            PlayMovie.transform.DOScale(new Vector3(0f,0f,0f),0.3f);
            foreach (GameObject elm in Channels)
            {
                elm.SetActive(true);
            }
            yield return new WaitForSeconds(0.3f);
            PlayMovie.SetActive(false);
        }
        yield break;
    }

    private void SetAccel()
    {
        SetTint.gameObject.SetActive(false);
        StartAccel = Input.acceleration;
    }

    private int[] ControllAccel()
    {
        int[] AccelSet = new int[3];

        if (StartAccel.x - Input.acceleration.x < -SelectBorder)
        {
            AccelSet[(int)Accelmode.x] = 1;
        }
        else if (StartAccel.x - Input.acceleration.x > SelectBorder)
        {
            AccelSet[(int)Accelmode.x] = -1;
        }
        else
        {
            AccelSet[(int)Accelmode.x] = 0;
        }
        return AccelSet;
    }

    private void ChannelChange()
    {
        Channel += ControllAccel()[(int)Accelmode.x];
        if (Channel < 0)
        {
            Channel = Channels.Length - 1;
        }
        if (Channel >= Channels.Length)
        {
            Channel = 0;
        }
        
        AnimationControll(ControllAccel()[(int)Accelmode.x],(int)Changemode.channel);
    }

    private void AnimationControll(int accel,int mode)
    {
        switch (mode)
        {
            case (int)Changemode.channel:
                if(accel == -1)
                {

                    Channels[firstindex].transform.localPosition += new Vector3(3500f, 0f, 0f);
                    ChannelMenu.transform.DOLocalMoveX(-700f, 0.5f).SetEase(Ease.InOutQuart).SetRelative(true);
                    firstindex++;
                    lastindex++;
                    if (firstindex >= Channels.Length)
                    {
                        firstindex = 0;
                    }

                    if (lastindex >= Channels.Length)
                    {
                        lastindex = 0;
                    }
                }
                else if(accel == 1)
                {
                    Channels[lastindex].transform.localPosition -= new Vector3(3500f, 0f, 0f);
                    ChannelMenu.transform.DOLocalMoveX(700f, 0.5f).SetEase(Ease.InOutQuart).SetRelative(true);
                    lastindex--;
                    firstindex--;
                    if (lastindex < 0)
                    {
                        lastindex = Channels.Length - 1;
                    }
                    if (firstindex < 0)
                    {
                        firstindex = Channels.Length - 1;
                    }
                }
                break;

            case (int)Changemode.change:
                
                break;


            case (int)Changemode.program:
                StartCoroutine(ChannelFade());
                break;

            case (int)Changemode.play:
                StartCoroutine( ChannelFade());
                break;
        }
        
    }

    // Use this for initialization
    void Start() {
        StartAccel = Input.acceleration;
        ChannelLength = Channels.Length;
        lastindex = Channels.Length - 1;
        firstindex = 0;
        Channel = 0;
        PlayMode = (int)Changemode.channel;
        ChannelTextures = new List<GUITexture>();
        handle = GameObject.Find("TransHandle").GetComponent<HandleController>();
        for (int i = 0; i < Channels.Length; ++i)
        {
            ChannelTextures.Add(Channels[i].GetComponent<GUITexture>());
        }

        KeyIcon.OnDisableAsObservable()
            .Subscribe(_ => StartAccel = Input.acceleration);

        UIManager.OnHoldObservable(gameObject)
            .Where(_ => Input.mousePosition.y > 100f && PlayMode  != (int)Changemode.change && handle.Mode == 1)
            .Subscribe(_ => KeyIcon.SetActive(!KeyIcon.active));

        var SingleTapObservable =
        UIManager.OnTapObservable()
            .Where(_ => Input.mousePosition.y > 100f  && (PlayMode != (int)Changemode.change) && !SetTint.gameObject.active && handle.Mode == 1)
            .Subscribe(_ => AnimationControll(0,(int)Changemode.play));

        UIManager.OnInputTouchDownObservable()
            .Where(_ => SetTint.gameObject.active)
            .Delay(System.TimeSpan.FromSeconds(0.01f))
            .Subscribe(_ => SetAccel());

        Observable
            .Timer(System.TimeSpan.FromSeconds(0), System.TimeSpan.FromSeconds(Speed))
            .Where(_ => !(SetTint.gameObject.active) && PlayMode != (int)Changemode.change && handle.Mode == 1 && !KeyIcon.active)
            .Subscribe(_ => ChannelChange());

       
    }       

	// Update is called once per frame
	void Update () {

        if (Channel < 0)
        {
            Channel = Channels.Length - 1;
        }
        if (Channel >= Channels.Length)
        {
            Channel = 0;
        }
        PlayMovie.GetComponent<RawImage>().texture = Movies[Channel];

//if (Input.GetKeyDown(KeyCode.Z))
//        {
//            Channel--;
//            Channels[firstindex].transform.localPosition += new Vector3(6f,0f,0f);
//            MenuAnimator.SetTrigger("BackTrigger");
//            firstindex++;
//            lastindex++;
//            if (firstindex >= Channels.Length)
//            {
//                firstindex = 0;
//            }

//            if (lastindex >= Channels.Length)
//            {
//                lastindex = 0;
//            }
//        }
            
//        if (Input.GetKeyDown(KeyCode.X))
//        {
//            Channel++;
//            Channels[lastindex].transform.localPosition -= new Vector3(6f, 0f, 0f);
//            MenuAnimator.SetTrigger("MoveTrigger");
//            lastindex--;
//            firstindex--;
//            if (lastindex < 0)
//            {
//                lastindex = Channels.Length-1;
//            }
//            if (firstindex < 0)
//            {
//                firstindex = Channels.Length - 1;
//            }
//        }
    }
}
