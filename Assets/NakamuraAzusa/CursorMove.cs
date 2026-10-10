using UnityEngine;
using UnityEngine.InputSystem;

public class CursorMove : MonoBehaviour
{
    private void Update()
    {
        if (Mouse.current != null)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                Debug.Log(hit.point);
            }
        }
    }
}
