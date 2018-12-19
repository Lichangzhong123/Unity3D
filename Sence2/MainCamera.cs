using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float smooth = 0.3f;
    public float distance = 4f;
    public float height = 2f;
    public static bool fps = false;

    private float yVelocity = 0.0f;
    private Transform player;
    private Vector3 offset;
    private Transform lookPos;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").transform;
        //if (fps == true)
        //{
        //    transform.position = new Vector3(4, 1.7f, 3.8f);
        //    transform.eulerAngles = new Vector3(19.8f, 53.5f, 0f);
        //    distance = transform.position.z - player.position.z;
        //    height = transform.position.y - player.position.y;
        //    lookPos = player.Find("CameraLookPos");
        //}
         BehidePlayer();
    }
    void BehidePlayer()
    {
        if (fps==true)
        {
            transform.position = new Vector3(4, 1.7f, 3.8f);
            transform.eulerAngles = new Vector3(19.8f, 53.5f, 0f);
            distance = transform.position.z - player.position.z;
            height = transform.position.y - player.position.y;
            lookPos = player.Find("CameraLookPos");
        }
        else
        {
            if (player == null)
            {
                return;
            }
            transform.position = player.position;
            transform.rotation = player.rotation;
            transform.Translate(0, height, -distance, Space.Self);
            transform.LookAt(player);

            offset = transform.position - player.position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        //transform.position = player.position + offset;

        float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, player.eulerAngles.y, ref yVelocity, smooth);
        Vector3 v3 = player.position;
        v3 += Quaternion.Euler(0, yAngle, 0) * new Vector3(0, 0, -distance);
        v3.y += height;
        transform.position = v3;
        if (fps == true)
        {
            transform.LookAt(lookPos);
        }
        else
        {
            transform.LookAt(player);
        }
    }
}
