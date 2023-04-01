using UnityEngine.XR.Interaction.Toolkit;

public class SnapObjects : XRSocketInteractor {
    
    public string ObjectTag;
    public override bool CanHover(IXRHoverInteractable interactable) {
        return base.CanHover(interactable) && interactable.transform.CompareTag(ObjectTag);
    }
    public override bool CanSelect(IXRSelectInteractable interactable) {
        return base.CanSelect(interactable) && interactable.transform.CompareTag(ObjectTag);
    }
}
