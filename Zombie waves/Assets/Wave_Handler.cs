using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wave_Handler : MonoBehaviour {
    public Text Killed_amnt;
    private int TotalZombieKilled = 0;
    public Zombie WhatToSpawn;
    public Necromancer Necro;
    private GameObject Hero;
    public Text Wavetxt;
    public Slider Progress;
    public Text guiwave;
    private int CurrentWave=0;
    private int ZombiesSpawned = 0;
    private int ZombiesKilledInWave;
    private int AmmountOfZombiesInThisWave = 5;
    private float zombiespawncooldown;
    private int zombiehealth;
    private bool startdissapearing = true;
    private float appearingoftxtstmp;
    public float texthowlong = 1.2f;
    private bool allowtospawn = false;
    private bool allowtoprepare = true;
    private float ZombieSpawnTimeStamp;
    private bool Wave_Preparation = false;
    //Powerupy
    public GameObject Powerup_Speed;
    public GameObject Powerup_Dmg;
    public GameObject Powerup_Star;
    private float poweruptimestamp;
    private float powerupcooldown=17f;
    private int powerrandomchance;
    //Bronie
    public GameObject Weapon_Powerup;
    private float Weapontimestamp;
    private float Weaponcooldown = 17f;
    private int Weaponrandomchance;
    // Use this for initialization
    void Start () {
        Weapontimestamp = Time.time + Weaponcooldown;
        Weaponrandomchance = Random.Range(1, 7);
        poweruptimestamp = Time.time + powerupcooldown;
        powerrandomchance = Random.Range(1, 5);
        Killed_amnt.text = TotalZombieKilled.ToString();
        Hero=GameObject.Find("Hero");
        ZombieSpawnTimeStamp = Time.time;
    }
	    
	// Update is called once per frame
	void Update () {
        if (Weaponrandomchance > 1 && Weapontimestamp <= Time.time)
        {
            float x;
            float y;
            Weapontimestamp = Time.time + Weaponcooldown;
            switch (Weaponrandomchance)
            {
                case 2:
                    x = Random.Range(-13f, 13f);
                    y = Random.Range(-13f, 13f);
                    Vector3 nowy = new Vector3(x, y);
                    Weapon_Powerup.GetComponent<Weapon_Powerup>().SetID(1);
                    Instantiate(Weapon_Powerup, nowy, Quaternion.identity);
                    Weaponrandomchance = Random.Range(1, 7);
                    break;
                case 3:
                    x = Random.Range(-13f, 13f);
                    y = Random.Range(-13f, 13f);
                    Vector3 nowy1 = new Vector3(x, y);
                    Weapon_Powerup.GetComponent<Weapon_Powerup>().SetID(2);
                    Instantiate(Weapon_Powerup, nowy1, Quaternion.identity);
                    Weaponrandomchance = Random.Range(1, 7);
                    break;
                case 4:
                    x = Random.Range(-13f, 13f);
                    y = Random.Range(-13f, 13f);
                    Vector3 nowy2 = new Vector3(x, y);
                    Weapon_Powerup.GetComponent<Weapon_Powerup>().SetID(3);
                    Instantiate(Weapon_Powerup, nowy2, Quaternion.identity);
                    Weaponrandomchance = Random.Range(1, 7);
                    break;
                case 5:
                    x = Random.Range(-13f, 13f);
                    y = Random.Range(-13f, 13f);
                    Vector3 nowy3 = new Vector3(x, y);
                    Weapon_Powerup.GetComponent<Weapon_Powerup>().SetID(4);
                    Instantiate(Weapon_Powerup, nowy3, Quaternion.identity);
                    Weaponrandomchance = Random.Range(1, 7);
                    break;
                case 6:
                    x = Random.Range(-13f, 13f);
                    y = Random.Range(-13f, 13f);
                    Vector3 nowy4 = new Vector3(x, y);
                    Weapon_Powerup.GetComponent<Weapon_Powerup>().SetID(5);
                    Instantiate(Weapon_Powerup, nowy4, Quaternion.identity);
                    Weaponrandomchance = Random.Range(1, 7);
                    break;
            }
        }
        else
        {
            if(Weapontimestamp <= Time.time)
            {
                Weaponrandomchance = Random.Range(1, 7);
                Weapontimestamp = Time.time + Weaponcooldown;
            }
        }
        //spawn zombie
        if (powerrandomchance > 1 && poweruptimestamp <= Time.time)
        {
            float x;
            float y;
            
            poweruptimestamp = Time.time + powerupcooldown;
            
            switch (powerrandomchance)
            {
            
                case 2:
                    
                    x = Random.Range(-13f, 13f);
                    y = Random.Range(-13f, 13f);
                    Vector3 nowy = new Vector3(x, y);
                    Instantiate(Powerup_Star, nowy, Quaternion.identity);
                    powerrandomchance = Random.Range(1, 5);
                    break;
                case 3:
                    
                    x = Random.Range(-13f, 13f);
                    y = Random.Range(-13f, 13f);
                    Vector3 nowy1 = new Vector3(x, y);
                    Instantiate(Powerup_Dmg, nowy1, Quaternion.identity);
                    powerrandomchance = Random.Range(1, 5);
                    break;
                case 4:
                    
                    x = Random.Range(-13f, 13f);
                    y = Random.Range(-13f, 13f);
                    Vector3 nowy2 = new Vector3(x, y);
                    Instantiate(Powerup_Star, nowy2, Quaternion.identity);
                    powerrandomchance = Random.Range(1, 5);
                    break;
            }
        }
        else
        {
            if (poweruptimestamp <= Time.time)
            {
                
                powerrandomchance = Random.Range(1, 5);
                poweruptimestamp = Time.time + powerupcooldown;
            }
        }
        if (ZombieSpawnTimeStamp <= Time.time&&allowtospawn)
        {
            float x;
            float y;
            x = Random.Range(-13f, 13f);
            y = Random.Range(-13f, 13f);
            Vector3 nowy = new Vector3(x, y);
            if ((Hero.transform.position - nowy).magnitude > 10f&&ZombiesSpawned<AmmountOfZombiesInThisWave)
            {
                ZombiesSpawned++;
                WhatToSpawn.hp = zombiehealth;
                if (CurrentWave % 5 == 0)
                {
                    Necromancer b0ss = (Necromancer)Instantiate(Necro, nowy, Quaternion.identity);
                    b0ss.UpgradePerLevel(CurrentWave);
                }
                else
                {
                    Instantiate(WhatToSpawn, nowy, Quaternion.identity);
                }
                ZombieSpawnTimeStamp = Time.time + zombiespawncooldown;
            }
        }
        //text dissapear
        if (appearingoftxtstmp <= Time.time&&!startdissapearing)
        {
            startdissapearing = true;
            StartCoroutine(FadeImage(true));
            allowtospawn = true;
        }
        //ending wave
        if (ZombiesKilledInWave == AmmountOfZombiesInThisWave)
        {
            allowtoprepare = true;
            allowtospawn = false;
            Wave_Preparation = false;
        }
        //preparation
        if (allowtoprepare&&!Wave_Preparation)
        {
            WhatToSpawn.transform.localScale = (new Vector3(1, 1, 1));
            WhatToSpawn.TurnBackNormalHealth();
            WhatToSpawn.TurnBackNormalMass();
            WhatToSpawn.TurnBackNormalSpeed();
            Progress.value = 0;
            ZombiesSpawned = 0;
            Wave_Preparation = true;
            CurrentWave++;
            guiwave.text = CurrentWave.ToString();
            ZombiesKilledInWave = 0;
            allowtoprepare = false;
            if (CurrentWave % 5 == 0)
            {
                Wavetxt.text = "Walka z b0ssem! Fala: " + CurrentWave.ToString();
            }
            else
            {
                Wavetxt.text = "Fala: " + CurrentWave.ToString();
            }
            StartCoroutine(FadeImage(false));
            if (CurrentWave % 5 == 0)
            {
                /*
                WhatToSpawn.transform.localScale=(new Vector3(5, 5, 5));
                WhatToSpawn.speed = WhatToSpawn.speed + ((CurrentWave / 5) * 0.5f);
                WhatToSpawn.GetComponent<Rigidbody2D>().mass = WhatToSpawn.GetComponent<Rigidbody2D>().mass * 4 * (1 + (CurrentWave / 5 * 0.2f));
                zombiehealth = 500 + (CurrentWave /5) * 500;
                AmmountOfZombiesInThisWave = 1;
                zombiespawncooldown = 1 - (CurrentWave / 20);
                */
                
                AmmountOfZombiesInThisWave = CurrentWave/5;
                zombiespawncooldown = 10f - (CurrentWave / 20f);
            }
            else
            {

                
                
                if (CurrentWave > 5)
                {
                    zombiehealth = 100 + ((CurrentWave - 5) * 15);
                    WhatToSpawn.speed = WhatToSpawn.speed + (CurrentWave - 5 / 5) * 0.1f;
                    WhatToSpawn.GetComponent<Rigidbody2D>().mass= WhatToSpawn.GetComponent<Rigidbody2D>().mass * (1 + (CurrentWave / 5 * 0.1f));
                }
                else
                {
                    zombiehealth = 100;
                }
                if (CurrentWave >= 5)
                {
                    zombiespawncooldown = 1f - (CurrentWave / 20f);
                    if (zombiespawncooldown < 0)
                    {
                        zombiespawncooldown = 0.1f;
                    }
                }
                else
                {
                    zombiespawncooldown = 1f;
                }
                AmmountOfZombiesInThisWave = CurrentWave * 5;
                if (AmmountOfZombiesInThisWave > 30) AmmountOfZombiesInThisWave = 30;
            }
            //bylo allowtospawn=true;
        }
	}
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1f; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                if (CurrentWave - 1 == 0)
                {
                    Wavetxt.color = new Color(1f, 1f, 1f, i);
                }
                else
                {
                    Wavetxt.color = new Color(1f, 1f / (CurrentWave - 1), 1f / (CurrentWave - 1), i);
                }
                yield return null;
            }
            Wavetxt.color = new Color(0, 0, 0, 0);
            allowtoprepare = true;
            
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1f; i += Time.deltaTime)
            {
                // set color with i as alpha
                
                appearingoftxtstmp = Time.time + texthowlong;
                if (CurrentWave - 1 == 0)
                {
                    Wavetxt.color = new Color(1f, 1f, 1f, i);
                }
                else
                {
                    Wavetxt.color = new Color(1f, 1f / (CurrentWave - 1), 1f / (CurrentWave - 1), i);
                }
                startdissapearing = false;
                yield return null;
            }
            
        }
    }
    IEnumerator WaitASec()
    {

        
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    public void ZombieGotKilled()
    {
        TotalZombieKilled++;
        Killed_amnt.text = TotalZombieKilled.ToString();
        ZombiesKilledInWave++;
        Progress.value = (float)ZombiesKilledInWave / (float)AmmountOfZombiesInThisWave * 100f;
    }
}
