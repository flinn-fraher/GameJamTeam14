using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    // Start is called before the first frame update

    public
    Transform StartPos;
    [SerializeField]
    CarEndTrigger End;

    void Start()
    {
        End.PassSelf(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
