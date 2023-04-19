using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;


namespace RPG.Core
{

    public class CinematicControlRemover : MonoBehaviour
    {
        private GameObject player;
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            player = GameObject.FindWithTag("Player");
        }
        private void DisableControl(PlayableDirector pd)
        {
            
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        private void EnableControl(PlayableDirector pd)
        {
            print("continue");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }

}
