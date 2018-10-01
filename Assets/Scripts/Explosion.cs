using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float growth;
    public float lift;
    public GameObject mainCamera;




    void Start () {
        Destroy(this.gameObject, 0.4f);
        mainCamera = Camera.main.gameObject;
    }
	
	void Update () {
        Vector3 targetDir = mainCamera.transform.position - transform.position;
        float step = 200 * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        //newDir += new Vector3(90, 180, 0);
        transform.rotation = Quaternion.LookRotation(newDir);

        transform.Translate(0, +lift*Time.deltaTime, 0);
        transform.localScale += new Vector3(growth*Time.deltaTime, growth*Time.deltaTime, growth*Time.deltaTime);

	}
}
