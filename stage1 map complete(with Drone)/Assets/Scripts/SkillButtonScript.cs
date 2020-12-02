using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonScript : MonoBehaviour
{
    public int num;
    private GameObject EquipmentObject;
    private Equipment_Control EqScript;

    public Button nowButton;

    public Button Battery_Button;

    public Button skill_1_Button;
    public Button skill_2_Button;

    //Button m_Bt;

    // Start is called before the first frame update
    void Start()
    {
        //m_Bt = this.transform.GetComponent<Button>();
        //m_Bt.onClick.AddListener(fClick);

        EquipmentObject = GameObject.Find("EquipmentObject");
        EqScript = EquipmentObject.GetComponent<Equipment_Control>();
        nowButton.onClick.AddListener(fClick);
        Battery_Button.onClick.AddListener(batteryClick);

        if (num == 9) // stage 선택의 경우
        {
            if(EqScript.stage_clear == 0)
            {
                nowButton.interactable = false;
            }
        }
        else if(EqScript.skill_state[num-1]==1)
        {
            nowButton.interactable = false;
        }
    }

    void batteryClick()
    {
        Debug.Log("Battery upgrade " + num.ToString());
        EqScript.battery_level = EqScript.battery_level + 1;
        EqScript.money = EqScript.money - 50;
    }

        void fClick()
    {
        
        Debug.Log("buy skill "+num.ToString());
        if (num == 3)
        {
            EqScript.updateSkillState(num, 1);
            EqScript.money = EqScript.money - 50;
        }


        if (num == 4)
        {
            EqScript.updateSkillState(num, 1);
            EqScript.money = EqScript.money - 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EqScript.money < 100)
        {
            skill_2_Button.interactable = false;
            if (EqScript.money < 50)
            {
                skill_1_Button.interactable = false;
                Battery_Button.interactable = false;
            }
        }

        if (num == 9) // stage 선택의 경우
        {
            if (EqScript.stage_clear == 0)
            {
                nowButton.interactable = false;
            }
        }
        else if (EqScript.skill_state[num - 1] == 1)
        {
            nowButton.interactable = false;
        }
    }
}
