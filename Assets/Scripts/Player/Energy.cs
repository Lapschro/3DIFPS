using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float energy = 100;
    public Renderer objectRenderer;
    public PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(energy > 10) {
            objectRenderer.enabled = false;
        }
        else {
            objectRenderer.enabled = true;
        }

        if (player.moving) {
            energy += 2f * Time.deltaTime;
        }
        else {
            energy -= 4f * Time.deltaTime;
        }
        energy = Mathf.Clamp(energy, 0, 100);
    }
}
