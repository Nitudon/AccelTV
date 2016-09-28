using UnityEngine;
using System.Collections;
using UniRx;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


    static public IObservable<long> OnInputTouchDownObservable()
    {
        return Observable
            .EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0));
    }

    static public IObservable<long> OnInputTouchUpObservable()
    {
        return Observable
            .EveryUpdate()
            .Where(_ => Input.GetMouseButtonUp(0));
    }

    static public IObservable<Vector3> OnSwipeDistanceObservable()
    {
        return Observable
            .EveryUpdate()
            .Select(_ => Input.mousePosition);
    }

    static public IObservable<double> OnTapObservable()
    {
        return
        OnInputTouchDownObservable()
                .Timestamp()
                .Zip(OnInputTouchUpObservable().Timestamp(), (Down, Up) => (Up.Timestamp - Down.Timestamp).TotalMilliseconds / 1000f)
                .Where(time => time < 0.5f);
    }

    //static public IObservable<long> OnButtonPointerDownObservable(Button button)
    //{
    //    return Observable.EveryUpdate()
    //        .Where(_ => button.OnPointerDown(button.gameObject.GetComponent<>));
    //}

    static public IObservable<long> OnButtonHoldObservable(Button button, GameObject gameobject)
    {
        return button.OnClickAsObservable()
            .SelectMany(_ => Observable.Timer(TimeSpan.FromSeconds(1)))
            .TakeUntil(OnInputTouchUpObservable())
            .RepeatUntilDestroy(gameobject);
    }

    static public IObservable<long> OnHoldObservable(GameObject gameobject)
    {
        return
        OnInputTouchDownObservable()
        .SelectMany(_ => Observable.Timer(TimeSpan.FromSeconds(1)))
        .TakeUntil(OnInputTouchUpObservable())
        .RepeatUntilDestroy(gameobject);
    }

    static public IObservable<System.Collections.Generic.IList<long>> OnDoubleTapObservable()
    {
        return
            OnInputTouchDownObservable()
            .Buffer(OnInputTouchDownObservable().Throttle(TimeSpan.FromSeconds(2f)))
            .Where(x => x.Count >= 2);
    }

    static public IObservable<Vector3> OnSwipeObservable()
    {
        return
            OnSwipeDistanceObservable()
             .Zip(OnSwipeDistanceObservable().Skip(60), (pos1, pos2) => pos2 - pos1)
             .Where(x => Input.GetMouseButtonUp(0) && x.magnitude > 100f);
    }

    static public IObservable<Vector3> OnVerticalSwipeObservable(float border)
    {
        return
            OnSwipeDistanceObservable()
            .Zip(OnSwipeDistanceObservable().Skip(60), (pos1, pos2) => new Vector3(0f,pos2.y-pos2.y,0f))
             .Where(x => Input.GetMouseButtonUp(0) && x.magnitude > border);
    }

    static public IObservable<long> OnRightAccelObservable(Vector3 StartAccel ,float border)
    {
        return Observable.EveryUpdate()
            .Where(_ => StartAccel.x - Input.acceleration.x < -border);

    }

    static public IObservable<long> OnLeftAccelObservable(Vector3 StartAccel, float border)
    {
        return Observable.EveryUpdate()
            .Where(_ => StartAccel.x - Input.acceleration.x > border);

    }

    
    //static public IObservable<long> OnInputMoveObservable()
    //{
    //    return
    //        Observable.FromEvent<>
    //}

    //static public IObservable<long> OnDragObservable()
    //{
    //    return
    //        OnInputTouchDownObservable()
    //        .SelectMany
    //}
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
	
	}
}
