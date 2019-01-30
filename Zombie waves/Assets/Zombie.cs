using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {
    public AudioClip ugh;
    public Blood blood;
    private Wave_Handler Wave_handler;
    public Hero hero;
    public Pocisk blt;
    public float hp=100;
    public AudioClip dead;
    public AudioClip gothitsnd;
    private Vector2 playerpos;
    private float playerdist;
    public GameObject target;
    public float speed = 0.3f;
    public float rotatespeed = 5f;
    private bool IsDying = false;
    public int ExpValue = 10;
    Vector2 pos;
    private bool canbite = true;
    private float firetimestamp=0;
	// Use this for initialization
	void Start () {
        
        Wave_handler = GameObject.Find("Wave_Handler").GetComponent<Wave_Handler>();
        hero = GameObject.Find("Hero").GetComponent<Hero>();
        Color original = GetComponent<SpriteRenderer>().color;
        original.g += Random.Range(-0.1f, 0.1f);
        GetComponent<SpriteRenderer>().color = original;
        target = GameObject.Find("Hero");
	}
	
	// Update is called once per frame
	void Update () {
        pos= transform.position;
        playerpos = target.transform.position;
       Vector2 distancevect = pos - playerpos;
        playerdist = distancevect.magnitude;
        if (playerdist < 20)
        {
            
            transform.position = Vector3.MoveTowards(pos, playerpos,speed*Time.deltaTime);
            var dir = playerpos - pos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);
        }
        if (hp <= 0)
        {
            if (!IsDying)
            {
                canbite = false;
                Wave_handler.ZombieGotKilled();
                hero.givemeexp(ExpValue);
                GetComponent<AudioSource>().PlayOneShot(dead);
                IsDying = true;
            }
            StartCoroutine(WaitASec());
            
        }
	}
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.collider.tag == "Hurtful")
        {
            
            pos = transform.position;
            playerpos = target.transform.position;
            Vector2 distancevect = pos - playerpos;
            playerdist = distancevect.magnitude;
            if (playerdist > 10)
            {
                if (playerdist > 20)
                {
                    GetComponent<AudioSource>().volume = 0f;
                }
                GetComponent<AudioSource>().volume = 0.4f;
            }
            else
                GetComponent<AudioSource>().volume = 0.7f;
            GetComponent<AudioSource>().PlayOneShot(gothitsnd);
            for(int i = 0; i < Random.Range(1, 4); i++)
            {
                Instantiate(blood,transform.position,Quaternion.identity);
            }

            hp += -col.gameObject.GetComponent<Pocisk>().dealdmg() ;
            
            
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Hurtful")
        {

            pos = transform.position;
            playerpos = target.transform.position;
            Vector2 distancevect = pos - playerpos;
            playerdist = distancevect.magnitude;
            if (playerdist > 10)
            {
                if (playerdist > 20)
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
            GetComponent<Rigidbody2D>().AddForce(col.transform.position*25);
            hp += -col.gameObject.GetComponent<Pocisk>().dealdmg();


        }
        if (col.tag == "StarShrap")
        {

            pos = transform.position;
            playerpos = target.transform.position;
            Vector2 distancevect = pos - playerpos;
            playerdist = distancevect.magnitude;
            if (playerdist > 10)
            {
                if (playerdist > 20)
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
    void OnTriggerStay2D(Collider2D col)
    {
        
        if (col.tag == "Fire"&&firetimestamp<=Time.time )
        {

            Flame_bullet fb = col.gameObject.GetComponent<Flame_bullet>();
            
            hp += -(fb.dealdmg());
            firetimestamp = Time.time + fb.giveflamecooldown();
            pos = transform.position;
            playerpos = target.transform.position;
            Vector2 distancevect = pos - playerpos;
            playerdist = distancevect.magnitude;
            if (!IsDying&&hp>0)
            {
                if (playerdist > 10)
                {
                    if (playerdist > 20)
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
    IEnumerator WaitASec()
    {
        
        speed = 0;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    public void TurnBackNormalMass()
    {
        GetComponent<Rigidbody2D>().mass = 1;
    }
    public void TurnBackNormalSpeed()
    {
        speed = 2;
    }
    public void TurnBackNormalHealth()
    {
        hp = 100;
    }
    public bool isBaddieAlive()
    {
        return canbite;
    }
}
