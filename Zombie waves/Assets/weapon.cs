using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public abstract class weapon : MonoBehaviour {
    protected Hero hero;
    protected Vector2 heropos;
    protected Vector2 dir;
    protected string nazwa;
	// Use this for initialization
	void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public abstract void Shoot(Vector2 dir, Vector2 heropos);//wydaj pocisk w odpowiednim miejscu
    public abstract float givecooldown();
    public abstract string UpdateName();
}
