using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoEnergy : MonoBehaviour {
    public float energy = 100;
    public Renderer objectRenderer;

    public TutoPlayer player;

    public float minimunEnergy;

    public bool insideSafetyZone = true;

    public bool isControllable;

    public Text energyText;

    // Update is called once per frame
    void Update() {
        Weapon weapon = player.playerWeapon ?? null;
        energyText.text = "" + (int)energy;
        if (!weapon) {
            return;
        }
        if (energy > minimunEnergy) {
            objectRenderer.enabled = false;

        }
        else {
            objectRenderer.enabled = true;
        }

        if (!insideSafetyZone) {
            energy -= 20f * Time.deltaTime;
        }
        else {
            if (player.moving) {
                energy += 2f * Time.deltaTime;
            }
            else {
                energy -= 4f * Time.deltaTime;
            }
        }

        energy = Mathf.Clamp(energy, 0, 100);

        energyText.text = "" + (int)energy;
    }

    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Zona") {
            Debug.Log("Enter ring");
            insideSafetyZone = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Zona") {
            Debug.Log("Exit ring");
            insideSafetyZone = false;
        }
    }
}
