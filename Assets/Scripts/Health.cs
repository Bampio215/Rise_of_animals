using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth; // Máu tối đa
    public int currentHealth; // Máu hiện tại
    public HealthBar healthBar; // Tham chiếu đến thanh máu
    public bool isPlayer;
    public int strength = 1;
    public int health = 0;
    public int hardMax = 0;
    public int hard = 0;
    private Property playerProperty;
    AudioManager audioManager;
    private ExpController expController;
    public GameObject projectilePrefab;
    public int ExpCreep;
    void Start()
    {
        LoadPlayerData();
        expController = Object.FindFirstObjectByType<ExpController>();
        if (isPlayer)
        {
            maxHealth = health;
            currentHealth = maxHealth;
        }
        else
        {
            maxHealth = maxHealth * (int)Mathf.Pow(2, hard);

            currentHealth = maxHealth;

            ExpCreep = ExpCreep * (hard + 1);
        }
        if (healthBar != null)
        {
            healthBar.targetObject = gameObject; // Cập nhật đối tượng mục tiêu
            healthBar.SetHealth(currentHealth, maxHealth); // Cập nhật thanh máu
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth); // Cập nhật thanh máu
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            TakeDamage(strength);
        }
    }


    void Die()
    {
        // Kiểm tra nếu đối tượng là Boss
        if (CompareTag("Boss"))
        {
            // Đóng băng thời gian
            Time.timeScale = 0;

            if (hard == hardMax)
            {
                hardMax++;
                PlayerPrefs.SetInt("HardMax", hardMax);
            }

            expController.CurrentEXP += 3 * ExpCreep;
            PlayerPrefs.Save();
            gameObject.SetActive(false);

        }
        else
        {
            if (CompareTag("Player"))
            {
                Time.timeScale = 0;

            }
            else
            {
                expController.CurrentEXP += ExpCreep;
                if (healthBar != null)
                {

                    Destroy(healthBar.gameObject);
                    healthBar = null;
                }
                Destroy(gameObject);
            }
        }

    }
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    void LoadPlayerData()
    {
        if (PlayerPrefs.HasKey("Strength"))
        {
            strength = PlayerPrefs.GetInt("Strength");
        }
        if (PlayerPrefs.HasKey("Health"))
        {
            health = PlayerPrefs.GetInt("Health");
        }
        if (PlayerPrefs.HasKey("Hard"))
        {
            hard = PlayerPrefs.GetInt("Hard");
        }
        if (PlayerPrefs.HasKey("HardMax"))
        {
            hardMax = PlayerPrefs.GetInt("HardMax");
        }

    }
}
