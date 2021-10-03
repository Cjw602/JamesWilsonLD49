using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUIController : MonoBehaviour
{
    [SerializeField] GameObject holder;
    public GameObject SegmentPrefab;


    private void Start()
    {
        for (int SegmentIndex = 0; SegmentIndex < Game.Instance.jumpsAllowed; SegmentIndex++)
        {
            Instantiate(SegmentPrefab, holder.transform);
        }
        updateJumps(0);
    }

    public void updateJumps(int jumps)
    {
        for (int segmentIndex = 0; segmentIndex < holder.transform.childCount; segmentIndex++)
        {
            Transform segment = holder.transform.GetChild(segmentIndex);
            if (segmentIndex < jumps)
            {
                segment.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                segment.GetChild(0).gameObject.SetActive(false);
            }
        }
    }


}
