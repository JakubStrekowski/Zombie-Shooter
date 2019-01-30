using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class guihealth : MonoBehaviour {
    
    private Hero heroscript;
    private GameObject hp1;
    private GameObject hp2;
    private GameObject hp3;
    private GameObject hp4;
    private GameObject hp5;
    private GameObject hp6;
    private int curhp;
    // Use this for initialization
    void Start () {
        
        heroscript = GameObject.Find("Hero").GetComponent<Hero>();
        hp1 = GameObject.Find("life_1");
        hp2 = GameObject.Find("life_2");
        hp3 = GameObject.Find("life_3");
        hp4 = GameObject.Find("life_4");
        hp5 = GameObject.Find("life_5");
        hp6 = GameObject.Find("life_6");
        
        hp1.SetActive(false);
        hp2.SetActive(false);
        hp3.SetActive(false);
        hp4.SetActive(false);
        hp5.SetActive(false);
        hp6.SetActive(false);
        
        checkhp();
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    public void lowerhp()
    {
        
        if (hp6.activeInHierarchy == true)
        {
            hp6.SetActive(false);
        }
        else if (hp5.activeInHierarchy == true)
        {
            hp5.SetActive(false);
        }
        else if (hp4.activeInHierarchy == true)
        {
            hp4.SetActive(false);
        }
        else if (hp3.activeInHierarchy == true)
        {
            hp3.SetActive(false);
        }
        else if (hp2.activeInHierarchy == true)
        {
            hp2.SetActive(false);
        }
        else if (hp1.activeInHierarchy == true)
        {
            hp1.SetActive(false);
        }
    }
    public void checkhp()
    {
        curhp = heroscript.giveactivehealth();
        switch (curhp)
        {
            case 1: hp1.SetActive(true); break;
            case 2: hp2.SetActive(true); goto case 1;
            case 3: hp3.SetActive(true); goto case 2;
            case 4: hp4.SetActive(true); goto case 3;
            case 5: hp5.SetActive(true); goto case 4;
            case 6: hp6.SetActive(true); goto case 5;
        }
    }
}
