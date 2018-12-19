using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCollider : MonoBehaviour
{

    private Transform plane;
    private Transform enemy;
    // Use this for initialization
    void Start()
    {
        plane = transform.parent.Find("Plane");
        enemy = transform.parent.Find("Enemy");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("进入草丛");
            plane.GetComponent<Collider>().enabled = false;
            enemy.GetComponent<EnemyMove>().isAttack = false;
            enemy.GetComponent<EnemyMove>().isMoveing = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (plane.GetComponent<Collider>().enabled != false)
                plane.GetComponent<Collider>().enabled = false;
            if (enemy.GetComponent<EnemyMove>().isAttack != false && enemy.GetComponent<EnemyMove>().isMoveing != false)
            {
                print("隐蔽中...");
                enemy.GetComponent<EnemyMove>().isAttack = false;
                enemy.GetComponent<EnemyMove>().isMoveing = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("你曝光了");
            plane.GetComponent<Collider>().enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
