using UnityEngine;
using System.Collections;

public class Weapon_Powerup : MonoBehaviour {
    public int weaponID;
    private float lifetimestamp;
    private float lifetime = 15f;
    private Hero hero;
    public AudioClip gotsnd;
    private bool did = false;
	// Use this for initialization
	void Start () {
        lifetimestamp = Time.time + lifetime;
        hero = GameObject.Find("Hero").GetComponent<Hero>();
	}
	
	// Update is called once per frame
	void Update () {
        if (lifetimestamp <= Time.time)
        {
            Destroy(gameObject);
        }
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Hero" && !did)
        {
            did = true;
            GetComponent<AudioSource>().PlayOneShot(gotsnd);
            hero.ChangeWeapon(weaponID);
            StartCoroutine(wait());
        }
    }
    public void SetID(int set)
    {
        weaponID = set;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.3f);
        Destroy(gameObject);
    }
}
