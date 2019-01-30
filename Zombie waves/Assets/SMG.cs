using UnityEngine;
using System.Collections;


public class SMG : weapon {
    public GameObject bullet;
    public AudioClip shootsnd;
    private float shootspeed = 9f;
    private float shootcooldown = 0.13f;
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public override string UpdateName()
    {
        name = "SMG";
        return name;
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
        dir.x += Random.Range(-0.05f, 0.05f);
        dir.y += Random.Range(-0.05f, 0.05f);
        projectile.GetComponent<Rigidbody2D>().velocity = dir * shootspeed;
        GetComponent<AudioSource>().volume = 1f;
        GetComponent<AudioSource>().enabled = true;
        AudioSource.PlayClipAtPoint(shootsnd, heropos, 1.2f);
        GetComponent<AudioSource>().volume = 1f;
    }
}
