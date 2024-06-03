using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void ChangeGravity(bool _bool)
    {
        if(_bool)
        {
            Physics2D.gravity = new Vector2(0, -10f);
        }
        else
        {
            Physics2D.gravity = Vector2.zero;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("main Scene");
    }
}
