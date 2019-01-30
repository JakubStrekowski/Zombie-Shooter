using UnityEngine;
using System.Collections;
using System;

public class Flame_bullet : Pocisk {
    private ParticleSystem partsys;
    private ParticleSystem partsys_smoke;
    private float livingtime = 3f;
    private float timespan;
    private float attackcooldown=0.3f;
    // Use this for initialization
    void Start () {
        partsys = transform.Find("Flame_part").gameObject.GetComponent<ParticleSystem>();
        partsys_smoke = transform.Find("Smoke_part").gameObject.GetComponent<ParticleSystem>();
        hero = GameObject.Find("Hero").GetComponent<Hero>();
        timespan = Time.time + livingtime + (hero.giveMarksmanship() * 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
        if (timespan <= Time.time)
        {

            StartCoroutine(WaitASec());
            
        }
    }
    public override float dealdmg()
    {
            return 10 + hero.giveMarksmanship()*2;
    }
    public float giveflamecooldown()
    {
        return attackcooldown;
    }
    IEnumerator WaitASec()
    {
        StartCoroutine(FadeImage(true));
        yield return new WaitForSeconds(1.8f);
        
        Destroy(gameObject);
    }
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * 5)
            {
                // set color with i as alpha
                partsys.startColor = new Color(1f, 1f, 1f, i);
                partsys_smoke.startColor = new Color(1f, 1f, 1f, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime * 5)
            {
                // set color with i as alpha
                partsys.startColor = new Color(1f, 1f, 1f, i);
                partsys_smoke.startColor = new Color(1f, 1f, 1f, i);
                yield return null;
            }
        }
    }
}
