using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EquipCheck : MonoBehaviour
{
    private GameObject EquipmentObject;
    private Equipment_Control EqScript;

    public Text MoneyState;
    public Text ZombieState;
    public Text BatteryState;




    // Start is called before the first frame update
    void Start()
    {
        EquipmentObject = GameObject.Find("EquipmentObject");


        EqScript = EquipmentObject.GetComponent<Equipment_Control>();
        Debug.Log(EqScript.skill_state[0]);

        
        int money = EqScript.money;
        MoneyState.text = money.ToString();
        ZombieState.text = EqScript.zombie_level.ToString();
        BatteryState.text = EqScript.battery_level.ToString();
        //resourceText.text = "Enter Your Text Here";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
