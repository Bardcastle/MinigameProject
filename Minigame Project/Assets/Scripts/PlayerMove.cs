using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public bool Pause;
    public bool gameStarted;
    public GameObject DashBox;
    public GameObject Player;
    public GameObject Camera;
    private float MouseX;
    private float MouseY;
    private float DashLength = 10.0f;
    private float speed = 10.0f;
    private float TurnSpeed = 2.0f;
    private float ForwardInput;
    private float SideBySideInput;
    public bool dashing = false;
    private int DashWait = 0;
    private int DashCooldown = 0;
    private Vector3 Temp;
    private Vector3 WallCollision;
    private Vector3 Offset = new Vector3(0, 0, 5);
    public int PlayerHealth = 3;
    public int PlayerScore = 0;
    private bool GameOver = false;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI title;
    public Button startButton;
    public Button restartButton;
    public Image damageFlash;

    // Update is called once per frame
    void Update()
    {
        // Game Over
        if (PlayerHealth <= 0)
        {
            gameOver();
        }

        // Out of Bounds Detection & Correction
        if (Player.transform.position.x > 24.45f)
        {
            Player.transform.position = new Vector3(24.45f, Player.transform.position.y, Player.transform.position.z);
        }
        if (Player.transform.position.x < -24.45f)
        {
            Player.transform.position = new Vector3(-24.45f, Player.transform.position.y, Player.transform.position.z);
        }
        if (Player.transform.position.z > 24.45f)
        {
            Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 24.45f);
        }
        if (Player.transform.position.z < -24.45f)
        {
            Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -24.45f);
        }


        if (Pause == false && !GameOver && gameStarted)
        {
            if (dashing == false)
            {
                // Player Forward Movement
                ForwardInput = Input.GetAxis("Vertical");
                transform.Translate(Vector3.forward * Time.deltaTime * speed * ForwardInput);
                // Player Side Movement
                SideBySideInput = Input.GetAxis("Horizontal");
                transform.Translate(Vector3.right * Time.deltaTime * speed * SideBySideInput);
                // Player Rotation By Mouse 
                MouseX = Input.GetAxis("Mouse X");
                transform.Rotate(Vector3.up, TurnSpeed * MouseX);
                // Player Look up/Down By Mouse
                MouseY = Input.GetAxis("Mouse Y");
                Camera.transform.Rotate(Vector3.left, TurnSpeed * MouseY); 
                if (Camera.transform.rotation.x > 60)
                {
                    Camera.transform.Rotate(Vector3.right, TurnSpeed);
                }
                else if (Camera.transform.rotation.x < -60)
                {
                    Camera.transform.Rotate(Vector3.right, TurnSpeed);
                }
                // Player Dash Attack Start
                if (DashCooldown == 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        dashing = true;
                        DashWait = 0;
                        DashCooldown = 50;
                    }
                }
                else
                {
                    DashCooldown -= 1;
                }
            }
            else   // Dash Attack
            {
                if (DashWait == 0)
                {
                    // Spawn Dash Collider
                    Temp = transform.position + Offset;
                    Instantiate(DashBox, GameObject.Find("Player").transform.position, Player.transform.rotation);
                } 
                transform.Translate(Vector3.forward * DashLength / 10.0f);
                DashWait += 1;
                if (DashWait >= 10)
                {
                    dashing = false;
                }
            }
            // Pauses the Game
            if (Input.GetKeyDown("space"))
            {
                Pause = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else // Pause State
        {
            if (Input.GetKeyDown("space") && !GameOver)
            {
                Pause = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void gameStart()
    {
        Debug.Log("Game STarted");
        Cursor.lockState = CursorLockMode.Locked;
        Pause = false;
        gameStarted = true;
        title.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        healthText.gameObject.SetActive(true);
    }

    public void gameOver()
    {
        Pause = true;
        Cursor.lockState = CursorLockMode.None;
        GameOver = true;
        gameOverText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void scoreUpdate(int updateValue)
    {
        PlayerScore += updateValue;
        scoreText.text = "Score: " + PlayerScore;
    }

    public void healthUpdate(int updateValue)
    {
        StartCoroutine(Damaged(0.2f));
        PlayerHealth -= updateValue;
        if (PlayerHealth == 2)
        {
            healthText.text = "Health: ♥♥";
        }
        else if (PlayerHealth == 1)
        {
            healthText.text = "Health: ♥";
        }
        else if (PlayerHealth == 0)
        {
            healthText.text = "Health: ";
        }
    }
    IEnumerator Damaged(float i)
    {
        damageFlash.color = new Color(255,0,0,i);
        yield return new WaitForSeconds(0.25f);
        while (damageFlash.color.a > 0)
        {
            yield return new WaitForSeconds(0.1f);
            i -= 0.01f;
            damageFlash.color = new Color(255, 0, 0, i);
        }
    }
}
