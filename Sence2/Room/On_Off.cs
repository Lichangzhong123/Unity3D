using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_Off : MonoBehaviour {

    public float initTime = 8f;
    public Material on;
    public Material off;

    private Transform door;
    // Use this for initialization
    void Start () {
        door = transform.parent.Find("Door");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void InitOn_Off()
    {
        transform.GetComponent<Renderer>().material = off;
        door.GetComponent<Door>().isPlay = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (door == null)
        {
            print("错误！门不存在");
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            if (door.GetComponent<Door>().isPropDoor == true)
            {
                string s = door.gameObject.tag;
                if (other.gameObject.GetComponent<Player>().propD[s] == null)//没有得到开门道具
                {
                    print("你没有得到开门道具,开门失败");
                    return;
                }
                else
                {
                    other.gameObject.GetComponent<Player>().propD[s] = null;
                    transform.GetComponent<Renderer>().material = on;
                    door.GetComponent<Door>().isPlay = true;
                }
            }
            else
            {
                transform.GetComponent<Renderer>().material = on;
                door.GetComponent<Door>().isPlay = true;
                Invoke("InitOn_Off", initTime);
            }
        }
    }
}
