using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    public GameObject explosion;
    public GameObject scoreText;
    public float bounciness = 0.5f;
    public float gravity = 4.0f;
    public float explosionStrength = 20.0f;

    [HideInInspector]
    public Vector3 velocity;

    private float destroyTimer = 1.0f;

	// Use this for initialization
	void Start () {
        //Destroy(this.gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 g = new Vector3(0.0f, -gravity, 0.0f) * Time.deltaTime;
        velocity += g;
        transform.position += velocity * Time.deltaTime;

        int score = (int) Mathf.Sqrt(Mathf.Pow(transform.position.x, 2.0f) + Mathf.Pow(transform.position.z, 2.0f));
        scoreText.GetComponent<TextMesh>().text = score.ToString();

        if (velocity.sqrMagnitude < 0.1f)
        {
            destroyTimer -= Time.deltaTime;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(velocity.normalized, Vector3.up);
        }

        if (destroyTimer < 0.0f)
        {
            ApiFunctions.PostScore(score);
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (velocity.y <= 0f)
        {
            if (other.gameObject.tag == "Spikes")
            {
                velocity = Vector3.zero;
                gravity = 0.0f;
            }
            else if (other.gameObject.tag == "Explosive")
            {
                velocity.y = -velocity.y + explosionStrength;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
                velocity.y = -velocity.y;
                velocity *= bounciness;
            }
        }
    }
}
