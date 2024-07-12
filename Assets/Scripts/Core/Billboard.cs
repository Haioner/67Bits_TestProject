using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        Vector3 direction = mainCamera.transform.position - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(-direction);
        transform.rotation = rotation;
    }
}
