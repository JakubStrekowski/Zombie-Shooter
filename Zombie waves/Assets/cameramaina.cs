using UnityEngine;
using System.Collections;

public class cameramaina : MonoBehaviour {
    public GameObject target;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mousepos= Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 heropos = target.transform.position;
       
        transform.position=(new Vector3((mousepos.x + 2*heropos.x) / 3, (mousepos.y + 2*heropos.y) / 3, -10));
    }
}
