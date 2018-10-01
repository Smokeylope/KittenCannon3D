using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject cannon;
    public GameObject cannonBall;
    public Vector3 positionOffset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        GameObject target = cannonBall;
        Vector3 offset = positionOffset;

		if (target == null)
        {
            target = cannon;
            offset = Vector3.zero;
            transform.rotation = target.transform.rotation;
        }
        else
        {
            Vector3 direction = target.transform.position;
            direction.y = 0.0f;
            direction.Normalize();

            transform.rotation = Quaternion.LookRotation(direction, Vector3.up); ;
        }

        
        transform.position = target.transform.position + transform.rotation * offset;
    }
}
