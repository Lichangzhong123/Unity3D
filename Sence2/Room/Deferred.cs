using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deferred : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Invoke("StartUseScript", 2f);
	}
	void StartUseScript()
    {
        transform.GetComponent<EnemyMove>().enabled = true;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
