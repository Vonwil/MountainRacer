using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BaseCarMovement : MonoBehaviour {
	public List<AxleInfo> axleInfos; // information about each axle
	public float maxMotorTorque; // max torque that the motor can apply to the wheel
	public Slider acceleration; // set slider to be called
	public float engine;
	public float brake = 100.0f;
	public bool isBraking;

	[System.Serializable]
public class AxleInfo {
		public WheelCollider leftWheel;
		public WheelCollider rightWheel;
		public bool motor; // is this wheel attached to motor?
	}

	public void FixedUpdate()
	{
		if (acceleration.value == 0) {
			engine = 0;
			isBraking = true;
		} else {
			engine = maxMotorTorque * acceleration.value;
			isBraking = false;
		}

		foreach (AxleInfo axleInfo in axleInfos) {
			if (axleInfo.motor) {
				axleInfo.leftWheel.motorTorque = engine;
				axleInfo.rightWheel.motorTorque = engine;
			}
			if (isBraking == true) {
				axleInfo.leftWheel.brakeTorque = brake;
				axleInfo.rightWheel.brakeTorque = brake;
			} else {
				axleInfo.leftWheel.brakeTorque = 0;
				axleInfo.rightWheel.brakeTorque = 0;
			}
				
			ApplyLocalPositionToVisuals (axleInfo.leftWheel);
			ApplyLocalPositionToVisuals (axleInfo.rightWheel);

		}
	}

// finds the corresponding visual wheel
// correctly applies the transform
public void ApplyLocalPositionToVisuals(WheelCollider collider)
{
		if (collider.transform.childCount == 0) {
			return;
		}

		Transform visualWheel = collider.transform.GetChild(0);

		Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose(out position, out rotation);

		visualWheel.transform.position = position;
		visualWheel.transform.rotation = rotation;
	}		
}