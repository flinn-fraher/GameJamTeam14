using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    bool isOpen=false;
    [SerializeField]
    bool isMuted = false;


    [SerializeField]
    List<Sprite> Icons = new List<Sprite>();

    [SerializeField]
    GameObject AudioSwitch;

    [SerializeField]
    GameObject Info;

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

    public void Mute()
    {
        if (isMuted)
        {
            isMuted = false;
            AudioSwitch.GetComponent<Image>().sprite = Icons[0];
        }
        else
        {
            isMuted = true;
            AudioSwitch.GetComponent<Image>().sprite = Icons[1];
        }
    }

    public void ChangeScreenSize()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OpenH2PMenu()
    {
        Info.SetActive(true);
    }
    public void CloseH2PMenu()
    {
        Info.SetActive(false);
    }

}
