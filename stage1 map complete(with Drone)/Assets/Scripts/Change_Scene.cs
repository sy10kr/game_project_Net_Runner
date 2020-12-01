using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene : MonoBehaviour
{


    
    // Start is called before the first frame update
    public void SceneChange()
    {
        SceneManager.LoadScene("scene_stage1");
    }

    public void SceneChange_2()
    {
        SceneManager.LoadScene("scene_stage2");
    }

    public void SceneChange_game_over()
    {
        SceneManager.LoadScene("game_over");
    }

    public void SceneChange_game_clear()
    {
        SceneManager.LoadScene("game_clear");
    }

    public void SceneChange_main()
    {
        SceneManager.LoadScene("scene_mainUI");
    }




    // Update is called once per frame
    void Update()
    {

    }
}
