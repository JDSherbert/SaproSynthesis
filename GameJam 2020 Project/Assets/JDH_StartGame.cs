using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script made by Joshua "JDSherbert" Herbert
/// for SpecialFX Gamejam.
/// https://www.justgiving.com/fundraising/levelupuk
/// </summary>

public class JDH_StartGame : MonoBehaviour
{
    public void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CreditJDH()
    {
        Application.OpenURL("https://jdsherbertportfolio.wordpress.com/");
    }
    public void CreditRW()
    {
        Application.OpenURL("https://richardwilkes.wixsite.com/portfolio");
    }

}
