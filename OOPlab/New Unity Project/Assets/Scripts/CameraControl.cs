using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	public float offsetx = 0;
	public float offsety = 0;

	public GameObject bg;
	public WheelJoint2D speed_car;
	
	void FixedUpdate()
	{
		if (target)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(new Vector3(target.position.x, target.position.y + 0.8f, target.position.z));
			Vector3 delta = new Vector3(target.position.x, target.position.y + 0.8f, target.position.z) - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;


			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
		Vector3 posBG = new Vector3(bg.transform.position.x, bg.transform.position.y, bg.transform.position.z);
		if (speed_car.motor.motorSpeed <= -250)
		{
			posBG.x = Camera.main.transform.position.x + 1.0f;
		}
		else if (speed_car.motor.motorSpeed >= 50)
		{
            posBG.x = Camera.main.transform.position.x - 1.0f;
        }
		bg.transform.position = Vector3.SmoothDamp(bg.transform.position, posBG, ref velocity, 1.0f);

	}
}
