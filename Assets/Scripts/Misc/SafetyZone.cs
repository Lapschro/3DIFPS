using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyZone : MonoBehaviour
{
    public float growthRate;
    public float minSize;
    public float maxSize;

    //private Vector3 growthVec;

    // Start is called before the first frame update
    void Start()
    {
        //growthVec = new Vector3(growthRate, growthRate, growthRate);
    }

    // Update is called once per frame
    void Update()
    {
        float newScale = Mathf.Clamp(gameObject.transform.localScale.x + growthRate, minSize, maxSize);
        gameObject.transform.localScale = new Vector3(newScale, gameObject.transform.localScale.y, newScale);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered ring");
        Energy e = other.gameObject.GetComponent<Energy>();
        if (e != null)
        {
            e.insideSafetyZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit ring");
        Energy e = other.gameObject.GetComponent<Energy>();
        if (e != null)
        {
            e.insideSafetyZone = false;
        }
    }


}
