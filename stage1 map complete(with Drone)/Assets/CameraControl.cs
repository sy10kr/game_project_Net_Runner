using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Camera main;
    [SerializeField] Camera sub;

    //control 0 = 플레이어, 나머진 기타.
    public int control = 0;
    public int hp = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        main.enabled = true;
        sub.enabled = false;
    }
    public void SetMain()
    {
        main.enabled = true;
        sub.enabled = false;
    }
    public void SetSub()
    {
        main.enabled = false;
        sub.enabled = true;
    }

}
