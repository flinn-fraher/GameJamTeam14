using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Flinn
{
    public interface IInitialisable
    {
        public void Init();
    }

    public class PlayerController : MonoBehaviour, IInitialisable
    {
        public Vector3 GetLaunchVelocity(Vector3 startPos, Vector3 targetPos, float speed, bool highArc)
        {
            Vector3 launchVelocity = Vector3.zero;
            Vector3 deltaPosition = targetPos - startPos;
            Vector3 horizontalDeltaPos = new Vector3(deltaPosition.x, 0, deltaPosition.z);
            Vector3 rotatedDeltaPosition = new Vector3(horizontalDeltaPos.magnitude, deltaPosition.y, 0);
            float angle1 = (180.0f / Mathf.PI) * Mathf.Atan((speed * speed + Mathf.Sqrt(Mathf.Pow(speed, 4) - Physics.gravity.magnitude * (Physics.gravity.magnitude * rotatedDeltaPosition.x * rotatedDeltaPosition.x + 2 * rotatedDeltaPosition.y * speed * speed))) / (Physics.gravity.magnitude * rotatedDeltaPosition.x));
            float angle2 = (180.0f / Mathf.PI) * Mathf.Atan((speed * speed - Mathf.Sqrt(Mathf.Pow(speed, 4) - Physics.gravity.magnitude * (Physics.gravity.magnitude * rotatedDeltaPosition.x * rotatedDeltaPosition.x + 2 * rotatedDeltaPosition.y * speed * speed))) / (Physics.gravity.magnitude * rotatedDeltaPosition.x));
            float minAngle = Mathf.Min(angle1, angle2);
            float maxAngle = Mathf.Max(angle1, angle2);
            float angle = highArc ? maxAngle : minAngle;
            if (!float.IsNaN(angle))
            {
                launchVelocity = Quaternion.AngleAxis(angle, Vector3.Cross(horizontalDeltaPos, Vector3.up)) * horizontalDeltaPos.normalized * speed;
            }
            return launchVelocity;
        }
        
        [SerializeField]
        private float _virusStrength = 1;
        
        [SerializeField]
        private float _strengthLossPerMove = .1f;
        
        [SerializeField]
        private float _strengthLossPerTick = .05f;

        [SerializeField] private float _strengthLossTickRate = .1f;

        [SerializeField] private bool _bShouldTickStrengthLoss = true;
        
        
        IEnumerator StrengthLoss()
        {
            while (_bShouldTickStrengthLoss)
            {
                //decrease the virus strength
                _virusStrength = _virusStrength - _strengthLossPerTick;
                //Debug.Log(message: _virusStrength);
                yield return new WaitForSeconds(_strengthLossTickRate);
            }
        }

        public void Init()
        {
            StartCoroutine(StrengthLoss());
        }
    }
}
