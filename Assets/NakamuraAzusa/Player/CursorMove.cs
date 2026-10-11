using UnityEngine;
using UnityEngine.InputSystem;

public class CursorMove : MonoBehaviour
{
    private Ray _lastRay;
    private float _rayDistance = 100.0f;
    private bool _hasFiredRay = false;

    [SerializeField] private GameSe _gameSe;
    [SerializeField] private AudioClip _selectSE;

    public void OnCatch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CatchRay();
        }
    }

    private void CatchRay()
    {
        if (Mouse.current != null)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            _lastRay = ray;
            _hasFiredRay = true;

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Carp"))
                {
                    Debug.Log("タップされたオブジェクト: " + hit.collider.name);
                    CarpController carpController = hit.collider.GetComponent<CarpController>();
                    if (carpController != null)
                    {
                        _gameSe.Play(_selectSE);
                        carpController.TransitionToCatched();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!_hasFiredRay) return;

        Gizmos.color = Color.red;

        Gizmos.DrawRay(_lastRay.origin, _lastRay.direction * _rayDistance);

        Vector3 endPoint = _lastRay.origin + (_lastRay.direction * _rayDistance);
        Gizmos.DrawSphere(endPoint, 0.2f);
    }
}
