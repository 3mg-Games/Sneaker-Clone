using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Core
{
    public class SectionUnlocker : MonoBehaviour
    {
        public Animator CameraAnimation;
        public float cameraTransitionSpeed;

        public List<GameObject> LockedSections = new List<GameObject>();
        public List<GameObject> UnlockSection = new List<GameObject>();
        public List<int> UnlockLevel = new List<int>();
        public List<bool> Unlocked = new List<bool>();

        GameManager gm;
        private void Start()
        {
            gm = GetComponent<GameManager>();
            unlockSection();
        }

        public void unlockSection()
        {
            StartCoroutine(unlock(cameraTransitionSpeed));
        }
        IEnumerator unlock(float t)
        {
            if (gm.Level >= UnlockLevel[0] && !Unlocked[0])
            {
                gm.GameplayPause = true;
                CameraAnimation.Play("Second U");
                yield return new WaitForSeconds(t);
                LockedSections[0].SetActive(false);
                UnlockSection[0].SetActive(true);
                Unlocked[0] = true;

            }

            if (gm.Level >= UnlockLevel[1] && !Unlocked[1])
            {
                gm.GameplayPause = true;
                CameraAnimation.Play("Third U");
                yield return new WaitForSeconds(t);
                LockedSections[1].SetActive(false);
                UnlockSection[1].SetActive(true);
                Unlocked[1] = true;
            }
        }
    }
}
