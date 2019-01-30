using UnityEngine;
using System.Collections;

public class Skeleton : Enemy {
    public AudioClip dead1;
    public AudioClip gothitsnd1;
    public AudioClip ugh1;
    // Use this for initialization
    void Start () {
        base.Start();
        ugh = ugh1;
        ExpValue = 5;
        hp = 70;
        walkingtowards = true;
        dead = dead1;
        gothitsnd = gothitsnd1;
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        if (col.tag == "NoSpawn")
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2d(col);
    }
}
