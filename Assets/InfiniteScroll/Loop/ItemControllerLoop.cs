using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;

[RequireComponent(typeof( InfiniteScroll))]
public class ItemControllerLoop : UIBehaviour, IInfiniteScrollSetup
{

    [SerializeField]
    private bool SideOn;

    [SerializeField]
    private bool VertOn;

    [SerializeField]
    private GameObject TimeBar;

    private bool isSetuped = false;
    private Vector3 StartAccel = new Vector3();

	public void OnPostSetupItems ()
	{
		GetComponentInParent<ScrollRect> ().movementType = ScrollRect.MovementType.Unrestricted;
		isSetuped = true;
	}

	public void OnUpdateItem (int itemCount, GameObject obj)
	{
		if( isSetuped == true ) 
			return;

		var item = obj.GetComponentInChildren<Item> ();
		item.UpdateItem (itemCount);
	}

    void Start()
    {
        StartAccel = GameObject.Find("ChannelList").GetComponent<ChannelManager>().StartAccel;
    }

    void Update()
    {
        
        if (VertOn)
        {
            if (StartAccel.y - Input.acceleration.y < -0.12f && transform.position.y < 2632f)
            {
                transform.position += new Vector3(0f, 300f, 0f) * Time.deltaTime;
                TimeBar.transform.position += new Vector3(0f, 300f, 0f) * Time.deltaTime;
            }

            if (StartAccel.y - Input.acceleration.y > 0.12f && transform.position.y > -2622f)
            {
                transform.position -= new Vector3(0f, 300f, 0f) * Time.deltaTime;
                TimeBar.transform.position -= new Vector3(0f, 300f, 0f) * Time.deltaTime;
            }

            if (StartAccel.y - Input.acceleration.y < -0.1f && transform.position.y < 2632f)
            {
                transform.position += new Vector3(0f, 300f, 0f) * Time.deltaTime;
                TimeBar.transform.position += new Vector3(0f, 300f, 0f) * Time.deltaTime;
            }

            if (StartAccel.y - Input.acceleration.y > 0.1f && transform.position.y > -2622f)
            {
                transform.position -= new Vector3(0f, 300f, 0f) * Time.deltaTime;
                TimeBar.transform.position -= new Vector3(0f, 300f, 0f) * Time.deltaTime;
            }
        }
        if (SideOn)
        {
            if (StartAccel.x - Input.acceleration.x < -0.2f)
            {
                transform.position += new Vector3(400f, 0f, 0f) * Time.deltaTime;
            }

            if (StartAccel.x - Input.acceleration.x > 0.2f)
            {
                transform.position -= new Vector3(400f, 0f, 0f) * Time.deltaTime;
            }

            if (StartAccel.x - Input.acceleration.x < -0.4f)
            {
                transform.position += new Vector3(400f, 0f, 0f) * Time.deltaTime;
            }

            if (StartAccel.x - Input.acceleration.x > 0.4f)
            {
                transform.position -= new Vector3(400f, 0f, 0f) * Time.deltaTime;
            }
        }
    }
}
