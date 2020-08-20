using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commandManagement : MonoBehaviour
{
    public string command;

    void Start()
    {
        command = "";
    }

    public void SetCommand(string com)
    {
        command = com;
    }
}
