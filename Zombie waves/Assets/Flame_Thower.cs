using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Flame_Thower : weapon {
    public AudioClip shootsnd;
    public GameObject bullet;
    private float shootspeed = 5.75f;
    private float shootcooldown = 0.05f;
    
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    override public void Shoot(Vector2 dir, Vector2 heropos)
    {
        
        GameObject projectile = (GameObject)Instantiate(bullet, heropos, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = dir * shootspeed;
        hero.GetComponent<AudioSource>().volume = 0.6f;
        hero.GetComponent<AudioSource>().PlayOneShot(shootsnd);
        //Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90);
    }
    override public float givecooldown()
    {
        return shootcooldown;
    }
    public override string UpdateName()
    {
        hero = GameObject.Find("Hero").GetComponent<Hero>();
        name = "Miotacz Płomieni";
        return name;
    }
}
