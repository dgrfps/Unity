using UnityEngine;

public class mCameraController : MonoBehaviour
{
    [System.Serializable]
    struct Sensivity
    {
        public float x;
        public float y;
        public float scale;
        public Sensivity(float x, float y, float s)
        {
            this.x = x; this.y = y; this.scale = s;
        }
    }

    [SerializeField] Sensivity sensivity = new Sensivity(1, 1, 100);
    [SerializeField] float smooth = .125f;
    [SerializeField] float maxYaw = 80f;
    [SerializeField] Transform body;

    float rotation = 0;
    float s_x;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float x = Input.GetAxis("Mouse X") * sensivity.x * sensivity.scale * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensivity.y * sensivity.scale * Time.deltaTime;

        rotation -= y;
        rotation = Mathf.Clamp(rotation, -Mathf.Abs(maxYaw), Mathf.Abs(maxYaw));

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotation, 0, 0), Time.deltaTime / smooth);
        s_x = Mathf.Lerp(s_x, x, Time.deltaTime / smooth);
        body.Rotate(body.transform.up * s_x);
    }
}
