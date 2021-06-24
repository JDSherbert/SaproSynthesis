using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Script made by Joshua "JDSherbert" Herbert
/// for SpecialFX Gamejam.
/// https://www.justgiving.com/fundraising/levelupuk
/// </summary>

public class JDH_HUD : MonoBehaviour
{
    [System.Serializable]
    public class UIElements
    {
        public Slider health;

        public Slider energy;
        public GameObject energylowtext;

        public Slider nutrition;

        public GameObject hintbox;
        public GameObject hint1;
        public GameObject hint2;

        public Text mushroomcount_txt;

        public GameObject biohazardwarning;
        public GameObject pausemenu;
        public AudioSource audiosource_voice;
        public AudioSource audiosource_ui;
    }

    [System.Serializable]
    public class Parameters
    {
        public float health = 100f;
        public float energy = 100f;
        public float nutrition = 100f;

        public float maxhealth = 100f;
        public float maxenergy = 100f;
        public float maxnutrition = 100f;

        public bool biohazard = false;

        public bool isDead = false;
    }

    [System.Serializable]
    public class ParameterData
    {
        public float health_loss = -1.0f;
        public float energy_loss = -0.1f;
        public float nutrition_loss = -0.2f;

        public float biohazardmultiplier = 3.0f;

        public int hintnumber = 0;
    }

    [System.Serializable]
    public class TickSystem
    {
        public float tick = 0f;
        public float tickmax = 5f;
        public float tickincrement = 0.1f;
        public bool ticked = false;
    }

    [System.Serializable]
    public class Payload
    {
        //SFX
        public AudioClip healthlow_voice;
        public AudioClip energylow_voice;
        public AudioClip biohazardwarning_voice;
        public AudioClip biohazardnormalized_voice;

        public AudioClip energylow_alarm;
        public AudioClip biohazard_alarm;
        public AudioClip pausemenuon;
        public AudioClip pausemenuoff;

        public AudioClip notification;

        public bool biotriggered;
        public bool energytriggered;

    }

    public UIElements ui = new UIElements();
    public Parameters parameter = new Parameters();
    public ParameterData data = new ParameterData();
    public TickSystem ticksys = new TickSystem();
    public Payload payload = new Payload();



 

    public void Update()
    {
        Tick();
        CheckDead();
        CheckHazard();
        CheckEnergy();
        Decay();
        DisplayStatus();
        KillSelf();
        Paused();
        HintMenu();
    }

    public void Tick()
    {
        ticksys.ticked = false;
        ticksys.tick += ticksys.tickincrement;
        if (ticksys.tick >= ticksys.tickmax)
        {
            ticksys.ticked = true;
            ticksys.tick = 0f;
        }
    }
    public void Decay()
    {
        if (ticksys.ticked == true)
        {
            //Biohazard false
            if (parameter.biohazard == false)
            {
                parameter.energy += data.energy_loss;
                parameter.nutrition += data.nutrition_loss;

                //If any stats are 0 or below
                if (ui.energy.value <= 0f || ui.nutrition.value <= 0.0f)
                {
                    parameter.health += data.health_loss;
                }
            }

            if (parameter.biohazard == true)
            {
                parameter.energy += data.energy_loss * data.biohazardmultiplier;
                parameter.nutrition += data.nutrition_loss * data.biohazardmultiplier;

                //If any stats are 0 or below
                if (ui.energy.value <= 0f || ui.nutrition.value <= 0.0f)
                {
                    parameter.health += data.health_loss * data.biohazardmultiplier;
                }
            }
        }
    }

    public void DisplayStatus()
    {
        ui.energy.value = parameter.energy;
        ui.nutrition.value = parameter.nutrition;
        ui.health.value = parameter.health;
    }

    public void CheckEnergy()
    {
        if (parameter.energy < 25.0f)
        {
            ui.energylowtext.SetActive(true);
            ui.audiosource_ui.PlayOneShot(payload.energylow_alarm, 0.01f);
            if (payload.energytriggered == false)
            {
                ui.audiosource_voice.PlayOneShot(payload.energylow_voice, 1.0f);
                payload.energytriggered = true;
            }
        }
        else
        {
            ui.energylowtext.SetActive(false);
            if (payload.energytriggered == true)
            {
                payload.energytriggered = false;
            }
        }
    }
    public void CheckHazard()
    {
        if (parameter.biohazard == true)
        {
            ui.biohazardwarning.SetActive(true);
            ui.audiosource_ui.PlayOneShot(payload.biohazard_alarm);

            if (payload.biotriggered == false)
            {
                ui.audiosource_voice.PlayOneShot(payload.biohazardwarning_voice);
                payload.biotriggered = true;
            }
        }
        else
        {
            ui.biohazardwarning.SetActive(false);
            if (payload.biotriggered == true)
            {
                ui.audiosource_voice.PlayOneShot(payload.biohazardnormalized_voice);
                payload.biotriggered = false;
            }
        }
    }

    public void CheckDead()
    {
        if (ui.health.value <= 0.0f)
        {
            Debug.Log("DEAD");
            parameter.isDead = true;
        }
    }

    public void KillSelf()
    {
        if (Input.GetKey(KeyCode.O))
        {
            parameter.energy += data.energy_loss * 20;
            parameter.nutrition += data.nutrition_loss * 20;
        }
    }

    public void Paused()
    {
        if (ui.pausemenu.activeSelf == true)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }

        if (Input.GetButtonDown("Cancel") && ui.pausemenu.activeSelf == false)
        {
            ui.audiosource_ui.PlayOneShot(payload.pausemenuon);
            
            ui.pausemenu.SetActive(true);
        }
        else if (Input.GetButtonDown("Cancel") && ui.pausemenu.activeSelf == true)
        {
            
            ui.audiosource_ui.PlayOneShot(payload.pausemenuoff);
            ui.pausemenu.SetActive(false);
        }
    }

    public void HintMenu()
    {
        if (ui.hintbox.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.H) && data.hintnumber == 2)
        {
            ui.hintbox.SetActive(false);
            ui.hint1.SetActive(false);
            ui.hint2.SetActive(false);
            data.hintnumber++;
        }

        if (Input.GetKeyDown(KeyCode.H) && data.hintnumber == 1)
        {
            ui.hint1.SetActive(false);
            ui.hint2.SetActive(true);
            data.hintnumber++;
        }

        if (Input.GetKeyDown(KeyCode.H) && data.hintnumber == 0)
        {
            ui.hintbox.SetActive(true);
            ui.hint1.SetActive(true);
            data.hintnumber++;
        }

        if(data.hintnumber >= 3)
        {
            data.hintnumber = 0;
        }

    }




















    //EASTEREGG
    public GameObject gilbey4eva;
    public void CloneGilbey4eva(bool rachaelexists)
    {
        string description = "gay";


        float maximum = Mathf.Infinity;
        float gilbeys = 0;

        while(rachaelexists == true && gilbeys < maximum)
        {
            Instantiate(gilbey4eva);
            Debug.Log("Gilbey4eva is " + description);
            gilbeys++;
        }

    }

}
