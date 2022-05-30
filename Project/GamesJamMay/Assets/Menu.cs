using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    bool isOpen=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OpenOptions()
    {
        if (isOpen)
        {
            isOpen = false;
            anim.SetTrigger("Close");
        }
        else
        {
            anim.SetTrigger("Open");
            isOpen = true;
        }

    }
}
