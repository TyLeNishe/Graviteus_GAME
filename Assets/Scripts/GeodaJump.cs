using UnityEngine;

public class GeodaJump : MonoBehaviour
{
    public float JumpForce = 5f;
    public float DestroyTime = 1f;

    private Rigidbody rb;
    private bool JumpFlag = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnMouseDown()
    {
        if (JumpFlag == false)
        {
            Jump();
        }
    }
    void Jump()
    {
        JumpFlag = true;
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        Destroy(gameObject, DestroyTime);
    }
}
