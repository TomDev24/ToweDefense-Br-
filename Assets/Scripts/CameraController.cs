using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float panSpeed = 30f;
    [SerializeField]
    private float panThickness = 10f;

    [SerializeField]
    private float scrollSpeed = 5f;
    [SerializeField]
    private float camFollowSpeed = 10f;

    [SerializeField]
    private float minY = 10f;
    [SerializeField]
    private float maxY = 80f;

    private bool doMovement = true;

    //More about RTS camera https://forum.unity.com/threads/rts-camera-script.72045/
    void Update()
    {
        if (GameManager.gameEnded)
        {
            this.enabled = false; // when restart the scne, it will be enabled
            return;
        }

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panThickness)
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panThickness)
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panThickness)
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panThickness)
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * scrollSpeed * 2000f * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * camFollowSpeed); 
    }
}
