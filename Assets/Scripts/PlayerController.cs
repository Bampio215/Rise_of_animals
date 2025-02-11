using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 10.0f;
    public int cd = 0;
    private float xRange = 20;
    public AudioManager audioManager;
    // Thay đổi để lấy dữ liệu từ PlayerPrefs
    public GameObject projectilePrefab;
    private bool delaySpace = false;

    Animator animator;

    void Start()
    {
        // Không cần tạo đối tượng Property nữa
        // Thay vào đó, lấy các giá trị từ PlayerPrefs
        LoadPlayerData();

        transform.rotation = Quaternion.Euler(0, 0, 0);
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed_f", 1);
        animator.SetBool("Static_b", true);
    }

    void Update()
    {

        transform.position = new Vector3(transform.position.x, 0, 0);
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        // Di chuyển trái phải
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            animator.SetFloat("Speed_f", 1);
            animator.SetBool("Static_b", false);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            animator.SetFloat("Speed_f", 1);
            animator.SetBool("Static_b", false);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("Static_b", true);
        }

        // Tạo đạn khi nhấn space
        if (Input.GetKeyDown(KeyCode.Space) && !delaySpace)
        {
            audioManager.playSFX(audioManager.gunshot);

            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            delaySpace = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("IsATK", true);
            StartCoroutine(Reset());
        }
        else
        {
            animator.SetBool("IsATK", false);
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1.0f - 1.0f * (float)cd / 100);
        delaySpace = false;
    }

    // Hàm tải dữ liệu người chơi từ PlayerPrefs
    void LoadPlayerData()
    {
        // Kiểm tra nếu dữ liệu tồn tại trong PlayerPrefs
        if (PlayerPrefs.HasKey("Speed"))
        {
            speed = PlayerPrefs.GetInt("Speed");
        }
        // Nếu không có, khởi tạo giá trị mặc định
        else
        {
            speed = 0f; // Giá trị mặc định
        }

        if (PlayerPrefs.HasKey("Cooldown"))
        {
            cd = PlayerPrefs.GetInt("Cooldown");
        }
    }
}
