using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                transform.GetChild(i).gameObject.AddComponent<Wall>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
