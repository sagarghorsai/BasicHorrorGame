using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [Header("Serialized")]
    public float moveSpeed;
    public float sprintSpeed;
    public float jumpForce;
    public Rigidbody rb;
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    public Image StaminaBar;
    public float Stamina;
    public float MaxStamina;
    public float StaminaRegenRate;
    public float StaminaDrainRate;

    private float xRotation = 0f;
    private Inventory inventory;
    PhotonView view;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Stamina = MaxStamina;
        inventory = GetComponent<Inventory>();
        view = GetComponent<PhotonView>();

    }

    private void Update()
    {
        if (view.IsMine) { 
        Move();
        Rotate();
        UpdateStamina();
        UpdateStaminaBar();


        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
            {
                inventory.SelectItem(i - 1);
                Debug.Log("Pressed");
            }
        }

        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
            {
                inventory.DeselectCurrentItem(); // Deselect the current item
                inventory.SelectItem(i - 1); // Select the new item
            }
        }

        // Optionally, handle item deselection when no key is pressed (optional)
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            inventory.DeselectCurrentItem(); // Deselect current item when key 0 is pressed
        }


    }}

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && Stamina > 0;
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        Vector3 dir = transform.right * x + transform.forward * z;
        dir *= currentSpeed;
        dir.y = rb.velocity.y;
        rb.velocity = dir;

        if (isSprinting)
        {
            Stamina -= StaminaDrainRate * Time.deltaTime;
            if (Stamina < 0) Stamina = 0;
        }
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void UpdateStamina()
    {
        if (!Input.GetKey(KeyCode.LeftShift) || rb.velocity.magnitude == 0)
        {
            Stamina += StaminaRegenRate * Time.deltaTime;
            if (Stamina > MaxStamina) Stamina = MaxStamina;
        }
    }

    private void UpdateStaminaBar()
    {
        StaminaBar.fillAmount = Stamina / MaxStamina;
    }
}
