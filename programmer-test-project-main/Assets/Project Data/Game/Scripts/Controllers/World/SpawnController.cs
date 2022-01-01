using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnControl
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] Vector2 blockDistanceRange = new Vector2(1f, 3f);
        [SerializeField] Vector2 platformZScaleRange = new Vector2(0.5f, 6f);
        [SerializeField] Vector2 platformYposRange = new Vector2(-3f, 3f);
        [SerializeField] private float defaultBlockLenght = 4;

        public GameObject[] RoadBlockPrefabs;
        public GameObject prefabFinish;
        public GameObject prefabCoin;

        private int blocksCount = 8;
        private float CurrentZpos;

        List<GameObject> CurrentBlocks = new List<GameObject>();

        public void CreatePlatforms()
        {
            for (int i = 0; i < blocksCount; i++)
            {
                SpawnBlock(i);
            }

            SpawnFinish();
        }

        public void CreateCoins()
        {
            SpawnCoin();
        }



        void SpawnBlock(int i)
        {
            float BlockDistance = i == 0 ? 0 : Random.Range(blockDistanceRange.x, blockDistanceRange.y);
            float platformZScale =
                i == 0 ? platformZScaleRange.y : Random.Range(platformZScaleRange.x, platformZScaleRange.y);
            float platformYpos = i == 0 ? 0 : Random.Range(platformYposRange.x, platformYposRange.y);

            CurrentZpos += BlockDistance;
            CurrentZpos += i == 0 ? 0 : defaultBlockLenght * platformZScale;
            Vector3 blockPos = new Vector3(0, platformYpos, CurrentZpos);


            GameObject block = Instantiate(RoadBlockPrefabs[Random.Range(0, RoadBlockPrefabs.Length)], blockPos,
                Quaternion.identity, transform);

            block.transform.localScale = new Vector3(1, 1, platformZScale);

            CurrentBlocks.Add(block);
        }


        void SpawnFinish()
        {
            Vector3 parentScale = CurrentBlocks[CurrentBlocks.Count - 1].transform.localScale;

            GameObject finish = Instantiate(prefabFinish, CurrentBlocks[CurrentBlocks.Count - 1].transform);

            finish.transform.localScale = new Vector3(1f / parentScale.z, 1f / parentScale.y, 1f / parentScale.x);
        }


        void SpawnCoin()
        {

            for (int i = 0; i < CurrentBlocks.Count; i++)
            {
                var obj = CurrentBlocks[i];
                if (i > 0 && i < blocksCount - 1)
                {
                    GameObject coins = Instantiate(prefabCoin, obj.transform);

                    GameObject coins1 = Instantiate(prefabCoin, obj.transform);
                    Vector3 pos1 = coins1.transform.position;
                    pos1.z += 1;
                    coins1.transform.position = pos1;

                    GameObject coins2 = Instantiate(prefabCoin, obj.transform);
                    Vector3 pos2 = coins2.transform.position;
                    pos2.z -= 1;
                    coins2.transform.position = pos2;
                }
            }

        }
    }
}