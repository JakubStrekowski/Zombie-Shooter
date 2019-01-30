using UnityEngine;
using System.Collections;


public class Spawner : MonoBehaviour {
    public GameObject WhatToSpawn;
    public GameObject player;
    private float timespawnstamp;
    private float spawncooldown = 7f;
    public int spawnlimit = 3;
    private float mindistance = 4f;
    private float Poweruptimestamp;
    private float Powerupcooldown = 30f;
    private int ChanceofSpawn;
	// Use this for initialization
	void Start () {
        timespawnstamp = Time.time;
        Poweruptimestamp = Time.time+ Powerupcooldown;
        ChanceofSpawn = Random.Range(0, 1);
    }
	
	// Update is called once per frame
	void Update () {

        if (Poweruptimestamp <= Time.time && ChanceofSpawn == 1)
        {
            Poweruptimestamp = Time.time + Powerupcooldown;

        }
        if (timespawnstamp <= Time.time)
        {
            float x;
            float y;
            x = Random.Range(-10f, 10f);
            y = Random.Range(-10f, 10f);
            Vector3 nowy = new Vector3(x, y);
            if ((player.transform.position - nowy).magnitude > mindistance) {
                Instantiate(WhatToSpawn, nowy, Quaternion.identity);
                timespawnstamp = Time.time + spawncooldown;
            }
         }
	}
}
