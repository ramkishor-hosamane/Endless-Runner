using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public bool goUp;
    public int side=0;
    public AudioSource myAudio;
	public AudioClip coinCollection;
    void Start()
    {
        myAudio = GetComponent<AudioSource> ();
        transform.Rotate(0,0,Random.Range(0.0f,2f));
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp == true) {
			transform.Rotate (0, 0, 0);
			transform.Translate (0,0,0.043f);
		} else {
			transform.Rotate (0, 0, 3.5f);
		}
    }
    void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			goUp = true;
            float x = other.gameObject.transform.position.x;
            //Debug.Log(transform.position.x);
            //Debug.Log(  x);
            if(transform.position.x<x)
                side=1;
            else
            {
                side=-1;
            }


            myAudio.PlayOneShot (coinCollection, 1);
        }
    }
}
