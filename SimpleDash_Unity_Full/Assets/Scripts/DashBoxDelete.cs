using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBoxDelete : MonoBehaviour
{
    public GameObject Player;
    private PlayerMove PlayerScript;
    private int Wait = 80;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerScript = Player.GetComponent<PlayerMove>();
        transform.Translate(Vector3.forward * 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerScript.Pause)
        {
            if (Wait == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                Wait -= 1;
            }
        }
    }
}
