using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEndTrigger : MonoBehaviour
{
    private CarManager cM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PassSelf(CarManager _cM)
    {
        cM = _cM;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            other.gameObject.transform.position = cM.StartPos.transform.position;

        }
    }
}
