using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float openSpeed = 5.0f;//开门速度
    public float padlockSpeed = 2.0f;//关门速度
    public bool isPropDoor = false;
    private Vector3 openPos;
    private Vector3 padlockPos;
    public bool isPlay = false;
    private bool isInit = false;

    // Use this for initialization
    void Start()
    {
        Invoke("Synchro", 2f);
        //padlockPos = transform.position;
    }
    void Synchro()
    {
        padlockPos = transform.position;
        isInit = true;
        openPos = transform.parent.Find("OpenPos").position;
    }
    bool IsBigGap(Vector3 goalPos, Vector3 currentPos)
    {
        if (Mathf.Abs(goalPos.x - currentPos.x) >= 0.01
            || Mathf.Abs(goalPos.z - currentPos.z) >= 0.01
            || Mathf.Abs(goalPos.y - currentPos.y) >= 0.01)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlay == true)
        {
            if (IsBigGap(openPos, transform.position) == false)
            {
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, openPos, Time.deltaTime * openSpeed);
        }
        else if (IsBigGap(padlockPos, transform.position) == true && isInit == true && isPropDoor == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, padlockPos, Time.deltaTime * padlockSpeed);
        }
    }
}
