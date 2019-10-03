using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class UniRxMyTest : MonoBehaviour
{
    [SerializeField]
    private Button TestButton;

    [SerializeField]
    private Text TestText;

    int count = 0;
    bool stateCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        var stream = this.UpdateAsObservable()
            .Where(a => Input.GetMouseButton(0));

        var otherStream = stream.Throttle(TimeSpan.FromMilliseconds(200f));

        stream.Buffer(otherStream)
            .Where(x => x.Count >= 2)
            .Subscribe(a =>
            {
                Debug.Log("Test");
                Debug.Log(a.Count);
            });

        this.UpdateAsObservable()
            .Select(a=>stateCheck)
            .DistinctUntilChanged()
            .Where(o=>o == true)
            .Subscribe(o=>{
                Debug.Log(o);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stateCheck = !stateCheck;
        }
    }
}
