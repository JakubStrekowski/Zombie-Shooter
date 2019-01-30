using UnityEngine;
using System.Collections;
using System;

public class Sztucer : weapon {
    public AudioClip shootsnd;
    public GameObject bullet;
    private float shootspeed = 18f;
    private float shootcooldown = 3.2f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public override float givecooldown()
    {
        return shootcooldown;
    }
    public override void Shoot(Vector2 dir, Vector2 heropos)
    {
        GameObject projectile = (GameObject)Instantiate(bullet, heropos, Quaternion.identity);
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90);
        projectile.transform.rotation = rotation;
        projectile.GetComponent<Rigidbody2D>().velocity = dir * shootspeed;
        hero.GetComponent<AudioSource>().PlayOneShot(shootsnd);
    }
    public override string UpdateName()
    {
        hero = GameObject.Find("Hero").GetComponent<Hero>();
        name = "Sztucer";
        return name;
    }
}
