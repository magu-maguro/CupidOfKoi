using UnityEngine;

public class CreditButtonController : MonoBehaviour
{
    bool isCreditPanelVisible = false;
    [SerializeField] private CreditController creditController;

    public void ToggleCreditPanel()
    {
        isCreditPanelVisible = !isCreditPanelVisible;
        creditController.ToggleCreditPanel(isCreditPanelVisible);
    }
}
