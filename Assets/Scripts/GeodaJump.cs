using System.Threading;
using UnityEngine;

public class GeodaJump : MonoBehaviour
{
    bool JumpFlag = false;
    public float Limit = 3f;
    public float Step = 1f;
    private float CurrentTime = 1f;
    void Update()
    {
        if (JumpFlag)
        {
            CurrentTime += Time.deltaTime;
            transform.position = transform.position + new Vector3(0, ((1 * CurrentTime * CurrentTime * CurrentTime) /Step ), 0);
            if (CurrentTime >= Limit)
            {
                CurrentTime = 1f;
                Destroy(gameObject);
            }
        }
    }


    private void OnMouseDown()
    {
        JumpFlag = true;
        CurrentTime = 1f;
    }

}