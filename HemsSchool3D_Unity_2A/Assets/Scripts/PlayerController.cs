using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float lookSensitivity = 2f;
    public Transform cameraPivot;
    private CharacterController cc;
    private float pitch;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        if (cameraPivot == null)
        {
            Camera main = Camera.main;
            if (main) cameraPivot = main.transform;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
#if UNITY_WEBGL || UNITY_STANDALONE || UNITY_EDITOR
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(h, 0, v);
        Vector3 world = transform.TransformDirection(input);
        cc.SimpleMove(world * moveSpeed);

        float yaw = Input.GetAxis("Mouse X") * lookSensitivity;
        transform.Rotate(0, yaw, 0);
        pitch -= Input.GetAxis("Mouse Y") * lookSensitivity;
        pitch = Mathf.Clamp(pitch, -75, 75);
        if (cameraPivot) cameraPivot.localEulerAngles = new Vector3(pitch, 0, 0);
#endif
    }
}
