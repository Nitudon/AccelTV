using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : UIBehaviour 
{
	[SerializeField]
	Text uiText,timeText;

	[SerializeField]
	Image uiBackground, uiIcon;

    [SerializeField]
    private GameObject[] Lists;

    public bool AlphaColor;

    void Start()
    {
        for(int i = 0; i < Lists.Length; i++)
        {
            if(this.gameObject.name != "Item")
            if(i != Mathf.Abs(int.Parse(this.gameObject.name))%5)
            {
                Lists[i].SetActive(false);
            }
        }
    }

	private readonly Color[] colors = new Color[]
	{
		new Color(1, 1, 1, 0.5f),
		new Color(1f, 1f, 1, 1f),
	};

	public void UpdateItem (int count) 
	{
        int cn = count % 24;
        if (cn < 0)
            cn += 24;
        timeText.text = cn.ToString();
		uiText.text = "Ch" + ((count%5)+1).ToString();
        if(AlphaColor)
		uiBackground.color = colors[0];
        else
        uiBackground.color = colors[1];

        uiIcon.sprite = Resources.Load<Sprite>(((Mathf.Abs(count)) % 30 +1).ToString("icon000"));
	}
}
