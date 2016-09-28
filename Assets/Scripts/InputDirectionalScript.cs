using UnityEngine;
using System.Collections;

public class InputDirectionalScript : MonoBehaviour {

    private float minSwipeDistX,swipeDistX, SignValueX;
    private float minSwipeDistY,swipeDistY, SignValueY;
    private Vector3 startPos,endPos;

    public void OnRightSwipe (){ Debug.Log("RightSwipe"); }
    public void OnLeftSwipe() { Debug.Log("LeftSwipe"); }

    void Start()
    {
        minSwipeDistX = 30;
        minSwipeDistY = 30;
    }


    void Update()
    {

        if (Input.touchCount > 0)
        {

            //タッチを取得
            Touch touch = Input.touches[0];
            //タッチフェーズによって場合分け
            switch (touch.phase)
            {
                //タッチ開始時
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;

                //タッチ終了時
                case TouchPhase.Ended:
                    endPos = new Vector2(touch.position.x, touch.position.y);
                    swipeDistX = (new Vector3(endPos.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
                    swipeDistY = (new Vector3(0, endPos.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

                    Debug.Log("X" + swipeDistX.ToString());
                    Debug.Log("Y" + swipeDistY.ToString());

                    if (swipeDistX > swipeDistY && swipeDistX > minSwipeDistX)
                    {
                        SignValueX = Mathf.Sign(endPos.x - startPos.x);
                        if (SignValueX > 0)
                        {
                            OnRightSwipe();
                        }
                        else if (SignValueX < 0)
                        {
                            OnLeftSwipe();
                        }
                    }
                    else if (swipeDistY > minSwipeDistY)
                    {
                        SignValueY = Mathf.Sign(endPos.y - startPos.y);
                        if (SignValueY > 0)
                        {
                            //上方向にスワイプしたとき

                        }
                        else if (SignValueY < 0)
                        {
                            //下方向にスワイプしたとき
                        }
                    }
                    if (swipeDistX < minSwipeDistX && swipeDistY < minSwipeDistY)
                    {
                        // タップした時
                    }
                    break;
            }
        }
    }

}
