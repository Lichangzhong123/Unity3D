using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotSpeed = 45f;
    public float jumpHeight = 1f;
    public float hp;

    public bool invincible = false;//是否无敌状态
    public bool gunMode = false;//枪战模式

    public GameObject uziGunPrefab;//枪预制体
    public Transform sightBead;//准星

    public Dictionary<string, GameObject> propD = new Dictionary<string, GameObject>();

    [HideInInspector]
    public bool isWall = false;//是否碰撞到墙

    private Rigidbody rgb;
    private Transform gunPos;//玩家枪的坐标
    private Transform uziGun;//枪
    private Transform muzzle;//枪口
    private bool isGround = true;


    //public float speed = 3.0F;
    //public float rotateSpeed = 3.0F;
    //// Use this for initialization
    private void Awake()
    {
        if (gunMode == true)
        {
            MainCamera.fps = true;
        }
    }
    void Start()
    {
        rgb = GetComponent<Rigidbody>();
        if (gunMode == true)
        {
            gunPos = transform.Find("GunPos");
            uziGun = Instantiate(uziGunPrefab).transform;
            sightBead.gameObject.SetActive(true);
            muzzle = uziGun.Find("Muzzle");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.gameOver == true)
        {
            return;
        }
        if (isWall == true)
        {
            print("撞到南墙了");
            return;
        }

        float xDeg = Input.GetAxis("Horizontal");
        float yDeg = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(xDeg, 0, yDeg) * moveSpeed * Time.deltaTime, Space.Self);
        transform.eulerAngles += new Vector3(0, xDeg * rotSpeed * Time.deltaTime, 0);

        if (uziGun != null && gunPos != null)
        {
            uziGun.transform.position = gunPos.position;
            // uziGun.eulerAngles = gunPos.eulerAngles;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10);

            RaycastHit hit;
            int layerNum = LayerMask.NameToLayer("SightBeadPlane");
            int layerMask = 1 << layerNum;

            //Vector3 v3 = ray.GetPoint(10f) - muzzle.position;
            uziGun.GetComponent<UziGun>().bulletGoal = ray.GetPoint(10f);//传入射击目标点
            if (Input.GetMouseButtonDown(0))
            {
                //按键发射子弹
                uziGun.GetComponent<UziGun>().isAttack = true;
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                sightBead.position = hit.point;
                sightBead.LookAt(Camera.main.transform.position);
                //transform.rotation = Quaternion.LookRotation(hit.point);

                uziGun.LookAt(ray.GetPoint(10f));
                // Camera.main.transform.LookAt(sightBead);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && isGround == true)
        {
            transform.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight * 200f);
            isGround = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        isGround = true;
        if (invincible == true)
        {
            return;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Game.gameOver = true;
        }
    }
}
