using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRing : MonoBehaviour
{
    public float growthRate;
    public float minSize;
    public float maxSize;
    public float drainRate;

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

    private void OnTriggerStay(Collider other)
    {
        Energy e = other.gameObject.GetComponent<Energy>();
        if (e != null)
        {
            e.energy = Mathf.Clamp(e.energy - 20.0f * Time.deltaTime, 0.0f, 100.0f);
        }
    }
}
