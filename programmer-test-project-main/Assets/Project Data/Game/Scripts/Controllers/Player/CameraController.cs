using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameControl
{
    public class CameraController : MonoBehaviour
    {
        public Transform Target;

        private Vector3 startDistance, moveVec;

        void Start()
        {
            startDistance = transform.position - Target.position;
        }

        private void Update()
        {
            moveVec = Target.position + startDistance;
        
            moveVec.y = startDistance.y;

            transform.position = moveVec;
        }
    }

}
