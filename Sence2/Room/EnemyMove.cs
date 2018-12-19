using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float attackSpeed = 1.0f;
    public Transform bulletGoal;
    public Transform hikingRoutes;
    [HideInInspector]
    public bool isAttack = false;//是否在攻击模式
    [HideInInspector]
    public bool isMoveing = false;//是否在移动中
    [HideInInspector]
    public Vector3 nextHiking;//移动的下一坐标
    [HideInInspector]
    public Transform gunModePos;//枪模式下获取玩家坐标
    [HideInInspector]
    public bool isAttacking = false;//是否为攻击中
    private Transform uziGun;
    private Vector3 muzzle;
    private List<Vector3> hikingPosList;
    private bool gunMode;//枪模式；
    private float attackTime = 0f;

    // Use this for initialization
    void Start()
    {
        if (hikingRoutes == null)
        {
            hikingRoutes = GameObject.Find("HikingRoutes").transform;
        }
        hikingPosList = new List<Vector3>();
        for (int i = 0; i < hikingRoutes.childCount; i++)
        {
            Vector3 v3 = hikingRoutes.GetChild(i).position;
            v3.y = transform.position.y;
            hikingPosList.Add(v3);
        }
        nextHiking = transform.position;
        gunMode = MainCamera.fps;
        uziGun = transform.Find("UziGun");

    }
    // Update is called once per frame
    void Update()
    {
        if (Game.gameOver == true)
        {
            return;
        }
        if (isMoveing == false && isAttack == false)
        {
            InitHiking(true);
        }
        if (isAttack == true && gunMode == true)
        {
            if (isAttacking == false)
            {
                transform.LookAt(gunModePos);

                attackTime += attackSpeed * Time.deltaTime;
                if (attackTime >= 4f)
                {
                    uziGun.GetComponent<UziGun>().bulletGoal = bulletGoal.position;
                    uziGun.GetComponent<UziGun>().isAttack = true;
                    attackTime = 0f;
                    InitHiking(false);
                    isAttacking = true;
                }

                transform.position = Vector3.MoveTowards(transform.position, nextHiking, moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextHiking, moveSpeed * Time.deltaTime);
        }
        if (transform.position == nextHiking)
        {
            isMoveing = false;
        }
    }
    void InitHiking(bool isLook)
    {
        int rand = Random.Range(0, hikingPosList.Count);
        nextHiking = hikingPosList[rand];
        if (isLook)
            transform.LookAt(nextHiking);

        isMoveing = true;
    }
}
