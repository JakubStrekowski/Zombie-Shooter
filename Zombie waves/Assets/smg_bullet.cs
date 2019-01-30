﻿using UnityEngine;
using System.Collections;
using System;

public class smg_bullet : Pocisk {
    private float livingtime = 4f;
    float timespan;
    private bool touched = false;
    // Use this for initialization
    void Start () {
        timespan = Time.time + livingtime;
        hero = GameObject.Find("Hero").GetComponent<Hero>();
    }
	
	// Update is called once per frame
	void Update () {
        if (timespan <= Time.time)
        {
            Destroy(gameObject);
        }
        if (touched)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag != "Hurtful")
        {
            touched = true;
        }
    }
    public override float dealdmg()
    {
        return 10 + hero.giveMarksmanship() * 3;
    }
}
