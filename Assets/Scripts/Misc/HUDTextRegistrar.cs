using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDTextRegistrar : MonoBehaviour
{
    private HP playerHP;
    private Energy playerEnergy;

    [SerializeField]
    private HUDManager hudManager;

    private UnityEngine.UI.Text text;

    public enum ValueField { undefined, HP, Energy, Visibility };
    public ValueField valueField = ValueField.undefined;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
        playerHP = hudManager.player.GetComponent<HP>();
        playerEnergy = hudManager.player.GetComponent<Energy>();
        //playerVisible = hudManager.player.GetComponent<Energy>().
    }

    // Update is called once per frame
    void Update()
    {
        int value; // = 100_000_000;
        switch(valueField)
        {
            case ValueField.HP:
                value = (int) playerHP.hp;
                //text.text = ((int) playerHP.hp).ToString();
                break;
            case ValueField.Energy:
                value = (int)playerEnergy.energy;
                //text.text = ((int) playerEnergy.energy).ToString();
                break;
            default:
                value = 100_000_000;
                //text.text = "???";
                break;
        }

        if (value >= 10_000_000)
            text.text = "???";
        else
            text.text = value.ToString();
    }
}
