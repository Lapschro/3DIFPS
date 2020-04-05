using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameChanger : MonoBehaviour
{

    public GameObject nameCanvas;

    private void OnTriggerEnter(Collider other)
    {
        nameCanvas.SetActive(true);
    }
}
