using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    public AudioClip Advance_snd;
    public Text AdvanceInfo;
    public Text gui_text;
    public weapon CurrentWeapon;
    public Canvas LosingCanvas;
    public AudioClip turnedzombie;
    public AudioClip hatysz;
    public Text lvl;
    public Canvas Awans;
    public Text ammount;
    public Slider EXP;
    public GameObject damagedfog;
    public guihealth guiscript;
    private int maxhealth=3;
    public float speed=3f;
    public GameObject starbullet;
    private float gothitcooldown = 1.5f;
    private float gothitanimfrequency = 0.15f;
    private float timestamp;
    public AudioClip shootsnd;
    private int currenthealth;
    private bool immortalafterbite;
    private bool loweringimmortality;
    private float timestampGotHit;
    Color original;
    private bool isDead = false;
    Color damaged = new Color(1f,0f,0f,0f);
    private float timestampblink;
    private bool loweringAnimImmort;
    private int nextlevelexp=100;
    // - - - - - - - - - - - - -
    public int Level=1;
    public int currentexp = 0;
    public int Marksmanship = 1;
    public int Agility = 1;
    public int Endurance = 1;
    private int PunktyDoWydania = 0;
    private int OriginalMarksmanship;
    private float diagonalspeed;
    // - - - - - - - - - - - - -
    // Use this for initialization
    void Start()
    {
        EXP.maxValue = nextlevelexp;
        AdvanceInfo.enabled = false;
        gui_text.text = CurrentWeapon.UpdateName();
        LosingCanvas.enabled = false;
        PunktyDoWydania = 0;
        lvl.text = Level.ToString();
        Awans.enabled = false;
        damagedfog.GetComponent<SpriteRenderer>().color = damaged;
        immortalafterbite = false;
        loweringimmortality = false;
        loweringAnimImmort = false;
        currenthealth = maxhealth;
        timestampGotHit = Time.time;
        timestampblink = Time.time;
        original = GetComponent<SpriteRenderer>().color;
        gothitcooldown = 1.0f + (Endurance * 0.5f);
        speed = speed + (Agility * 0.4f);
        guiscript.checkhp();
        diagonalspeed = speed / 1.41f;
    }

    // Update is called once per frame
    void Update()
    {
        ammount.text = PunktyDoWydania.ToString();
        EXP.value = currentexp;
         if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))&& (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))|| (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))&& (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            || (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))&& (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))|| (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))&& (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
        {
            speed = diagonalspeed;
        }
        else
        {
            
            speed = 3 + (Agility * 0.4f);
        }
         if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
         {

             transform.Translate(new Vector3(speed * Time.deltaTime, 0),Space.World);
         }
         if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
         {
             transform.Translate(new Vector3(0, speed * Time.deltaTime), Space.World);
         }
         if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
         {
             transform.Translate(new Vector3(0, -speed * Time.deltaTime), Space.World);

         }
         if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
         {
             transform.Translate(new Vector3(-speed*Time.deltaTime,0),Space.World);

         }
        if (Input.GetKey(KeyCode.Mouse0)&&timestamp<=Time.time&&Time.timeScale>0.5f)
        {
            timestamp = Time.time + CurrentWeapon.givecooldown();
            
            Vector2 mousepos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 mypos = transform.position;
            Vector2 dir = mousepos - mypos;
            dir.Normalize();
            CurrentWeapon.Shoot(dir, mypos);

        }
        Vector2 mousepos1 = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 posi = transform.position;
        var dir1 = mousepos1 - posi;
        var angle = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        if (currenthealth == 0&&isDead==false)
        {
            isDead = true;
            Time.timeScale = 0f;
            GetComponent<AudioSource>().PlayOneShot(turnedzombie);
            LosingCanvas.enabled = true;
            
        }
        if (immortalafterbite == true)
        {
            if (loweringimmortality == false)
            {
                if (currenthealth != 0)
                {
                    GetComponent<AudioSource>().PlayOneShot(hatysz);
                    StartCoroutine(FadeImage(true));
                }
                timestampGotHit = Time.time + gothitcooldown;
                loweringimmortality = true;
            }
            if (timestampGotHit > Time.time)
            {
                if (loweringAnimImmort)
                {
                    timestampblink = Time.time + gothitanimfrequency;
                    loweringAnimImmort = false;
                }
                if (timestampblink > Time.time)
                {
                    GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
                }
                else if(timestampblink+gothitanimfrequency>Time.time)
                {
                    GetComponent<SpriteRenderer>().color = original;
                }
                else
                {
                    loweringAnimImmort = true;
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().color = original;
                immortalafterbite = false;
                loweringimmortality = false;
                loweringAnimImmort = false;
            }
        }
        if (currenthealth == 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.8f, 0.3f, 1f);
        }
        if (currentexp == nextlevelexp)
        {
            GetComponent<AudioSource>().PlayOneShot(Advance_snd);
            Level++;
            lvl.text = Level.ToString();
            PunktyDoWydania++;
            currentexp = 0;
            nextlevelexp += 50;
            EXP.maxValue = nextlevelexp;
            if (PunktyDoWydania > 0)
            {
                AdvanceInfo.enabled = true;
            }
            else
            {
                AdvanceInfo.enabled = false;
            }
        }
        if(PunktyDoWydania>0&& Input.GetKey(KeyCode.Space)&&!isDead)
        {
            Awans.enabled = true;
            Time.timeScale = 0.05f;
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        
        if (col.collider.tag == "Baddie"&&immortalafterbite==false)
        {
            if (col.gameObject.GetComponent<Zombie>() != null)
            {
                if (col.gameObject.GetComponent<Zombie>().isBaddieAlive() == true)
                {
                    immortalafterbite = true;
                    guiscript.lowerhp();
                    currenthealth--;
                }
            }
            else
            if(col.gameObject.GetComponent<Enemy>() != null)
            {
                if (col.gameObject.GetComponent<Enemy>().isBaddieAlive() == true)
                {
                    immortalafterbite = true;
                    guiscript.lowerhp();
                    currenthealth--;
                }
                immortalafterbite = true;
                guiscript.lowerhp();
                currenthealth--;
            }
        }
    }
    public int giveMarksmanship()
    {
        return Marksmanship;
    }
    public int giveAgility()
    {
        return Agility;
    }
    public int giveEndurance()
    {
        return Endurance;
    }
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 0.3f; i >= 0; i -= Time.deltaTime * 2)
            {
                // set color with i as alpha
                damagedfog.GetComponent<SpriteRenderer>().color = new Color(1f, 0.00f, 0.00f, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 0.3f; i += Time.deltaTime * 2)
            {
                // set color with i as alpha
                damagedfog.GetComponent<SpriteRenderer>().color = new Color(1f, 0.00f, 0.00f, i);
                yield return null;
            }
        }
    }
    public int giveactivehealth()
    {
        return currenthealth;
    }
    public void givemeexp(int iloscexp)
    {
        currentexp += iloscexp;
        if (currentexp > nextlevelexp)
        {
            currentexp = nextlevelexp;
        }
    }
    public void MarksmanAdvance()
    {
        if (PunktyDoWydania > 0)
        {
            Marksmanship++;
            PunktyDoWydania--;
            if (PunktyDoWydania > 0)
            {
                AdvanceInfo.enabled = true;
            }
            else
            {
                AdvanceInfo.enabled = false;
            }
        }
    }
    public void AgilityAdvance()
    {
        if (PunktyDoWydania > 0)
        {
            Agility++;
            speed = speed + (Agility * 0.4f);
            diagonalspeed = speed / 1.41f;
            if (speed > 6) speed = 6;
            PunktyDoWydania--;
            if (PunktyDoWydania > 0)
            {
                AdvanceInfo.enabled = true;
            }
            else
            {
                AdvanceInfo.enabled = false;
            }
        }
    }
    public void EnduranceAdvance()
    {
        if (PunktyDoWydania > 0)
        {
            if (maxhealth < 6)
            {
                maxhealth++;
            }
            Endurance++;
            gothitcooldown = 1.0f + (Endurance * 0.5f);
            if (currenthealth < 6)
            {
                currenthealth++;
            }
            guiscript.checkhp();
            PunktyDoWydania--;
            if (PunktyDoWydania > 0)
            {
                AdvanceInfo.enabled = true;
            }
            else
            {
                AdvanceInfo.enabled = false;
            }
        }
    }
    public void exitmenu()
    {
        
        Awans.enabled = false;
        Time.timeScale = 1f;
    }
    public void Exitgame()
    {
        
        Application.Quit();
    }
    public void ResetLevel()
    {
       
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void GotoMenu()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void Powerup(int type,Vector2 pos)
    {
        switch (type)
        {
            case 1:StartCoroutine(GiveBoost(1,pos)); break;
            case 2: StartCoroutine(GiveBoost(2, pos)); break;
            case 3: StartCoroutine(GiveBoost(3, pos)); break;
            default:break;
        }
    }
    IEnumerator GiveBoost(int type, Vector2 pos)
    {

        switch (type)
        {
            case 1: speed=speed+(speed/4); yield return new WaitForSeconds(15); speed = 3f + (Agility * 0.4f);
                if (speed > 6) speed = 6; break;
            case 2: OriginalMarksmanship = Marksmanship; Marksmanship += 3;/*do zmiany na damage*/ yield return new WaitForSeconds(10);Marksmanship = OriginalMarksmanship; break;
            case 3:
                for (int i = 0; i <= 360; i += 45)
                {
                    GameObject projectile = (GameObject)Instantiate(starbullet, pos, Quaternion.identity);
                    projectile.transform.Rotate(new Vector3(0, 0, i));
                    projectile.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(2,0)*10, ForceMode2D.Impulse) ; 
                }
                break;
            default: break;
        }
    }
    public void ChangeWeapon(int ID)
    {
        switch (ID)
        {
            // case 1:CurrentWeapon = GameObject.Find("Pistolet").GetComponent<Pistol>(); gui_text.text = CurrentWeapon.UpdateName(); break;
            //case 2: CurrentWeapon = GameObject.Find("Miotacz Płomieni").GetComponent<Flame_Thower>(); gui_text.text = CurrentWeapon.UpdateName(); break;
            //case 3: CurrentWeapon = GameObject.Find("Shotgun").GetComponent<Strzelba>(); gui_text.text = CurrentWeapon.UpdateName(); break;
            // case 4: CurrentWeapon = GameObject.Find("SMG").GetComponent<SMG>(); gui_text.text = CurrentWeapon.UpdateName(); break;
            case 1:
                GameObject pistol =(GameObject) Instantiate(Resources.Load("Pistol"));
                CurrentWeapon = pistol.GetComponent<Pistol>() ; gui_text.text = CurrentWeapon.UpdateName();
                CurrentWeapon.transform.parent = transform;
                break;
            case 2:

                GameObject strzelba = (GameObject)Instantiate(Resources.Load("Shotgun"));
                CurrentWeapon = strzelba.GetComponent<Strzelba>(); gui_text.text = CurrentWeapon.UpdateName();
                CurrentWeapon.transform.parent = transform;
                break;
            case 3:

                GameObject smg = (GameObject)Instantiate(Resources.Load("SMG"));
                CurrentWeapon = smg.GetComponent<SMG>() ; gui_text.text = CurrentWeapon.UpdateName();
                CurrentWeapon.transform.parent = transform;
                break;
            case 4:

                GameObject ft = (GameObject)Instantiate(Resources.Load("flame_thrower"));
                CurrentWeapon = ft.GetComponent<Flame_Thower>() ; gui_text.text = CurrentWeapon.UpdateName();
                CurrentWeapon.transform.parent = transform;
                break;
            case 5:

                GameObject sztucer = (GameObject)Instantiate(Resources.Load("Sztucer"));
                CurrentWeapon = sztucer.GetComponent<Sztucer>(); gui_text.text = CurrentWeapon.UpdateName();
                CurrentWeapon.transform.parent = transform;
                break;

        }
    }
}
