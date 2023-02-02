using UnityEngine;

public class CameraController : MonoBehaviour
{
    public MapGenerator mapSize;
    public Transform cameraTransform;
    public float movementSpeed;
    public float movementTime;
    public Vector3 zoomAmount;


    Vector3 newPosition;
    Vector3 newZoom;
    Vector3 dragStartPosition;
    Vector3 dragCurrentPosition;
    Vector2Int cameraRestriction;

    readonly Vector3 planePosition = new Vector3(0, 1f, 0);

    void Start()
    {
        newPosition = transform.position;
        newZoom = cameraTransform.localPosition;
        cameraRestriction = mapSize.getMapSize();
    }

    void Update()
    {
        handleMovementInput();
        handleMouseInput();
    }

    void handleMouseInput() {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, planePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, planePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                updateNewPosition(transform.position + dragStartPosition - dragCurrentPosition);
            }
        }
    }


    void handleMovementInput() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            updateNewPosition(newPosition + (transform.forward * movementSpeed));
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            updateNewPosition(newPosition + (transform.forward * -movementSpeed));
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            updateNewPosition(newPosition + (transform.right * movementSpeed));
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            updateNewPosition(newPosition + (transform.right * -movementSpeed));
        }

        if (Input.GetKey(KeyCode.R)) {
            updateNewZoom(newZoom + zoomAmount);
        }
        if (Input.GetKey(KeyCode.F)) {
            updateNewZoom(newZoom - zoomAmount);
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    void updateNewZoom(Vector3 vector)
    {
        float zoomValue = vector.y > 13f ? 13f : vector.y < 0f ? 0f : vector.y;
        newZoom = new Vector3(0, zoomValue, -zoomValue);
    }

    void updateNewPosition(Vector3 vector)
    {
        float x = vector.x < 0f ? 0f : vector.x > cameraRestriction.x ? cameraRestriction.x : vector.x;
        float z = vector.z < 0f ? 0f : vector.z > cameraRestriction.y ? cameraRestriction.y : vector.z;
        newPosition = new Vector3(x, vector.y, z);
    }
}
