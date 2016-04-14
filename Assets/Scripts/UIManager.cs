using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

    CarMovement car;

    //variables
    public Slider speed;

	// Use this for initialization
	void Start () {
        car = GameObject.FindGameObjectWithTag("Car").GetComponent<CarMovement>();
	}
	
	// Update is called once per frame
	void Update () {

        speed.value = car.movementSpeed;
	
	}

    public void OnValueChange(float value)
    {
        if (value < 25)
        {
            car.movementSpeed = 25.0f;
        }
    }
}
