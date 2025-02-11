using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttachRotation : MonoBehaviour
{
    public Transform parent; // GameObject cha ch?a t?t c? nhân v?t

    void Start()
    {
        if (parent != null)
        {
            foreach (Transform child in parent)
            {
                if (child.GetComponent<CharacterRotation>() == null)
                {
                    // Thêm script CharacterRotation vào m?i nhân v?t con
                    child.gameObject.AddComponent<CharacterRotation>();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
