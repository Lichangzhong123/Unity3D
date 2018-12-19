using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Text gameoverText;
    public Transform player;
    public RectTransform slider;
    public Text hpNumbText;

    public bool gunMode = false;

    [HideInInspector]
    public static bool gameOver = false;

    private bool isWin = false;
    private float playerHp;
    private float filledHp;
    // Use this for initialization
    void Start()
    {
        //gameoverText = transform.FindChild("Text").GetComponent<Text>();
        player = GameObject.Find("Player").transform;
        if (player != null)
        {
            gunMode = player.GetComponent<Player>().gunMode;
            if (gunMode == true)
            {
                filledHp = player.GetComponent<Player>().hp;
                playerHp = filledHp;
                hpNumbText.text = filledHp + "/" + playerHp;
            }
            else
            {
                slider.gameObject.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //print(gunMode);
        if (gameOver == true)
        {
            if (isWin == true)
            {
                print("游戏胜利！");
                gameoverText.text = "游戏胜利";
            }
            else
            {
                print("角色死亡！游戏结束");
                gameoverText.text = "游戏结束";
            }
            //if (gameoverText.enabled == false)
            //{

            gameoverText.enabled = true;

            //}
        }
    }
    void OnPlayerBeAttacked(float damage)
    {
        playerHp -= damage;
        if (playerHp > 0)
        {
            print("角色受伤");
            hpNumbText.text = filledHp + "/" + playerHp;
        }
        else
        {
            print("角色死亡");
            gameOver = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isWin = true;
            gameOver = true;
        }
    }
}
