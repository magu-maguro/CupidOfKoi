using UnityEngine;

public class CarpController : MonoBehaviour
{
    public enum CarpState
    {
        Normal,
        RunAway,
        Catched
    }

    [SerializeField] private CarpState _currentState = CarpState.Normal;

    private CarpMove _carpMove;

    private void Awake()
    {
        _carpMove = GetComponent<CarpMove>();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case CarpState.Normal:

                break;

            case CarpState.RunAway:

                break;

            case CarpState.Catched:

                break;
        }
    }

    public void TransitionTo(CarpState newState)
    {
        _currentState = newState;
    }

    public void TransitionToCatched()
    {
        TransitionTo(CarpState.Catched);
        _carpMove.NavStop();
        GameManager.Instance.GetComponent<CarpSpawner>().KoiSelected(gameObject);
    }
}
