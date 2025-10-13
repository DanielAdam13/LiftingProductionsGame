using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // Int nr is what nuber the box is in the tower from botom to the top
    public void AttachBox( GameObject forks, int nr)
    {
        Transform palletAndBoxTransform = this.gameObject.transform.parent;

        palletAndBoxTransform.SetParent(forks.transform);

        Vector3 newLocalPosition = this.transform.position;
        newLocalPosition.y = 0.4f + (1.0f * nr) + (palletAndBoxTransform.transform.Find("Visuals").transform.localScale.y/2); // Stacking logic
        palletAndBoxTransform.position = newLocalPosition;

        palletAndBoxTransform.localRotation = Quaternion.identity;
    }

    public void DetachBox()
    {
        if (this.gameObject.transform.parent != null && this.gameObject.transform.parent.parent != null)
        {
            this.gameObject.transform.rotation = this.gameObject.transform.parent.parent.rotation;
        }
      
        Transform visuals = this.transform.Find("Visuals");
        float boxHeight = 0.5f; // A default height in case visuals aren't found
        if (visuals != null)
        {
            boxHeight = visuals.lossyScale.y;
        }

        this.gameObject.transform.parent.SetParent(null);

        // --- Position the box on the ground ---
        RaycastHit hit;
        float maxDistance = 10.0f;

        if (Physics.Raycast(this.gameObject.transform.position, Vector3.down, out hit, maxDistance))
        {
            //Debug.Log("Raycast hit: " + hit.collider.name);
            this.transform.parent.transform.position = hit.point + new Vector3(0, boxHeight * 2 + 0.1f, 0);
        }
    }
}
