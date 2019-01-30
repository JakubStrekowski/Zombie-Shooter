using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float rand = Random.Range(0.1f, 0.3f);
        transform.localScale = new Vector3(rand, rand, rand);
        GetComponent<Rigidbody2D>().AddForce( new Vector3(Random.Range(-100f, 100f)*Time.deltaTime, Random.Range(-100f, 100f) * Time.deltaTime,0),ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
