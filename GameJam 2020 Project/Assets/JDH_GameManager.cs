using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script made by Joshua "JDSherbert" Herbert
/// for SpecialFX Gamejam.
/// https://www.justgiving.com/fundraising/levelupuk
/// </summary>

public class JDH_GameManager : MonoBehaviour
{
    [System.Serializable]
    public class ManagerData
    {
        public GameObject Player;
        public JDH_HUD hud;
        public JDH_Item item;


        public GameObject gameoverscreen;
        public AudioClip gameoversound;

        public GameObject winstatescreen;
        public AudioClip winsound;


        public float delay = 5f;
    }

    public ManagerData managerdata = new ManagerData();

    public void Start()
    {
        managerdata.hud = GameObject.FindWithTag("Player").GetComponent<JDH_HUD>();
        managerdata.gameoverscreen.SetActive(false);
    }

    public void Update()
    {
        MushroomCounter();
        GameOver();
       
    }

    public void GameOver()
    {
        if(managerdata.hud.parameter.isDead == true)
        {
            managerdata.gameoverscreen.SetActive(true);
            managerdata.hud.ui.audiosource_ui.PlayOneShot(managerdata.gameoversound);
            managerdata.hud.parameter.isDead = false;
            StartCoroutine(Sequence());
        }
    }

    public IEnumerator Sequence()
    {
        



        yield return new WaitForSeconds(managerdata.delay);

        SceneManager.LoadScene(0);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void MushroomCounter()
    {
        managerdata.hud.ui.mushroomcount_txt.text = managerdata.item.itemdata.mushrooms.ToString();

        if (managerdata.item.itemdata.mushrooms >= 5)
        {
            managerdata.hud.ui.audiosource_ui.PlayOneShot(managerdata.winsound);
            WinState();
        }

    }

    public void WinState()
    {
        managerdata.item.itemdata.mushrooms = 0;


        managerdata.winstatescreen.SetActive(true);
        
        Time.timeScale = 0;
        StartCoroutine(Sequence());

    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
