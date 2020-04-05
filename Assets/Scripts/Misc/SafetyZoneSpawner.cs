using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyZoneSpawner : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text countdownText;
    float startTime;
    float remainingTime;

    public float timeToActivate;
    public float timeToDeactivate;
    public GameObject safetyZonePrefab;

    private float countdownTime;
    private GameObject safetyZoneActive;

    // Start is called before the first frame update
    void Start()
    {
        //startTime = Time.realtimeSinceStartup;
        ResetTimer(timeToActivate);
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime = Mathf.Clamp(countdownTime - (Time.realtimeSinceStartup - startTime), 0.0f, countdownTime);
        countdownText.text = ((int)remainingTime).ToString("D2");

        if (remainingTime == 0.0f)
        {
            if (safetyZoneActive == null)
                SpawnSafetyZone();
            else
                DespawnSafetyZone();
        }
    }

    private void ResetTimer(float countdown)
    {
        startTime = Time.realtimeSinceStartup;
        countdownTime = countdown;
    }
    void SpawnSafetyZone()
    {
        float x = Random.Range(-10.0f, 10.0f);
        float z = Random.Range(-10.0f, 10.0f);
        safetyZoneActive = Instantiate(safetyZonePrefab, new Vector3(x, 0.0f, z), Quaternion.identity);
        ResetTimer(timeToDeactivate);
    }

    void DespawnSafetyZone()
    {
        Destroy(safetyZoneActive);
        safetyZoneActive = null;
        ResetTimer(timeToActivate);
    }
}
