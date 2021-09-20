using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.SceneManagements;
using UnityEngine.AI;
namespace SceneManagements
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] float fadeOutTime=0.5f;
        [SerializeField] float fadeInTime=0.5f;
        [SerializeField] float fadeWaitTime = 1f;
        enum DestinationIdentifier
        {
            A,B,C,D,E
        }
        [SerializeField] Transform spawnPoint;
        [SerializeField] int SceneToLoad = -1;
        [SerializeField] DestinationIdentifier destination;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if(SceneToLoad<0)
            {
                Debug.Log("scene to load not set");
                yield break;
            }
            Fader fader = FindObjectOfType<Fader>();
            DontDestroyOnLoad(gameObject);
            
            yield return fader.fadeOut(fadeOutTime);
            print("Loaded scene");
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();
            yield return SceneManager.LoadSceneAsync(SceneToLoad);
            wrapper.Load();
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.fadeIn(fadeInTime);
            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) continue;
                if(destination != portal.destination) continue;
                return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;            
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;            
        }
    }

}
