using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyMove : MonoBehaviour
{
    // ♥
    public GameObject Player;
    private PlayerMove PlayerScript;
    private float speed = 5.0f;
    public Vector3 Temp;

    // Update is called once per frame
    void Update()
    {
        if (!PlayerScript.Pause)
        {
            Temp = Player.transform.position;
            transform.LookAt(Player.transform.position);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

        }
    }
    private void Start()
    {
        Player = GameObject.Find("Player");
        PlayerScript = Player.GetComponent<PlayerMove>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DashBox"))
        {
            PlayerScript.scoreUpdate(1);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player") && PlayerScript.dashing == false)
        {
            PlayerScript.healthUpdate(1);
        }
    }
}
