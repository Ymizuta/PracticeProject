using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StopAnimation()
    {

    }

    void SampleEvent()
    {
        StartCoroutine("StopCoroutine");
    }

    IEnumerator StopCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
    }

}
