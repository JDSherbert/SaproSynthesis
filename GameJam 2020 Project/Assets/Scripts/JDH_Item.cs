using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script made by Joshua "JDSherbert" Herbert
/// for SpecialFX Gamejam.
/// https://www.justgiving.com/fundraising/levelupuk
/// </summary>

public class JDH_Item : MonoBehaviour
{
    [System.Serializable]
    public class ItemData
    {
        //Energy
        public float battery_energy = 100.0f;
        public AudioClip battery;

        //Health
        public float healthpack_heal = 100.0f;
        public AudioClip heal;

        //Nutrition
        public float water_nutrition = 50.0f;
        public AudioClip waterdrink;
        public float food_nutrition = 75.0f;
        public AudioClip foodcrunch;

        //Mushrooms
        public int mushrooms = 0;
        public AudioClip mushroompicked;

    }

    public ItemData itemdata = new ItemData();
}
