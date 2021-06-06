using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //DEATH

    //scripts
    private Player pl;
    private Attack1 att;
    private Move mv;
    private LevelSystem lv;

    //effect
    public Animator crossfade;


    private void Awake()
    {
        //Make sure there's only one instance
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        att = GameObject.Find("Attack1").GetComponent<Attack1>();
        mv = GameObject.Find("Move").GetComponent<Move>();
        lv = GameObject.Find("LevelBar").GetComponent<LevelSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        //save high score
        lv.setHighScore();

        //disable scripts
        lv.enabled = false;
        pl.enabled = false;
        mv.enabled = false;
        att.enabled = false;

        //apply effect & open menu
        crossfade.Play("gameover_fade_in");
        Invoke("NoChild", 2);
    }

    public void PlayAgain()
    {
        Debug.Log("PlayAgain");
        SceneManager.LoadScene("Game");
    }

    public void BackToMenu()
    {
        Debug.Log("BackToMenu");
        SceneManager.LoadScene("MainMenu");
    }

    private void NoChild()
    {
        crossfade.transform.GetChild(0).SetParent(GameObject.Find("GUI").transform);
        crossfade.transform.GetChild(0).SetParent(GameObject.Find("GUI").transform);
    }
}
