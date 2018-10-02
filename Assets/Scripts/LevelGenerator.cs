using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public int objectCount = 500;
    public float minDistance = 5.0f;
    public float maxDistance = 200.0f;

    public List<GameObject> objects;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < objectCount; ++i)
        {
            float angle = Random.Range(0.0f, Mathf.PI * 2.0f);
            float distance = Random.Range(minDistance, maxDistance);

            Vector3 direction = new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle));
            Vector3 position = direction * distance;

            GameObject.Instantiate(objects[Random.Range(0, objects.Count)], position, Quaternion.LookRotation(direction, Vector3.up));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
