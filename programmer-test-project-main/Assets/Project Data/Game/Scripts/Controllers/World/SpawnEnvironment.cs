using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnControl
{
    public class SpawnEnvironment : MonoBehaviour
    {
        public GameObject[] BackgPrefabs;
       
        private float CurrentZpos;
        private int BackgCount = 17;
        
        public void CreateEnvironment()
        {
            for (int i = 0; i < BackgCount; i++)
            {
                SpawnBackg();
            }
        }
        
        void SpawnBackg()
        {
            float BlockDistance = Random.Range(10,10);
            float platformYpos = Random.Range(-5, 10);
    
            CurrentZpos += BlockDistance;
            Quaternion rotate = Quaternion.Euler(0,-90,0);
            Vector3 blockPos = new Vector3(-2.5f,platformYpos, CurrentZpos);
    
            GameObject backg = Instantiate(BackgPrefabs[Random.Range(0, BackgPrefabs.Length)], blockPos, rotate,transform);
            
        }
    }

}

