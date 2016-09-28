using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class ProgramManager : MonoBehaviour {
    [SerializeField]
    private GameObject PlayInfoBar;

    [SerializeField]
    private Text Programname;

    [SerializeField]
    private Text Channelname;

    [SerializeField]
    private Button MenuButton;

    [SerializeField]
    private GameObject SetIcons;

    [SerializeField]
    private GameObject InfoCanvas;

    private MovieTexture[] ChannelLists;
    private int Channel;
    private int ChannelLength;
    private HandleController handle;
	// Use this for initialization
	void Start () {

        handle = GameObject.Find("TransHandle").GetComponent<HandleController>();
        ChannelLists = GameObject.Find("ChannelList").GetComponent<MovieList>().movieTexture;
        Channel = GameObject.Find("ChannelList").GetComponent<ChannelManager>().Channel;
        ChannelLength = GameObject.Find("ChannelList").GetComponent<ChannelManager>().ChannelLength;            

        var OnHoldInfoViewObservable =
            UIManager.OnHoldObservable(gameObject)
            .Where(_ => GameObject.Find("ChannelList").GetComponent<ChannelManager>().PlayMode == 2 && handle.Mode == 1)
            .Subscribe(_=> PlayInfoBar.SetActive(true));

        var OnMenuHoldObservable =
            UIManager.OnButtonHoldObservable(MenuButton, gameObject)
            .Subscribe(_ => SetIcons.SetActive(true));

        var OnMenuHoldBreakObservable =
            SetIcons.OnEnableAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ => SetIcons.SetActive(false));

        PlayInfoBar.OnEnableAsObservable()
            .Delay(System.TimeSpan.FromSeconds(5))
            .Subscribe(_ =>
            {
                if(GameObject.Find("ChannelList").GetComponent<ChannelManager>().PlayMode == 2)
                PlayInfoBar.SetActive(false);
            });

       
    }
	
    private IEnumerator InfoBarFade()
    {
        InfoCanvas.SetActive(true);
        PlayInfoBar.SetActive(true);
        yield return new WaitForSeconds(5f);
        InfoCanvas.SetActive(false);
        PlayInfoBar.SetActive(false);
        yield break;
        
            
    }


    private void TextView()
    {

        Channel = GameObject.Find("ChannelList").GetComponent<ChannelManager>().Channel;
        ChannelLength = GameObject.Find("ChannelList").GetComponent<ChannelManager>().ChannelLength;

        if (Channel < 0)
        {
            Channel = ChannelLength - 1;
        }
        if (Channel >= ChannelLength)
        {
            Channel = 0;
        }
        Channelname.text = (Channel+1).ToString() + "Ch";
        Programname.text = ChannelLists[Channel].name;
    }

	// Update is called once per frame
	void Update () {
        TextView();
	}
}
