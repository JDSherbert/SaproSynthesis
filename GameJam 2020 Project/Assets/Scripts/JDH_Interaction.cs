using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script made by Joshua "JDSherbert" Herbert
/// for SpecialFX Gamejam.
/// https://www.justgiving.com/fundraising/levelupuk
/// </summary>

public class JDH_Interaction : MonoBehaviour
{
    [System.Serializable]
    public class InteractionSettings
    {
        public float raycast_range = 3.0f;
    }

    [System.Serializable]
    public class InteractionData
    {
        public JDH_HUD hud;
        public JDH_Item item;
        public Camera camera;
        public RaycastHit hit;
        public Ray ray;
        public AudioSource audiosource;
    }

    [System.Serializable]
    public class Payload
    {
        public AudioClip interactionsound;
        public AudioClip usesound;
        public GameObject useUI;
        public bool looktrigger = false;
        public bool played = false;
    }

    public InteractionSettings interactionsetting = new InteractionSettings();
    public InteractionData interactiondata = new InteractionData();
    public Payload payload = new Payload();

    public void Awake()
    {
        interactiondata.camera = Camera.main;
        payload.useUI.SetActive(false);
        interactiondata.hud = GetComponent<JDH_HUD>();
        interactiondata.item = GetComponent<JDH_Item>();
    }

    public void Update()
    {
        InteractionScan();
    }

    public void InteractionScan()
    {
        interactiondata.ray.origin = interactiondata.camera.transform.position;
        interactiondata.ray.direction = interactiondata.camera.transform.TransformDirection(Vector3.forward);

        if 
        (Physics.Raycast
        (interactiondata.ray.origin, 
        interactiondata.ray.direction,
        out interactiondata.hit, 
        interactionsetting.raycast_range))
        {
            Debug.DrawRay
                (interactiondata.ray.origin,
                interactiondata.ray.direction * 
                interactionsetting.raycast_range, 
                Color.green);
            Debug.Log
                ("Hit point: " + interactiondata.hit.point + "\n" +
                "Hit collider: " + interactiondata.hit.collider + "\n" +
                "Hit item: " + interactiondata.hit.collider + "\n");

            payload.looktrigger = true;
            payload.useUI.SetActive(true);

            if(payload.looktrigger == true && payload.played == false)
            {
                if(interactiondata.hit.collider.tag == "Interactable")
                {
                    interactiondata.audiosource.PlayOneShot(payload.interactionsound, 1f);
                    payload.played = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                
                interactiondata.audiosource.PlayOneShot(payload.usesound);
                ConsumeItem();

                Destroy(interactiondata.hit.collider.gameObject);
            }
        }
        else
        {
            Debug.DrawRay
            (interactiondata.ray.origin,
            interactiondata.ray.direction *
            interactionsetting.raycast_range,
            Color.red);

            payload.looktrigger = false;
            payload.useUI.SetActive(false);
            payload.played = false;
        }
    }

    public void ConsumeItem()
    {
        if (interactiondata.hit.collider.gameObject.name.Contains("RW_Water"))
        {
            interactiondata.hud.parameter.nutrition += interactiondata.item.itemdata.water_nutrition;
            interactiondata.audiosource.PlayOneShot(interactiondata.item.itemdata.waterdrink);
        }
        if (interactiondata.hit.collider.gameObject.name.Contains("RW_Food"))
        {
            interactiondata.hud.parameter.nutrition += interactiondata.item.itemdata.food_nutrition;
            interactiondata.audiosource.PlayOneShot(interactiondata.item.itemdata.foodcrunch);
        }
        if (interactiondata.hit.collider.gameObject.name.Contains("RW_TreatmentKit"))
        {
            interactiondata.hud.parameter.health += interactiondata.item.itemdata.healthpack_heal;
            interactiondata.audiosource.PlayOneShot(interactiondata.item.itemdata.heal);
        }
        if (interactiondata.hit.collider.gameObject.name.Contains("RW_IonCell"))
        {
            interactiondata.hud.parameter.energy += interactiondata.item.itemdata.battery_energy;
            interactiondata.audiosource.PlayOneShot(interactiondata.item.itemdata.battery);
        }
        if (interactiondata.hit.collider.gameObject.name.Contains ("RW_FungusGreen"))
        {
            interactiondata.item.itemdata.mushrooms++;
            interactiondata.audiosource.PlayOneShot(interactiondata.item.itemdata.mushroompicked);
        }
        if (interactiondata.hit.collider.gameObject.name.Contains("RW_FungusRed"))
        {
            interactiondata.item.itemdata.mushrooms += 2;
            interactiondata.audiosource.PlayOneShot(interactiondata.item.itemdata.mushroompicked);
        }
    }
}
