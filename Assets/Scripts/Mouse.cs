using UnityEngine;

public class Mouse : MonoBehaviour
{
    public void Update()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousepos.x, mousepos.y, 0);
    }
}