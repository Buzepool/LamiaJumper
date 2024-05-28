using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    
    //Tudo só pra ativar o painel de gameover e restartar a fase

    public static GameController instance;

    //Lógica de tela de gameOver

    public GameObject gameOver;
    public GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ShowGameOver()
    {
        gameOver.SetActive(true);
        gameOverPanel.SetActive(true);
    }
    public void RestartGame(string lvlname)
    {
        SceneManager.LoadScene(lvlname);
    }
}
