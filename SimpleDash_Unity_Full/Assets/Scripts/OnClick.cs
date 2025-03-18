using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class OnClick : MonoBehaviour
{
    public bool start;
    private Button button;
    private PlayerMove player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        button = GetComponent<Button>();
        if (start == true)
        {
            button.onClick.AddListener(GameStart);
        }
        else
        {
            button.onClick.AddListener(Restart);
        }
    }

    void GameStart()
    {
        player.gameStart();
    }
    void Restart()
    {
        player.RestartGame();
    }

}
