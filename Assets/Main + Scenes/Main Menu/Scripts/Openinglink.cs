using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openinglink : MonoBehaviour
{
    public string link;

    public void OpenLink()
    {
        Application.OpenURL(link);
    }   
}
