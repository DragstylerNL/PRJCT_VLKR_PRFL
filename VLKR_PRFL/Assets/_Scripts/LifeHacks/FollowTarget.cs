using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    // =<[L][O][N][E]>================================================================================= Public Variables
    public Transform TARGET;
    public Vector3 OFFSET;
    
    // =<[L][O][N][E]>=========================================================================================== Update
    private void Update()
    {
        if(TARGET == null) return;
        transform.position = TARGET.position + OFFSET;
    }
}
