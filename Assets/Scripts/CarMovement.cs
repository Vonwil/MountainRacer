using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour
{

    public float movementSpeed = 20.0f;
    public float declerationRate = 10.0f;
    public Slider acceleration;

    //Wheels
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider backLeft;
    public WheelCollider backRight;


    //Wheel Collision detection
    private WheelHit whBackLeft;
    private WheelHit whBackRight;


    // Use this for initialization
    void Start ()
    {
    
	}

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GameLevel");
            acceleration.value = 0;
            movementSpeed = 0;
        }
    }

    void FixedUpdate()
    {
        backLeft.GetGroundHit(out whBackLeft);
        backRight.GetGroundHit(out whBackRight);

        if (whBackLeft.collider.gameObject.tag == "Grass")
        {
            Debug.Log("Grass");
        }
    }

    void Movement ()
    {
        movementSpeed += acceleration.value;

        //Apply force to rear wheels to simulate rear wheel drive
        backLeft.motorTorque = Mathf.Clamp(movementSpeed, 0.0f, 500.0f) * Time.deltaTime;
        backRight.motorTorque = Mathf.Clamp(movementSpeed, 0.0f, 500.0f) * Time.deltaTime;

        if (acceleration.value <= 0 && movementSpeed > 0)
        {
            movementSpeed -= declerationRate;
        }
    }
}