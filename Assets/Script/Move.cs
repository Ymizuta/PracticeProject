using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    [SerializeField] float speed_ = 10;
    private Vector3 current_poz;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        this.gameObject.transform.position += transform.forward * speed_ * Time.deltaTime;
        //プレイヤーが宙に浮かないようにy座標修正
        if (transform.position.y > 0.5)
        {
            current_poz = transform.position;
            current_poz.y = 0.5f;
            transform.position = current_poz;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("right");
            transform.eulerAngles = new Vector3(0,transform. ,0);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("left");
            transform.rotation = Quaternion.AngleAxis(transform.rotation.y - 90, Vector3.up);
        }
    }



}
