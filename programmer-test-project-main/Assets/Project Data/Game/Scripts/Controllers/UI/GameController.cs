using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpawnControl;
using PlayerControl;

namespace GameControl
{
      public class GameController : MonoBehaviour
      {
            public static GameController Instance;
      
      
            [SerializeField] private SpawnController spawnController;
            [SerializeField] private UIController uiController;
            [SerializeField] private SpawnEnvironment spawnEnvironment;
            [SerializeField] private PlayerController playerController;

            private void Awake()
            {
                  Instance = this;
            }


            private void Start()
            {
                  uiController.StartCounter();
            
                  spawnController.CreatePlatforms();
                  spawnController.CreateCoins();
                  spawnEnvironment.CreateEnvironment();
                  StartCoroutine(StartRoutine());
            }
      
            private IEnumerator StartRoutine()
            {
                  yield return new WaitForSeconds(3f);
           
                  playerController.MoveCharacter(true);
            }

            public void Win()
            {
                  uiController.ActivateWinButton();
            }
      }
}
