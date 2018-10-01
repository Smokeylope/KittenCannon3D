namespace GoogleVR.HelloVR
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ShootButton : MonoBehaviour
    {
        public GameObject cannonPlatform;
        private CannonShoot cs;

        void Start()
        {
            cs = cannonPlatform.GetComponent<CannonShoot>();
        }


        public void SetGazedAt(bool gazedAt)
        {
            if (gazedAt)
            {
                cs.shooting = true;

            } else
            {
                cs.shooting = false;
            }
        }
    }
}
