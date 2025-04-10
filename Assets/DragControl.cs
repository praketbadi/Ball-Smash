using UnityEngine;

public class DragControl : MonoBehaviour
{
    public LineRenderer line;
    public Rigidbody2D rb;
    public float dragLimit = 3f;
    public float Force = 10f;
    private Camera cam;
    private bool isDragging;

    Vector3 MousePosition
    {
        get
        {
            Vector3 position = cam.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0f;
            return position;
        }
    }

    private void Start()
    {
        cam = Camera.main;
        line.positionCount = 2;
        line.SetPosition(0, Vector2.zero);
        line.SetPosition(1, Vector2.zero);
        line.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DragStart();
        }
        if (isDragging)
        {
            dragg();
        }
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            DragEnd();
        }
    }

    void DragStart()
    {
        line.enabled = true;
        isDragging = true;
        line.SetPosition(0, MousePosition);
    }

    void dragg()
    {
        Vector3 startpos = line.GetPosition(0);
        Vector3 currentpos = MousePosition;
        Vector3 distance = currentpos - startpos;

        if (distance.magnitude <= dragLimit)
        {
            line.SetPosition(1, currentpos);
        }
        else
        {
            Vector3 limitVector = startpos + (distance.normalized * dragLimit);
            line.SetPosition(1, limitVector);
        }
    }

    private void DragEnd()
    {
        isDragging = false;
        line.enabled = false;

        Vector3 startpos = line.GetPosition(0);
        Vector3 currentpos = line.GetPosition(1);
        Vector3 distance = currentpos - startpos;
        Vector3 finalforce = distance * Force;

        rb.AddForce(-finalforce, ForceMode2D.Impulse);

        // The point awarding logic should NOT be here.
        // scoreman.instance.AddPoints();
    }

    // You'll need a way for the explosion to trigger the point increase.
    // This will likely involve the script on the exploding object.
}