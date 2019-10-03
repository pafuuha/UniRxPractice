using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DelegateStream : MonoBehaviour
{

    public delegate void SomeEvent(string msg);
    public SomeEvent someEvent;



    // Start is called before the first frame update
    void Start()
    {
        Observable.FromEvent<SomeEvent, string>(
            h => msg => h(msg),
            h => someEvent += h,
            h => someEvent -= h
        )
        .Subscribe(x => Debug.Log("OnComplateCallback:" + x));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
