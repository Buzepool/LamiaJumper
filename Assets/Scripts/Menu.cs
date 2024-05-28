using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string levelName;

    public void Jogar()
    {
        SceneManager.LoadScene(levelName);
    }

    // Update is called once per frame
    public void Sair()
    {
        Application.Quit();
    }
}
