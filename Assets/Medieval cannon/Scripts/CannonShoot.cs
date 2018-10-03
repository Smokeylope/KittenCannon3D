using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CannonShoot : MonoBehaviour {

    public GameObject barrel;
    public GameObject spawnPoint;
    public GameObject cannonBall;
    public GameObject platform;

    public float force;
    public float rotationSpeed;
    public float barrelSpeed;

    public float maxBarrelTilt;
    public float minBarrelTilt;
    private float currentTilt;
    private float currentDirection; //kierto y-akselin ympäri

    public float shotInterval;
    private float untilNextShot;

    public AudioSource startClick; //kun tykki alkaa nousta/laskea
    public AudioSource gunMoveSound; //kun tykki nousee/laskee
    public AudioSource platformClick; // kun lavetti alkaa kääntyä
    public AudioSource platformMoveSound; // kun lavetti kääntyy
    public AudioSource shootCannonSound; //
    public bool turnRight;
    public bool turnLeft;
    public bool turnUp;
    public bool turnDown;
    public bool shooting;

	void Start () {
        currentTilt = 0f;
        untilNextShot = 0f;
        currentDirection = 0f;
        turnRight = false;
        turnLeft = false;
        turnUp = false;
        turnDown = false;
        shooting = false;
	}

	void Update () {

        if (currentDirection >= 360f)
        {
            currentDirection -= 360f;
        } else if (currentDirection < 0f)
        {
            currentDirection = 360f - currentDirection;
        }

        if (untilNextShot >= 0)
        {
            untilNextShot -= Time.deltaTime;
        }
        // alkukolahdus
        if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.LeftArrow))
        {
            platformClick.Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Scenes/MenuScene");
        }
		
        if (Input.GetKey(KeyCode.LeftArrow)||turnLeft==true)
        {
            //käänny oikeaan
            float rot = rotationSpeed * Time.deltaTime;
            currentDirection -= rot;
            platform.transform.Rotate(0f, -rot, 0f);
            // ääni kun liikkuu
            if (platformMoveSound.isPlaying == false)
            {
                platformMoveSound.Play();
            }

        }

        if (Input.GetKey(KeyCode.RightArrow)||turnRight == true)
        {
            //käänny vasempaan
            float rot = rotationSpeed * Time.deltaTime;
            currentDirection += rot;
            platform.transform.Rotate(0f, rot, 0f);

            // ääni kun liikkuu
            if (platformMoveSound.isPlaying == false)
            {
                platformMoveSound.Play();
            }

        }
        // ääni pois
        if ((Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) ||
            (Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)))
        {
            if (platformMoveSound.isPlaying)
            {
                platformMoveSound.Stop();
            }
        }

        // alkukolahdus kun tykki alkaa nousta/laskea
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            startClick.Play();
        }

        if (Input.GetKey(KeyCode.UpArrow)||turnUp)
        {
            if (currentTilt < maxBarrelTilt)
            {
                float rot = barrelSpeed * Time.deltaTime;
                currentTilt += rot;
                barrel.transform.Rotate(Vector3.right * rot);
                //
                if (!gunMoveSound.isPlaying)
                {
                    gunMoveSound.Play();
                }

            }
            //tykki ylös
        }
        if (Input.GetKey(KeyCode.DownArrow)||turnDown)
        {
            //tykki alas
            if (currentTilt > minBarrelTilt)
            {
                float rot = barrelSpeed * Time.deltaTime;
                currentTilt -= rot;
                barrel.transform.Rotate(Vector3.left * rot);
                //
                if (!gunMoveSound.isPlaying)
                {
                    gunMoveSound.Play();
                }
            }
        }

        // audiosourcen pysäytys
        if ((Input.GetKeyUp(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)) ||
            (Input.GetKeyUp(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)))
        {
            if (gunMoveSound.isPlaying)
            {
                gunMoveSound.Stop();
            }
        }

        if ((Input.GetKeyDown(KeyCode.Space)||shooting) && untilNextShot<= 0)
        {
            GameObject cb = Instantiate(cannonBall, spawnPoint.transform.position, spawnPoint.transform.rotation);
            Camera.main.GetComponent<CameraController>().cannonBall = cb;
            cb.GetComponent<CannonBall>().velocity = spawnPoint.transform.forward * force;
            shootCannonSound.Play();
            untilNextShot = shotInterval;
        }

	}

    public void StopPlMoveSound()
    {
        if (platformMoveSound.isPlaying)
        {
            platformMoveSound.Stop();
        }
    }

    public void StopGunMoveSound()
    {
        if (gunMoveSound.isPlaying)
        {
            gunMoveSound.Stop();
        }
    }
}
