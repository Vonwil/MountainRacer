using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour {

    public float movementSpeed = 20.0f;
    public Slider acceleration;
	public Rigidbody rigbod;
    private float declerationRate;

    //Wheel Colliders
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider backLeft;
    public WheelCollider backRight;

    //Wheel friction variables (used to adjust stiffness)
    private WheelFrictionCurve backLeftWheelFriction;
    private WheelFrictionCurve backRightWheelFriction;
    private WheelFrictionCurve frontLeftWheelFriction;
    private WheelFrictionCurve frontRightWheelFriction;

    //Terrain stiffness
    public float grassStiffness = 0.2f;
    public float dirtStiffness = 0.3f;
    public float gravelStiffness = 0.4f;
    public float asphaltStiffness = 0.5f;

    //Wheel game objects
    public Transform frontLeftWheelMesh;
	public Transform backLeftWheelMesh;

    //Wheel detction 
    private WheelHit wheelL;
    private WheelHit wheelR;

    // Use this for initialization
    void Start ()
    {
        //Set center of mass slightly below the car
		rigbod.centerOfMass = new Vector3 (0, -1, 0);

        //Set each wheel friction curve to the corrosponding wheel
        backLeftWheelFriction = backLeft.forwardFriction;
        backRightWheelFriction = backRight.forwardFriction;
        frontLeftWheelFriction = frontLeft.forwardFriction;
        frontRightWheelFriction = frontRight.forwardFriction;
    }

    // Update is called once per frame
    void Update()
    {
        //Temporary restart button
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GameLevel");
            acceleration.value = 0;
            movementSpeed = 0;
        }

        //Wheel rotation
		frontLeftWheelMesh.Rotate(frontLeft.rpm / 60 * 360 * Time.deltaTime, 0, 0);
		backLeftWheelMesh.Rotate(backLeft.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }

    void FixedUpdate ()
    {
        Movement();

        // left wheel detection
        backLeft.GetGroundHit(out wheelL);
        backRight.GetGroundHit(out wheelR);

		if (backLeft.isGrounded && backRight.isGrounded) 
		{
			// grass terrain
			if (wheelR.collider.gameObject.tag == "Grass") 
			{
                backLeftWheelFriction.stiffness = grassStiffness;
                backRightWheelFriction.stiffness = grassStiffness;
                frontLeftWheelFriction.stiffness = grassStiffness;
                frontRightWheelFriction.stiffness = grassStiffness;
            }

			/*if (wheelR.collider.gameObject.tag == "Grass") 
			{
				WheelFrictionCurve fwdF;
				fwdF = backRight.forwardFriction;
				fwdF.stiffness = 0.5f;
				backRight.forwardFriction = fwdF;
			}
            */

			//asphalt terrain
			if (wheelL.collider.gameObject.tag == "Asphalt") 
			{
                backLeftWheelFriction.stiffness = asphaltStiffness;
                backRightWheelFriction.stiffness = asphaltStiffness;
                frontLeftWheelFriction.stiffness = asphaltStiffness;
                frontRightWheelFriction.stiffness = asphaltStiffness;
            }

			//if (wheelR.collider.gameObject.tag == "Asphalt") 
			//{
				//WheelFrictionCurve fwdF;
				//fwdF = backRight.forwardFriction;
				//fwdF.stiffness = 0.8f;
				//backRight.forwardFriction = fwdF;
			//}

			//dirt terrain
			if (wheelL.collider.gameObject.tag == "Dirt") 
			{
                backLeftWheelFriction.stiffness = dirtStiffness;
                backRightWheelFriction.stiffness = dirtStiffness;
                frontLeftWheelFriction.stiffness = dirtStiffness;
                frontRightWheelFriction.stiffness = dirtStiffness;
            }

			//if (wheelR.collider.gameObject.tag == "Dirt") 
			//{
				//WheelFrictionCurve fwdF;
				//fwdF = backRight.forwardFriction;
				//fwdF.stiffness = 0.6f;
				//backRight.forwardFriction = fwdF;
			//}

			//gravel terrain
			if (wheelL.collider.gameObject.tag == "Gravel") 
			{
                backLeftWheelFriction.stiffness = gravelStiffness;
                backRightWheelFriction.stiffness = gravelStiffness;
                frontLeftWheelFriction.stiffness = gravelStiffness;
                frontRightWheelFriction.stiffness = gravelStiffness;
            }

			//if (wheelR.collider.gameObject.tag == "Gravel") 
			//{
				//fwdF.stiffness = 0.4f;
				//backRight.forwardFriction = fwdF;
			//}
		}
    }

    void Movement ()
    {
        movementSpeed += acceleration.value;
        declerationRate = movementSpeed / 10;

        //Apply force to rear wheels to simulate rear wheel drive
        backLeft.motorTorque = movementSpeed * Time.deltaTime;
        backRight.motorTorque =movementSpeed * Time.deltaTime;

        if (acceleration.value <= 0 && movementSpeed > 0)
        {
            movementSpeed -= declerationRate;
        }
    }
}