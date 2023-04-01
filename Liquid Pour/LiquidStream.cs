using System.Collections;
using UnityEngine;
public class LiquidStream : MonoBehaviour
{
   private LineRenderer lineRenderer;
   private ParticleSystem splashParticle;
   private Coroutine pourRoutine;
   private Vector3 targetPosition;
   private IContainable pouringContainer;
   private RaycastHit hit;
   public LayerMask pourLayer;
   void Awake() {
       lineRenderer = GetComponent<LineRenderer>();
       splashParticle = GetComponentInChildren<ParticleSystem>();
       pouringContainer = GetComponentInParent<IContainable>();
       pourLayer = LayerMask.GetMask(new string[4]{"Liquid", "Glass Ware", "Tool", "Surface"});
       AudioManager.instance.PlaySfx("Liquid Pour");
   }
   void Start() {
       MoveToPosition(0, transform.position);
       MoveToPosition(1, transform.position);
   }
   void Update() {
       PourMechanic();
   }
   public void Begin() {
       StartCoroutine(UpdateParticle());
       pourRoutine = StartCoroutine(BeginPour());
   }
   IEnumerator BeginPour() {
       while(gameObject.activeSelf) {
            targetPosition = FindEndPoint();
            lineRenderer.startColor = pouringContainer.GetInfo()._liquidColor;
            lineRenderer.endColor = pouringContainer.GetInfo()._liquidColor;
            MoveToPosition(0, transform.position);
            AnimateToPosition(1, targetPosition);
            yield return null;
       }
   }
   public void End() {
       StopCoroutine(pourRoutine);
       pourRoutine = StartCoroutine(EndPour());
   }
   IEnumerator EndPour() {
       while(!HasReachedPosition(0, targetPosition)) {
           AnimateToPosition(0, targetPosition);
           AnimateToPosition(1, targetPosition);
           yield return null;
       }
       Destroy(gameObject);
   }
   Vector3 FindEndPoint() {
       Ray ray = new Ray(transform.position, Vector3.down);
       Physics.Raycast(ray, out hit, 2.0f, pourLayer, QueryTriggerInteraction.Ignore);
       float point = 2.0f;
       Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(point);

       return endPoint;
   }
   void MoveToPosition(int index, Vector3 targetPosition) {
        lineRenderer.SetPosition(index, targetPosition);
   }
   void AnimateToPosition(int index, Vector3 targetPosition) {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f);
        lineRenderer.SetPosition(index, newPosition);
   }
   void PourMechanic() {
        Liquid recievingContainer = null;
        try {
            recievingContainer = hit.collider.gameObject.GetComponentInChildren<Liquid>();
        } catch {}
        
        pouringContainer.EmptyContainer(1f);
        if(recievingContainer != null && hit.collider.tag == "Liquid") {
            recievingContainer.Add(pouringContainer.GetInfo()._chemicals);
        } else {
            // do nothing..
        }
   }
   bool HasReachedPosition(int index, Vector3 targetPosition) {
       Vector3 currentPosition = lineRenderer.GetPosition(index);
       return currentPosition == targetPosition;
   }
   IEnumerator UpdateParticle() {
       while(gameObject.activeSelf) {
           splashParticle.gameObject.transform.position = targetPosition;
           bool isHitting = HasReachedPosition(1, targetPosition);
           splashParticle.gameObject.SetActive(isHitting);
           yield return null;
       }
   }

   private void OnDestroy() {
    AudioManager.instance.StopSfx("Liquid Pour");
   }
}
