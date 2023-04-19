using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{

    public class Portal : MonoBehaviour
    {
        [SerializeField] private int currentSceneIndex;
        [SerializeField] private Transform portalSpawnPoint;
        [SerializeField] private DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;
        public enum DestinationIdentifier
        {
            A,
            B,
            C,
            D,
            E
        }
        
        private void Start()
        {
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                StartCoroutine(SceneLoader());
            }
        }

        private IEnumerator SceneLoader()
        {

            if(currentSceneIndex < 0)
            {
                yield break;
            }
            DontDestroyOnLoad(gameObject);

            UIFader uiFader = FindObjectOfType<UIFader>();

            yield return uiFader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(currentSceneIndex);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);
            yield return uiFader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.portalSpawnPoint.position);
            player.transform.rotation = otherPortal.portalSpawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) continue;

                if(portal.destination != destination) continue;

                return portal;
            }
            return null;
        }
    }
}
