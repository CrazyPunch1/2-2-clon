using UnityEngine;
using Cinemachine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] float maxVerticalAngle = 90f;
    [SerializeField] bool useCameraChange = false;
    [SerializeField] CinemachineVirtualCamera firstPersonCamera;
    [SerializeField] CinemachineVirtualCamera thirdPersonCamera;
    [SerializeField] GameObject firstPersonCanvas;
    [SerializeField] Shooting shooting;

    private float xRotation = 0f;
    private bool isInThirdPersonView = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(useCameraChange) SwitchToThirdPersonView(); // Start with first-person view by default
    }

    void Update()
    {

        LookAround();
        if (Input.GetKeyDown(KeyCode.Mouse1) && useCameraChange) // Toggle view on right-click
        {
            ChangeView();
        }
    }

    void LookAround()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;



        // Adjust up/down rotation (mouseY) to look up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxVerticalAngle, maxVerticalAngle); // Clamp vertical rotation to prevent over-rotation


        // Rotate player body left and right
        this.transform.Rotate(Vector3.up * mouseX);

        // Apply the vertical rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.eulerAngles.y, 0);
    }

    void ChangeView()
    {
        if (isInThirdPersonView)
        {
            SwitchToFirstPersonView();
        }
        else
        {
            SwitchToThirdPersonView();
        }
    }

    void SwitchToFirstPersonView()
    {
        shooting.enabled = true;
        firstPersonCanvas.gameObject.SetActive(true);
        thirdPersonCamera.Priority = 0; // Lower the priority of third-person camera
        firstPersonCamera.Priority = 10; // Raise the priority of first-person camera
        isInThirdPersonView = false;
        // Reset the camera rotation when switching to first-person view
        xRotation = firstPersonCamera.transform.localEulerAngles.x;
    }

    void SwitchToThirdPersonView()
    {
        shooting.enabled = false;
        firstPersonCanvas.gameObject.SetActive(false);
        firstPersonCamera.Priority = 0; // Lower the priority of first-person camera
        thirdPersonCamera.Priority = 10; // Raise the priority of third-person camera
        isInThirdPersonView = true;
    }
}
