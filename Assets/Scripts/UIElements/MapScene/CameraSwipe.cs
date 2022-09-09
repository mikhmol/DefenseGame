using UnityEngine;

public class CameraSwipe : MonoBehaviour
{
    private Vector2 startPos;
    private Camera cam;

    private float targetPosX, targetPosY;

    // camera motion smoothing speed
    public float moveSpeed;
    // camera moving limits on axis X and Y
    public float maxX, minX, maxY, minY;

    // getting component Camera on the MainCamera gameobject
    private void Start()
    {
        cam = GetComponent<Camera>();
        targetPosX = transform.position.x;
        targetPosY = transform.position.y;
    }

    private void Update()
    {
        // if the player clicked on the screen, we save the cursor
        // coordinates and translate them into world coordinates
        if (Input.GetMouseButtonDown(0))
        {
            startPos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        // this statement will be executed when the user has pressed
        // and not released
        else if (Input.GetMouseButton(0))
        {
            // from the current cursor position, we subtract the starting
            // position
            float posX = cam.ScreenToWorldPoint(Input.mousePosition).x - startPos.x;
            float posY = cam.ScreenToWorldPoint(Input.mousePosition).y - startPos.y;

            // calculating target position, Mathf.Clamp - valid camera limits
            targetPosX = Mathf.Clamp(transform.position.x - posX, minX, maxX);
            targetPosY = Mathf.Clamp(transform.position.y - posY, minY, maxY);
        }

        // changing camera position, Mathf.Lerp - camera motion smoothing
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosX, moveSpeed * Time.deltaTime),
            Mathf.Lerp(transform.position.y, targetPosY, moveSpeed * Time.deltaTime), transform.position.z);
    }
}
