using UnityEngine;
using System.Collections;

public abstract class Pocisk : MonoBehaviour {
    protected Hero hero;
	// Use this for initialization
	void Start () {
        //hero = GameObject.Find("Hero").GetComponent<Hero>(); dac kazdemu dziedziczonemu
    }

    // Update is called once per frame
    void Update () {
	
	}
    public abstract float dealdmg();
    
}
