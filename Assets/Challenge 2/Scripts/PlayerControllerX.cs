using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    private bool canPressSpace = true;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && canPressSpace)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);

            canPressSpace = false;

            // Bắt đầu coroutine để đợi trong 3 giây
            StartCoroutine(WaitBeforeNextPress());
        }
    }
    IEnumerator WaitBeforeNextPress()
    {
        // Đợi trong 3 giây
        yield return new WaitForSeconds(2f);

        // Cho phép ấn phím Space lần nữa
        canPressSpace = true;
    }
}
