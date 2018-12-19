using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private Transform enemy;
    // Use this for initialization
    void Start()
    {
        enemy = transform.parent.Find("Enemy");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && enemy != null)
        {
            if (other.GetComponent<Player>().invincible == true)
            {
                return;
            }

            enemy.GetComponent<EnemyMove>().isAttack = true;
            if (MainCamera.fps == true)
            {
                enemy.GetComponent<EnemyMove>().gunModePos = other.transform;
                return;
            }

            enemy.GetComponent<EnemyMove>().nextHiking = other.transform.position;
            enemy.transform.LookAt(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && enemy != null)
        {
            enemy.GetComponent<EnemyMove>().isAttack = false;
            enemy.GetComponent<EnemyMove>().isMoveing = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
