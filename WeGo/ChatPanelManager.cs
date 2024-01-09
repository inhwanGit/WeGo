using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanelManager : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject ReStartButton;
    
    public void ButtonActive()
    {
        bool IsActive = StartButton.activeSelf;

        StartButton.SetActive(!IsActive);
        ReStartButton.SetActive(!IsActive);
    }
}
