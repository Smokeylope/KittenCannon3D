namespace GoogleVR.HelloVR
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UpButton : MonoBehaviour
    {
        public GameObject cannonPlatform;
        private CannonShoot cs;
        bool clickPlayed;

        void Start()
        {
            cs = cannonPlatform.GetComponent<CannonShoot>();
            clickPlayed = false;
        }


        public void SetGazedAt(bool gazedAt)
        {
            if (gazedAt)
            {
                cs.turnUp = true;
                if (!clickPlayed)
                {
                    clickPlayed = true;
                    cs.startClick.Play();
                }

            } else
            {
                cs.turnUp = false;
                cs.StopGunMoveSound();
                clickPlayed = false;
            }
        }
    }
}
