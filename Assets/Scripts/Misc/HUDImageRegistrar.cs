using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDImageRegistrar : MonoBehaviour
{
    private HP playerHP;
    private Energy playerEnergy;
    public Sprite sprite0;
    public Sprite sprite1;

    [SerializeField]
    private HUDManager hudManager;

    private UnityEngine.UI.Image image;

    public enum ValueField { undefined, HP, Energy, Visibility };
    public ValueField valueField = ValueField.undefined;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
        playerHP = hudManager.player.GetComponent<HP>();
        playerEnergy = hudManager.player.GetComponent<Energy>();
    }

    // Update is called once per frame
    void Update()
    {
        bool invisible;
        switch(valueField)
        {
            case ValueField.Visibility:
                invisible = playerEnergy.isInvisible;
                break;
            default:
                invisible = true;
                break;
        }

        if (invisible)
            image.sprite = sprite0;
        else
            image.sprite = sprite1;
    }
}
