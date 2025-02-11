using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    public float rotationSpeed = 6000f; // Tốc độ xoay
    private float currentRotation = 0f; // Góc xoay hiện tại
    private float initialRotation; // Góc quay ban đầu
    private bool isReturning = false; // Cờ kiểm tra nếu đang quay về góc ban đầu
    private bool isRotating = false; // Cờ kiểm tra xem chuột có đang được nhấn hay không
    private float lastMouseX = 0f; // Lưu trữ vị trí chuột trước khi di chuyển

    void Start()
    {
        // Lưu lại góc quay ban đầu của nhân vật
        initialRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        // Khi bắt đầu nhấn chuột, lưu lại vị trí chuột
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            lastMouseX = Input.mousePosition.x; // Lưu vị trí chuột lúc bắt đầu
        }

        // Khi chuột trái đang được nhấn và kéo
        if (isRotating && Input.GetMouseButton(0))
        {
            // Lấy sự thay đổi vị trí chuột
            float mouseDeltaX = Input.mousePosition.x - lastMouseX;

            // Cập nhật góc xoay dựa trên sự di chuyển của chuột
            float rotationAmount = mouseDeltaX * rotationSpeed * Time.deltaTime;
            currentRotation += rotationAmount;

            // Giới hạn góc xoay trong phạm vi -180 đến 180 độ
            if (currentRotation > 180f)
                currentRotation -= 360f;
            else if (currentRotation < -180f)
                currentRotation += 360f;

            // Cập nhật góc quay của nhân vật
            transform.rotation = Quaternion.Euler(0, currentRotation, 0);

            // Lưu lại vị trí chuột hiện tại để tính delta cho lần di chuyển tiếp theo
            lastMouseX = Input.mousePosition.x;
        }

        // Nếu chuột đã được thả ra, bắt đầu quay về vị trí ban đầu
        if (Input.GetMouseButtonUp(0))
        {
            isReturning = true; // Bật cờ quay về góc ban đầu
        }

        // Nếu đang quay về góc ban đầu
        if (isReturning)
        {
            float returnSpeed = rotationSpeed * Time.deltaTime;

            if (Mathf.Abs(currentRotation - initialRotation) > 0.1f)
            {
                // Từ từ quay về góc ban đầu
                currentRotation = Mathf.MoveTowards(currentRotation, initialRotation, returnSpeed);
                transform.rotation = Quaternion.Euler(0, currentRotation, 0);
            }
            else
            {
                // Dừng quay khi đạt góc ban đầu
                currentRotation = initialRotation;
                isReturning = false;
                isRotating = false; // Dừng quá trình xoay
            }
        }
    }
}
