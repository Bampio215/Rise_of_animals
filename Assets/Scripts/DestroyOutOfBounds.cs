using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update
    private float topBound = 30;

    private float lowBound = -10;

    private Health health;
    public GameObject projectilePrefab;
    public int hard = 0;
    void Start()
    {
        LoadPlayerData();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            health = player.GetComponent<Health>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        else
        {
            if (transform.position.z < lowBound || transform.position.x < 3 * lowBound || transform.position.x > -3 * lowBound)
            {
                if (CompareTag("Boss")) // Kiểm tra nếu đối tượng có tag là "Boss"
                {
                    health.TakeDamage(health.maxHealth);
                    gameObject.SetActive(false);

                }
                else
                {
                    if (health != null)
                    {
                        health.TakeDamage((int)Mathf.Pow(2, hard));
                        Destroy(gameObject);
                    }
                }

            }
        }
    }
    void LoadPlayerData()
    {
        // Kiểm tra nếu dữ liệu tồn tại trong PlayerPrefs
        if (PlayerPrefs.HasKey("Hard"))
        {
            hard = PlayerPrefs.GetInt("Hard");
        }

    }

}
