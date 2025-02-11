using System.Collections;
using UnityEngine;
using TMPro;
public class GameOver : MonoBehaviour
{
    private GameObject boss;
    private GameObject player;
    private Health bossHealth;
    private Health playerHealth;
    public TextMeshProUGUI textOver;
    public TextMeshProUGUI Health;

    public GameObject Over;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        StartCoroutine(WaitAndFindBoss());
    }

    IEnumerator WaitAndFindBoss()
    {
        yield return new WaitForSeconds(30f);

        boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss != null)
        {
            // Lấy component Health từ đối tượng Boss
            bossHealth = boss.GetComponent<Health>(); // Dùng GetComponent trên boss
            if (bossHealth == null)
            {
                Debug.Log("Không thể tìm thấy component Health trên Boss.");
            }
        }
        else
        {
            Debug.Log("Không thể tìm thấy Boss.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Health.text = $"{playerHealth.currentHealth}/{playerHealth.maxHealth}";
        if (player != null && playerHealth != null)
        {
            if (playerHealth.currentHealth <= 0)
            {
                Over.SetActive(true);
                textOver.text = "Notification: You have been defeated. Health reached zero";
            }
        }
        if (boss != null && bossHealth != null)
        {
            if (bossHealth.currentHealth <= 0)
            {
                Over.SetActive(true);
                textOver.text = "Notification: The strongest beast has been tamed, a new level has been unlocked.";
            }
        }
    }
}
