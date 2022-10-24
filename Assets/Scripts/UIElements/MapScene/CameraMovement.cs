using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;

    [SerializeField]
    private float minCamSize, maxCamSize, zoomSpeed = 5f, moveSpeed = 3f;

    [SerializeField]
    private SpriteRenderer mapRenderer;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private Vector3 dragOrigin, finalVector;

    private void Awake()
    {
        cam = GetComponent<Camera>();

        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    private void Update() 
    {
        HandleMoveCamera();
    }

    private void HandleMoveCamera()
    {
        // save position of mouse in world space when player clicked on the screen (first time clicked)
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;


            if (SceneManager.GetActiveScene().name == "MapScene" || SceneManager.GetActiveScene().name == "TestScene") // Activate zoom ability for android
            {
                HandleZoomCamera(difference * 0.01f);
            }
        }
        // this statement will be executed when the user has pressed and not released
        else if (Input.GetMouseButton(0))
        {
            // calculate distance between drag origin and new position if it is still held down
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("origin " + dragOrigin + " newPosition " + cam.ScreenToWorldPoint(Input.mousePosition) + " = difference " + difference);

            // move the camera by that distance and use Mathf.Lerp - camera motion smoothing 
            finalVector = ClampCamera(cam.transform.position + difference); 
        }

        cam.transform.position = new Vector3(Mathf.Lerp(transform.position.x, finalVector.x, moveSpeed * Time.deltaTime),
                Mathf.Lerp(transform.position.y, finalVector.y, moveSpeed * Time.deltaTime), transform.position.z);

        if (SceneManager.GetActiveScene().name == "MapScene" || SceneManager.GetActiveScene().name == "TestScene") // Activate zoom ability for pc
        {
            HandleZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    public void HandleZoomCamera(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment * zoomSpeed, minCamSize, maxCamSize);
        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
