using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragableLevelPiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler {

	static GameObject targetStructure;
	public GameObject myPrefab;

	private static GameObject instantiatedObject;

	[HideInInspector]
	public ObjectTypes ObjectType = ObjectTypes.TILE;

	[HideInInspector]
	public int ID;

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		if (eventData.pointerCurrentRaycast.gameObject == gameObject) {
			targetStructure = gameObject;
			Debug.Log ("I got pointed at " + gameObject.name);
		}
	}

	#endregion

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		if (targetStructure == gameObject) {
			if (instantiatedObject == null) {
				Debug.Log ("Time to instantiate " + myPrefab.name);
				instantiatedObject = Instantiate (myPrefab, new Vector3 (-1000, 0, -1000), myPrefab.transform.rotation) as GameObject;
			}
		}
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if (targetStructure == gameObject) {
			if(instantiatedObject != null){
				RaycastHit hit;

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit, 100f)) {
					if (hit.collider.gameObject.CompareTag ("TemplateTile")) {
						instantiatedObject.transform.position = hit.collider.gameObject.transform.position;
					}
				}
			}
		}
	}

	#endregion

	#region IPointerUpHandler implementation

	public void OnPointerUp (PointerEventData eventData)
	{
		if (targetStructure == gameObject) {
			if(instantiatedObject != null){
				RaycastHit hit;

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit, 100f)) {
					if (hit.collider.gameObject.CompareTag ("TemplateTile")) {
						hit.collider.gameObject.GetComponent<TemplateTile> ().Add (instantiatedObject, ObjectType, ID);
					}
				} else {
					Destroy (instantiatedObject);
				}
			}
		}

		instantiatedObject = null;
		targetStructure = null;
	}

	#endregion
}