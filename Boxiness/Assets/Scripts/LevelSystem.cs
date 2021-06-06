using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Video;
using System.Data;
using System.Threading.Tasks;

public class LevelSystem : MonoBehaviour
{

    public Level[] Levels;
    public int currentLevel;
    public int highestLevel;
    public int levelLimit;
    public int currentXp;
    public int xpNeeded;

    //UI
    public Slider levelbar;
    public Text number;
    public Text lUp;
    public Text wUnlocked;
    public Text wName;

    //Player
    public Attack1 currentWeapon;
    public Player pl;
    public HealthBar hb;

    //Effects
    public ParticleSystem par;
    public Text plus;

    //Time
    public float slowdown;
    public float slowLength;

    private float beforeDelta;

    //Audio
    public AudioManager aud;

    // Start is called before the first frame update
    void Start()
    {
        xpNeeded = 100;
        levelbar.maxValue = xpNeeded;
        levelbar.value = currentXp;
        number.text = currentLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addXp(int xpToAdd)
    {
        currentXp += xpToAdd;

        //If xpNeeded is reached, level up
        if(currentXp >= xpNeeded)
        {
            currentXp = 0;
            levelbar.value = currentXp;
            levelUp();
        }

        //Update display
        levelbar.value = currentXp;
    }

    private void levelUp()
    {
        if (currentLevel + 1 <= levelLimit)
        {
            currentLevel += 1;

            //PLAY EFFECT
            par.Play();
            aud.Play("levelUp");

            //Time Effect
            beforeDelta = Time.fixedDeltaTime;
            Time.timeScale = slowdown;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            Invoke("ResetTime", slowLength / slowdown);

            //Kill all enemies
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] allKletts = GameObject.FindGameObjectsWithTag("Klett");
            GameObject[] allBullets = GameObject.FindGameObjectsWithTag("Enemy_Bullet");

            foreach (GameObject g in allEnemies)
            {
                g.GetComponent<Enemy1>().Die(false);
            }

            foreach (GameObject g in allKletts)
            {
                g.GetComponent<Klett>().Kill(false);
            }

            foreach (GameObject g in allBullets)
            {
                g.GetComponent<EnemyBullet1>().Kill();
            }

            //give lives back
            if (pl.Health + 1 <= pl.maxHealth)
            {
                pl.Health += 1;
                hb.SetHealth(pl.Health);
            }

            Weapon nextWeapon = null;

            //Apply rewards
            foreach (Level l in Levels)
            {
                if (currentLevel == l.number)
                {
                    currentWeapon.weapon = l.weapon;
                    nextWeapon = l.weapon;
                    currentWeapon.SetWeapon(l.weapon);
                }
            }

            //Show Text
            lUp.gameObject.SetActive(true);
            if (nextWeapon != null)
            {
                wUnlocked.gameObject.SetActive(true);
                wName.text = nextWeapon.name;
            }
            Invoke("HideText", slowLength / slowdown);

            //Calculate new xpNeeded
            xpNeeded = 100 + currentLevel * 10;
            levelbar.maxValue = xpNeeded;

            //Update display
            number.text = currentLevel.ToString();

            nextWeapon = null;
        }
    }

    private void ResetTime()
    {
        Time.fixedDeltaTime = beforeDelta;
        Time.timeScale = 1;
    }

    private void HideText()
    {
        lUp.gameObject.SetActive(false);
        wUnlocked.gameObject.SetActive(false);
    }

    public void setHighScore()
    {
        PlayerPrefs.SetInt("highScore", currentLevel);
    }
}
