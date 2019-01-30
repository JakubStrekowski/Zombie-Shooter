using UnityEngine;
using System.Collections;

public class Strzelba : weapon {
    public AudioClip shootsnd;
    public GameObject bullet;
    private float shootspeed = 8.5f;
    private float shootcooldown = 1.5f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    override public void Shoot(Vector2 dir, Vector2 heropos)
    {
        for (int i = 1; i <= 7; i++)
        {
            GameObject projectile = (GameObject)Instantiate(bullet, heropos, Quaternion.identity);
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90);
            projectile.transform.rotation = rotation;
            dir.x += Random.Range(-0.12f, 0.12f);
            dir.y += Random.Range(-0.12f, 0.12f);
            projectile.GetComponent<Rigidbody2D>().velocity = dir * shootspeed;
        }
        hero.GetComponent<AudioSource>().volume = 0.9f;
        hero.GetComponent<AudioSource>().PlayOneShot(shootsnd);
    }
    override public float givecooldown() {
        return shootcooldown;
    }
    override public string UpdateName()
    {
        hero = GameObject.Find("Hero").GetComponent<Hero>();
        return "Shotgun";
    }
}
