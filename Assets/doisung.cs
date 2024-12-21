using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doisung : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject[] weapons; // Mảng chứa các súng
    private int selectedWeapon = 0; // Súng hiện tại được chọn

    void Start()
    {
        SelectWeapon(); // Hiển thị súng đầu tiên khi bắt đầu
    }

    void Update()
    {
        // Kiểm tra input từ bàn phím
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Length >= 3)
        {
            selectedWeapon = 2;
        }

        SelectWeapon(); // Cập nhật súng được chọn
    }

    void SelectWeapon()
    {
        // Ẩn tất cả súng
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == selectedWeapon); // Chỉ kích hoạt súng được chọn
        }
    }
}
