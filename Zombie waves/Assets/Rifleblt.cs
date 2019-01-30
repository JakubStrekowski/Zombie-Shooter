using UnityEngine;
using System.Collections;

public class Rifleblt : Pocisk {
    private float livingtime = 3f;
    float timespan;
    private int counttouched = 0;
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
        if (counttouched>4)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggernEnter2D(Collider2D col)
    {
        if (col.tag != "Hurtful")
        {
            counttouched++;
        }
    }
    public override float dealdmg()
    {
        return 100 + hero.giveMarksmanship() * 25;
    }
}
