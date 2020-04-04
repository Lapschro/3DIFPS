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

    public enum ValueField { undefined, HP, Energy };
    public ValueField valueField = ValueField.undefined;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
        playerHP = hudManager.player.GetComponent<HP>();
        playerEnergy = hudManager.player.GetComponent<Energy>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(valueField)
        {
            case ValueField.HP:
                text.text = ((int) playerHP.hp).ToString();
                break;
            case ValueField.Energy:
                text.text = ((int) playerEnergy.energy).ToString();
                break;
            default:
                text.text = "???";
                break;
        }
        
    }
}
