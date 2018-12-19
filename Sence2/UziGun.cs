using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziGun : MonoBehaviour
{

    public float force = 5000f;//子弹射击的力
    public GameObject bulletPrefab;//子弹预制体
    [HideInInspector]
    public Vector3 bulletGoal;//子弹目标
    [HideInInspector]
    public bool isAttack = false;//是否攻击
    private Transform muzzle;//枪口坐标
    private Color muzzleDefaultColor;

    // Use this for initialization
    private void Awake()
    {
        //动态加载子弹预制体
        bulletPrefab = (GameObject)Resources.Load("Bullet2");
    }
    void Start()
    {
        muzzle = transform.Find("Muzzle");
        if (MainCamera.fps == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        muzzleDefaultColor = muzzle.GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack == true)
        {
            isAttack = false;
            if (transform.parent != null)
            {
                muzzle.GetComponent<MeshRenderer>().material.color = Color.red;
                Invoke("OpenFire", 1f);
            }
            else
            {
                OpenFire();
            }
        }
    }
    void OpenFire()
    {
        //枪战模式，点击鼠标左键发射子弹
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = muzzle.position;
        bullet.transform.eulerAngles = muzzle.eulerAngles;
        //Vector3 v3 = sightBead.position - muzzle.position - new Vector3(0, 0.4f, 0);

        bullet.GetComponent<Rigidbody>().AddForce((bulletGoal - muzzle.position).normalized * force);
        if (transform.parent != null)
        {
            muzzle.GetComponent<MeshRenderer>().material.color = muzzleDefaultColor;
            transform.parent.GetComponent<EnemyMove>().isAttacking = false;
        }
    }
}
