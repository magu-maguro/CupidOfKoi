using UnityEngine;

public class CarpController : MonoBehaviour
{
    public enum CarpState
    {
        Normal,
        RunAway,
        Catched
    }

    private CarpState _currentState = CarpState.Normal;

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

    public void TransitionTo(CarpState _newState)
    {

    }
}
