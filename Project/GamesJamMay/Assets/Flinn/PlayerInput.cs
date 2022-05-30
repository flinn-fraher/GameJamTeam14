using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;


namespace Flinn
{
    public class PlayerInput : MonoBehaviour, IInitialisable
    {
        [SerializeField] private CinemachineFreeLook _cinemachineComp;
        
        [SerializeField] private Transform _cameraArm;
        
        [SerializeField] private Transform _sphereMesh;

        [SerializeField] private PhysicMaterial _spherePhysicsMaterial;


        private Rigidbody _rb;

        private List<Rigidbody> _rigidbodies;
    
        private PlayerController _playerController;

        private bool bAiming = false;

        
        [SerializeField] private GameObject[] spherePoints;

        IEnumerator getsphereRigidBodies()
        {
            yield return new WaitForSeconds(0.1f);
            //spherePoints = GameObject.FindGameObjectsWithTag("SpherePoints");
            foreach (var point in spherePoints)
            {
                Rigidbody rbody = point.GetComponent<Rigidbody>();
                if (rbody)
                {
                    _rigidbodies.Add(rbody);
                    rbody.GetComponent<Collider>().material = _spherePhysicsMaterial;
                    
                }
            }
        }
        public void Init()
        {
            _rigidbodies = new List<Rigidbody>();
        
            Cursor.lockState = CursorLockMode.Locked;

            StartCoroutine((getsphereRigidBodies()));
            
            // _rb = GetComponentInChildren<Rigidbody>();
            // if (!_rb)
            // {
            //     Debug.LogError("Failed to find rigidbody on the player character");
            // }
            
            _playerController = GetComponent<PlayerController>();
            if (!_playerController)
            {
                Debug.LogError("Failed to find playerController on the player character");
            }
        }
        
        public float sensitivity = 5f;
        public float maxYAngle = 80f;
        private Vector2 _currentRotation;
     
        void Update()
        {
            // _currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            //
            // _currentRotation.y -= (bAiming ? 0 : Input.GetAxis("Mouse Y") * sensitivity);
            //
            // _currentRotation.x = Mathf.Repeat(_currentRotation.x, 360);
            // _currentRotation.y = Mathf.Clamp(_currentRotation.y, -maxYAngle, maxYAngle);
            //
            // _cameraArm.transform.rotation = Quaternion.Euler(_currentRotation.y,_currentRotation.x,0);
            //
            // _cameraArm.transform.position = _sphereMesh.transform.position;
        
            if (Input.GetMouseButton(0))
            {
                bAiming = true;
                StartCoroutine(MouseMovement());
                // LaunchCharacter();    
            }
        }

        IEnumerator MouseMovement()
        {
            float currentLaunchForce = 0;
            if (_cinemachineComp)
            {
                _cinemachineComp.m_YAxis.m_MaxSpeed = 0;
            }
            while (Input.GetMouseButton(0))
            {
                currentLaunchForce += Mathf.Abs(Input.GetAxis("Mouse Y"));
                
                //Debug.Log(currentLaunchForce);
                yield return null;
            }

            if (_cinemachineComp)
            {
                _cinemachineComp.m_YAxis.m_MaxSpeed = 2;
            }
            
            LaunchCharacter(currentLaunchForce);
            bAiming = false;
        }

        void LaunchCharacter(float launchForce)
        {
            Vector3 launchDirection = _cameraArm.forward;
            //float launchStrength = Random.Range(10, 100);
            
            Vector3 targetLocation = (launchDirection * (launchForce + Random.Range(0f, 5f))) + transform.position;
            
           // targetLocation.Scale(Vector3.up * launchForce);
            
            Vector3 launchVelocity = _playerController.GetLaunchVelocity(transform.position, targetLocation, launchForce, false);

            Debug.Log("adding force");
            
            foreach (var rbody in _rigidbodies)
            {
                if (rbody)
                {
                    Debug.Log(launchVelocity);
                    rbody.AddForce(launchVelocity);
                }
            }

        }
    
    }
}
