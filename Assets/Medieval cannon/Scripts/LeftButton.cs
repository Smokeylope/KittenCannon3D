namespace GoogleVR.HelloVR
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LeftButton : MonoBehaviour
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
                cs.turnLeft = true;
                if (!clickPlayed)
                {
                    clickPlayed = true;
                    cs.platformClick.Play();
                }
            } else
            {
                cs.turnLeft = false;
                cs.StopPlMoveSound();
                clickPlayed = false;
            }
        }
    }
}
