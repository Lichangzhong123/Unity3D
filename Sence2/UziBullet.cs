using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("DestroyMySelf", 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void DestroyMySelf()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grass")
        {
            return;
        }
        DestroyMySelf();
    }
}
