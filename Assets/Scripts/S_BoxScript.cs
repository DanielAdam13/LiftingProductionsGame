using UnityEngine;
using static UnityEngine.UI.Image;

public class S_BoxScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Int nr is what nuber the box is in the tower from botom to the top
    public void attachBox( GameObject forks, int nr)
    {
        Vector3 pos = transform.localPosition;
        pos.y += 0.4f + 1 * nr;
        
        this.gameObject.transform.SetLocalPositionAndRotation(pos, this.gameObject.transform.localRotation);
        this.gameObject.transform.parent = forks.transform;
    }

    public void detachBox()
    {
        if (this.gameObject.transform.parent != null && this.gameObject.transform.parent.parent != null)
        {
            //this.gameObject.transform.rotation = this.gameObject.transform.parent.parent.rotation;
        }

      
        Transform visuals = this.transform.Find("Visuals");
        float boxHeight = 0.5f; // A default height in case visuals aren't found
        if (visuals != null)
        {
            boxHeight = visuals.lossyScale.y;
        }

        this.gameObject.transform.SetParent(null);

        this.gameObject.transform.localScale = Vector3.one;

        // --- Position the box on the ground ---
        RaycastHit hit;
        float maxDistance = 10.0f;

        if (Physics.Raycast(this.gameObject.transform.position, Vector3.down, out hit, maxDistance))
        {
            transform.position = hit.point + new Vector3(0, boxHeight / 2.0f, 0);
        }
    }
}
