using System.Collections;
using UnityEngine;

public class MP_CharacterSelect : MonoBehaviour
{
    // ============================================================================================== "public" variables
    [SerializeField] private Transform selectorScreen;
    
    // =============================================================================================== private variables
    private bool _update = false;
    
    // ====================================================================================== Start selecting Characters
    public void StartSelection()
    {
        StartCoroutine(ShowUi());
    }

    // ============================================================================================ Start showing the UI
    private IEnumerator ShowUi()
    {
        float destination = 0f;
        bool pos = true;
        while (pos)
        {
            selectorScreen.localPosition = new Vector3(0, Mathf.Lerp(selectorScreen.localPosition.y, destination, 0.05f));
            if (selectorScreen.localPosition.y < destination + 0.01f)
            {
                selectorScreen.localPosition = new Vector3(0, destination);
                pos = false;
                _update = true;
            }
            yield return 0;
        }
        yield return 0;
    }
    
    // ========================================================================================================== Update
    private void Update()
    {
        if (!_update) return;
        print("Noice");
    }
}
