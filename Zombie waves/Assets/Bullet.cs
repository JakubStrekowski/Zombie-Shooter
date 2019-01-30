using UnityEngine;
using System.Collections;

public class Bullet : Pocisk {
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
    override public float dealdmg()
    {
        return 25+ hero.giveMarksmanship()*10;
    }
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime*2)
            {
                // set color with i as alpha
                GetComponent<SpriteRenderer>().color = new Color(0.07f, 0.07f, 0.07f, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime*2)
            {
                // set color with i as alpha
                GetComponent<SpriteRenderer>().color = new Color(0.07f, 0.07f, 0.07f, i);
                yield return null;
            }
        }
    }
    
}
