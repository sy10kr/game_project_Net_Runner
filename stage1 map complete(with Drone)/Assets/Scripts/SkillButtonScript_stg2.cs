using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonScript_stg2 : MonoBehaviour
{
    public int num;
    private GameObject EquipmentObject;
    private Equipment_Control EqScript;



    public Button nowButton;



    //Button m_Bt;

    // Start is called before the first frame update
    void Start()
    {
        //m_Bt = this.transform.GetComponent<Button>();
        //m_Bt.onClick.AddListener(fClick);

        EquipmentObject = GameObject.Find("EquipmentObject");
        EqScript = EquipmentObject.GetComponent<Equipment_Control>();



        if (num == 9) // stage 선택의 경우
        {
            if(EqScript.stage_clear == 0)
            {
                nowButton.interactable = false;
            }
        }

    }



    // Update is called once per frame
    void Update()
    {
    }

}
