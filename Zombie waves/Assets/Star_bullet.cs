using UnityEngine;
using System.Collections;

public class Star_bullet : MonoBehaviour {
    private float livingtime = 4f;
    private float timespan;
    // Use this for initialization
    void Start () {
        timespan = Time.time + livingtime;
    }
	
	// Update is called once per frame
	void Update () {
        if (timespan <= Time.time)
        {
            Destroy(gameObject);
        }
    }
}
