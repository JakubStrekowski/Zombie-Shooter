using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {
    public AudioClip ugh;
    public Wave_Handler waveh;
    protected float hp=100;
    protected Hero hero;
    public AudioClip dead; //to override
    public AudioClip gothitsnd;//to override
    private GameObject blood;
    private Vector3 playerpos;
    private Vector3 playerdist;
    public int ExpValue = 50;
    public float speed = 0.3f;
    public float rotatespeed = 5f;
    public float minimumdistanceseen = 20;
    public bool walkingtowards = true;
    bool IsDying = false;
    private GameObject target;
    private float firetimestamp = 0;
    // Use this for initialization
    protected void Start () {
        waveh = GameObject.Find("Wave_Handler").GetComponent<Wave_Handler>() ;
        blood = (GameObject)Resources.Load("blood");
        hero = GameObject.Find("Hero").GetComponent<Hero>();
        target = GameObject.Find("Hero");
    }
	
	// Update is called once per frame
	protected void Update () {
        playerpos = GameObject.Find("Hero").transform.position;
        playerdist = playerpos - transform.position;
        if (playerdist.magnitude < minimumdistanceseen){
            var dir = playerpos - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            if (walkingtowards)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerpos, speed * Time.deltaTime);
            }
        }
        if (hp <= 0)
        {
            if (!IsDying)
            {
                
                hero.givemeexp(ExpValue);
                GetComponent<AudioSource>().PlayOneShot(dead);
                IsDying = true;
            }
            StartCoroutine(WaitASec());

        }
    }
    void OnTriggerStay2D(Collider2D col)
    {

        if (col.tag == "Fire" && firetimestamp <= Time.time)
        {

            Flame_bullet fb = col.gameObject.GetComponent<Flame_bullet>();

            hp += -(fb.dealdmg());
            firetimestamp = Time.time + fb.giveflamecooldown();
            Vector2 pos = transform.position;
            playerpos = target.transform.position;
            Vector2 distancevect = pos - (Vector2)playerpos;
            
            if (!IsDying && hp > 0)
            {
                if (distancevect.magnitude > 10)
                {
                    if (distancevect.magnitude > 20)
                    {
                        GetComponent<AudioSource>().volume = 0f;
                    }
                    GetComponent<AudioSource>().volume = 0.1f;
                }
                else
                    GetComponent<AudioSource>().volume = 0.25f;
                GetComponent<AudioSource>().PlayOneShot(ugh);
            }

        }
    }
    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Hurtful")
        {
            if (col.tag == "Hurtful")
            {

                var pos = transform.position;
                playerpos = target.transform.position;
                playerdist = pos - playerpos;

                if (playerdist.magnitude > 10)
                {
                    if (playerdist.magnitude > 20)
                    {
                        GetComponent<AudioSource>().volume = 0f;
                    }
                    GetComponent<AudioSource>().volume = 0.4f;
                }
                else
                    GetComponent<AudioSource>().volume = 0.7f;
                GetComponent<AudioSource>().PlayOneShot(gothitsnd);
                for (int i = 0; i < Random.Range(1, 4); i++)
                {
                    Instantiate(blood, transform.position, Quaternion.identity);
                }
                GetComponent<Rigidbody2D>().AddForce(col.transform.position * 25);
                hp += -col.gameObject.GetComponent<Pocisk>().dealdmg();
            }
            if (col.tag == "StarShrap")
            {

                var pos = transform.position;
                playerpos = target.transform.position;
                playerdist = pos - playerpos;
                if (playerdist.magnitude > 10)
                {
                    if (playerdist.magnitude > 20)
                    {
                        GetComponent<AudioSource>().volume = 0f;
                    }
                    GetComponent<AudioSource>().volume = 0.4f;
                }
                else
                    GetComponent<AudioSource>().volume = 0.7f;
                GetComponent<AudioSource>().PlayOneShot(gothitsnd);
                for (int i = 0; i < Random.Range(10, 15); i++)
                {
                    Instantiate(blood, transform.position, Quaternion.identity);

                }

                hp += -200;
            }
        }
    }
    protected void OnCollisionEnter2d(Collision2D col)
    {
        if (col.collider.tag == "Hurtful") {
            
            var pos = transform.position;
            playerpos = target.transform.position;
            
            playerdist = pos - playerpos;
            if (playerdist.magnitude > 10)
            {
                if (playerdist.magnitude > 20)
                {
                    GetComponent<AudioSource>().volume = 0f;
                }
                GetComponent<AudioSource>().volume = 0.4f;
            }
            else
                GetComponent<AudioSource>().volume = 0.7f;
            GetComponent<AudioSource>().PlayOneShot(gothitsnd);
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                Instantiate(blood, transform.position, Quaternion.identity);
            }

            hp += -col.gameObject.GetComponent<Pocisk>().dealdmg();
        }
    }
    IEnumerator WaitASec()
    {
        
        speed = 0;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    public bool isBaddieAlive()
    {
        return !IsDying;
    }
}
