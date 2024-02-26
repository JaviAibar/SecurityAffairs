using UnityEngine;

public class SwitchClickableChildren : MonoBehaviour
{
    [SerializeField] private Selectable m_Selectable;
    private void Awake()
    {
        if (m_Selectable == null)  
            m_Selectable = GetComponentInChildren<Selectable>();
    }

    public void SwitchClickable()
    {
        m_Selectable.SwitchClicable();
    }
}
