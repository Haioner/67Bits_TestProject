using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Vector2 minMaxPos;
    [SerializeField] private float speed = 5f;
    private float _initialSpeed;

    private void OnEnable()
    {
        SetInitialRotation();

        speed += Random.Range(-0.5f, 0.5f);
        _initialSpeed = speed;
    }

    private void Update()
    {
        MoveNPC();
        RotateNPC();
    }

    public void DisableMovement()
    {
        anim.SetFloat("Speed", 0);
        enabled = false;
    }

    private void MoveNPC()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        anim.SetFloat("Speed", speed);
    }

    private void RotateNPC()
    {
        if (transform.position.x <= minMaxPos.x)
            transform.rotation = Quaternion.Euler(0, 90, 0);
        
        if (transform.position.x >= minMaxPos.y)
            transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    public void SetInitialRotation()
    {
        // Check the initial position and set the rotation accordingly
        float distanceToMin = Mathf.Abs(transform.position.x - minMaxPos.x);
        float distanceToMax = Mathf.Abs(transform.position.x - minMaxPos.y);

        if (distanceToMin < distanceToMax)
            transform.rotation = Quaternion.Euler(0, 90, 0);
        else
            transform.rotation = Quaternion.Euler(0, -90, 0);
    }
}
