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

    //Button m_Bt;

    // Start is called before the first frame update
    void Start()
    {
        //m_Bt = this.transform.GetComponent<Button>();
        //m_Bt.onClick.AddListener(fClick);

        EquipmentObject = GameObject.Find("EquipmentObject");
        EqScript = EquipmentObject.GetComponent<Equipment_Control>();
        nowButton.onClick.AddListener(fClick);

        if(EqScript.skill_state[num-1]==1)
        {
            nowButton.interactable = false;
        }
    }



    void fClick()
    {
        EqScript.updateSkillState(num, 1);
        Debug.Log("buy skill "+num.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (EqScript.skill_state[num - 1] == 1)
        {
            nowButton.interactable = false;
        }
    }
}
