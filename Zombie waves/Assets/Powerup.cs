using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
    public AudioClip speed;
    public AudioClip dmg;
    public AudioClip star;
    private Hero hero;
    public ParticleSystem partsys;
    public int Powerup_type;//1:speed, 2:Damage, 3:Firestar
                            // Use this for initialization
    private float livingtime = 15f;
    private float timespanlive;
    void Start () {
        timespanlive = Time.time + livingtime;
        hero = GameObject.Find("Hero").GetComponent<Hero>();
        StartCoroutine(FadeImage(false));
	}
	
	// Update is called once per frame
	void Update () {
        if (timespanlive <= Time.time)
        {
            StartCoroutine(WaitASec());
        }
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Hero")
        {
            switch (Powerup_type)
            {
                case 1: GetComponent<AudioSource>().PlayOneShot(speed); break;
                case 2: GetComponent<AudioSource>().PlayOneShot(dmg); break;
                case 3: GetComponent<AudioSource>().PlayOneShot(star); break;
                default:break;
            }
            
            hero.Powerup(Powerup_type,transform.position);
            Powerup_type = 0;
            StartCoroutine(WaitASec());
        }
    }
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1f; i >= 0; i -= Time.deltaTime * 3f)
            {
                // set color with i as alpha
                partsys.startColor= new Color(1f, 1f, 1f, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1f; i += Time.deltaTime * 1f)
            {
                // set color with i as alpha
                partsys.startColor = new Color(1f, 1f, 1f, i);
                yield return null;
            }
        }
    }
    IEnumerator WaitASec()
    {
        StartCoroutine(FadeImage(true));
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
