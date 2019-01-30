using UnityEngine;
using System.Collections;

public class Necromancer : Enemy {
    public AudioClip ugh1;
    public AudioClip dead1;
    public AudioClip gothitsnd1;
    private float timestampSkeleSpawn;
    private float SkeleCooldown = 3f;
    public GameObject Skeleton;
    public AudioClip summonsnd;
    private bool deadie = false;
	// Use this for initialization
	void Start () {
        base.Start();
        ugh = ugh1;
        timestampSkeleSpawn = Time.time;
        ExpValue = 500;
        hp = 350;
        walkingtowards = false;
        dead = dead1;
        gothitsnd = gothitsnd1;
	}
	
	// Update is called once per frame
	void Update () {
        if (hp <= 0&&deadie==false)
        {
            deadie = true;
            waveh.ZombieGotKilled();
        }
        base.Update();
        if (timestampSkeleSpawn <= Time.time)
        {
            timestampSkeleSpawn = Time.time + SkeleCooldown+Random.Range(-1f,1f);
            float distance = 0f;
            Vector3 nowy = transform.position;
            while (distance < 1)
            {
                nowy.x += Random.Range(-3f, 3f);
                nowy.y += Random.Range(-3f, 3f);
                distance = (nowy - transform.position).magnitude;
                
            }
            GetComponent<AudioSource>().PlayOneShot(summonsnd,0.5f);
            Instantiate(Skeleton, nowy, Quaternion.identity);
        }
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2d(col);
    }
    public void UpgradePerLevel(int level)
    {
        hp = 300 + (level * 10);
        SkeleCooldown = 3.5f - (level / 10f);
        if (SkeleCooldown < 1.01f) { SkeleCooldown = 1.01f; }
    }
}
