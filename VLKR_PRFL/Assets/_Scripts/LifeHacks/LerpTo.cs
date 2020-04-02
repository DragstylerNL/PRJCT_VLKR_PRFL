using System.Collections;
using UnityEngine;

public class LerpTo : MonoBehaviour
{
    public Vector3 targetPos;
    public float speed = 0.05f;
    public bool x, y, z;
    private Vector3 _pos;

    public void StartLerp()
    {
        _pos = Vector3.zero;
        StartCoroutine(Lerp());
    }

    IEnumerator Lerp()
    {
        while (transform.position != targetPos)
        {
            Vector3 position = transform.position;
            if(x)_pos.x = Mathf.Lerp(position.x, targetPos.x, speed);
            if(y)_pos.y = Mathf.Lerp(position.y, targetPos.y, speed);
            if(z)_pos.z = Mathf.Lerp(position.z, targetPos.z, speed);
            transform.position = _pos;
            yield return 0;
        }
        yield return 0;
    }
}
