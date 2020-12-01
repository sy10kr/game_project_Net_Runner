using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheck : MonoBehaviour
{
    private GameObject EquipmentObject;
    private Equipment_Control EqScript;

    private GameObject skill_icon_1;
    private GameObject skill_icon_2;
    private GameObject skill_icon_3;
    private GameObject skill_icon_4;



    // Start is called before the first frame update
    void Start()
    {
        EquipmentObject = GameObject.Find("EquipmentObject");
        EqScript = EquipmentObject.GetComponent<Equipment_Control>();
        Debug.Log(EqScript.skill_state[0]);

        skill_icon_1 = GameObject.Find("Skill_Icon_1");
        skill_icon_2 = GameObject.Find("Skill_Icon_2");
        skill_icon_3 = GameObject.Find("Skill_Icon_3");
        skill_icon_4 = GameObject.Find("Skill_Icon_4");

        if(EqScript.skill_state[0]==0)
        {
            skill_icon_1.SetActiveRecursively(false);
        }
        if (EqScript.skill_state[1] == 0)
        {
            skill_icon_2.SetActiveRecursively(false);
        }
        if (EqScript.skill_state[2] == 0)
        {
            skill_icon_3.SetActiveRecursively(false);
        }
        if (EqScript.skill_state[3] == 0)
        {
            skill_icon_4.SetActiveRecursively(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
