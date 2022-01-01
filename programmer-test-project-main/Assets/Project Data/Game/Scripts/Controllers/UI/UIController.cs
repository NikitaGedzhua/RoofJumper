using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameControl
{
   public class UIController : MonoBehaviour
   {
      [SerializeField] private Text startCountText;
      [SerializeField] private GameObject WinButton;
      [SerializeField] private Text startHoldText;
   
   
      public void StartCounter()
      {
         StartCoroutine(StartRoutine());
      }
   
      public void ActivateWinButton()
      {
         WinButton.SetActive(true);
      }
   
      
   
      private IEnumerator StartRoutine()
      {
         WaitForSeconds delay = new WaitForSeconds(1f);
   
         startCountText.text = 3.ToString();
         yield return delay;
         startCountText.text = 2.ToString();
         yield return delay;
         startCountText.text = 1.ToString();
         yield return delay;
         startCountText.gameObject.SetActive(false);
         startHoldText.gameObject.SetActive(false);
      }
   
      public void RestartLvl()
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
   }
}
