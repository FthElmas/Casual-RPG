using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{

    public class Portal : MonoBehaviour
    {
        private int currentSceneIndex;
        
        private void Start()
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
        }
    }
}
