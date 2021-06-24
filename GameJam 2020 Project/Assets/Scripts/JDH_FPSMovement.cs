using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script made by Joshua "JDSherbert" Herbert
/// for SpecialFX Gamejam.
/// https://www.justgiving.com/fundraising/levelupuk
/// </summary>

public class JDH_FPSMovement : MonoBehaviour
{
    [System.Serializable]
    public class PlayerProperties
    {
        public float movementspeed = 10.0f;
        public float jumpheight = 5.0f;
        public Vector3 movementVector;
        public Vector3 directionVector;
        public float damp = 3.7f;
    }

    public class PlayerData
    {
        public Rigidbody rigidbody;
        public AudioSource audiosource;
        public Collider collider;
        public Camera camera;
    }

    [System.Serializable]
    public class InputSettings
    {
        public float rotationX;
        public float rotationY;
        public float sensitivityX = 15F;
        public float sensitivityY = 15F;

        public float minimumX = -360F;
        public float maximumX = 360F;

        public float minimumY = -60F;
        public float maximumY = 60F;



    }

    public PlayerProperties playerproperty = new PlayerProperties();
    public PlayerData playerdata = new PlayerData();
    public InputSettings inputsetting = new InputSettings();

    public void Awake()
    {
        GetStuff();
    }

    public void Update()
    {
        LockCursor();
        MouseLook();
        Movement();
    }

    //Look Mouse
    public void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void MouseLook()
    {
        inputsetting.rotationX = playerdata.camera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * inputsetting.sensitivityX;

        inputsetting.rotationY += Input.GetAxis("Mouse Y") * inputsetting.sensitivityY;
        inputsetting.rotationY = Mathf.Clamp(inputsetting.rotationY, inputsetting.minimumY, inputsetting.maximumY);

        playerdata.camera.transform.localEulerAngles = new Vector3(-inputsetting.rotationY, inputsetting.rotationX, 0);
    }

    //Movement
    public void Movement()
    {

        // Get directions relative to camera
        Vector3 cameraforward = playerdata.camera.transform.forward;
        Vector3 cameraright = playerdata.camera.transform.right;

        if (Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.S) == false)
        {
            //playerdata.rigidbody.velocity = new Vector3(0, -transform.position.y * -1 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
            playerdata.rigidbody.AddForce((cameraright * -1) * playerproperty.movementspeed);
        if (Input.GetKey(KeyCode.D))
            playerdata.rigidbody.AddForce((cameraright * 1) * playerproperty.movementspeed);
        if (Input.GetKey(KeyCode.W))
            playerdata.rigidbody.AddForce((cameraforward * 1) * playerproperty.movementspeed);
        if (Input.GetKey(KeyCode.S))
            playerdata.rigidbody.AddForce((cameraforward * -1) * playerproperty.movementspeed);

    }

    public void GetStuff()
    {
        playerdata.rigidbody = GetComponent<Rigidbody>();
        playerdata.audiosource = GetComponent<AudioSource>();
        playerdata.collider = GetComponent<Collider>();
        playerdata.camera = Camera.main;
    }
}
