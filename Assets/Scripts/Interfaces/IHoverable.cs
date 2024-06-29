using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoverable
{
    public abstract void OnHover();
    public IHoverable HoveringOver()
    {
        Ray ray = CameraManager.Instance.GetCam().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int layerMask = LayerMask.GetMask("Cell");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            IHoverable hoverable = hit.collider.gameObject.GetComponent<IHoverable>();
            if (hoverable != null && hoverable == this)
            {
                return this;
            }
        }
        return null;
    }
}