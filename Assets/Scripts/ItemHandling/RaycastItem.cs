using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastItem : MonoBehaviour
{
    //public LayerMask ValidLayers;
    [SerializeField]
    UIManager uIManager;
    Camera cam;
    bool trackingPosition = true;
    private RaycastHit hitInfo;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        // Debug.DrawRay(ray.origin, ray.direction);

        if (!uIManager.inMenu)
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (!GameManager.Instance.pauseMenu.activeInHierarchy && Input.GetMouseButtonDown(0) &&
                    (hitInfo.collider.CompareTag("Erasable") || (hitInfo.collider.transform.parent != null && hitInfo.collider.transform.parent.CompareTag("Erasable"))))
                {
                    if (hitInfo.collider.transform.parent != null && hitInfo.collider.transform.parent.tag == "Erasable")
                    {
                        GameObject parent = hitInfo.collider.transform.parent.gameObject;
                        uIManager.ActivateItemCanvas(parent, parent.gameObject.GetComponent<ItemData>());
                    }
                    else
                        uIManager.ActivateItemCanvas(hitInfo.collider.gameObject, hitInfo.collider.gameObject.GetComponent<ItemData>());
                    trackingPosition = false;
                }
            }
        }
    }
}
