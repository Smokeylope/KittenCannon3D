namespace GoogleVR.HelloVR
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DownButton : MonoBehaviour
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
                cs.turnDown = true;
                if (!clickPlayed)
                {
                    clickPlayed = true;
                    cs.startClick.Play();
                }

            } else
            {
                cs.turnDown = false;
                cs.StopGunMoveSound();
                clickPlayed = false;
            }
        }
    }
}
