using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class ExpandingCrosshair : MonoBehaviour
{
    [Vector2AsRange]
    public Vector2 RadiusRange;

    public ArrowShooter arrowShooter;

    [Tooltip("Top piece of the aim reticle.")]
    public Image Top;
    [Tooltip("Bottom piece of the aim reticle.")]
    public Image Bottom;
    [Tooltip("Left piece of the aim reticle.")]
    public Image Left;
    [Tooltip("Right piece of the aim reticle.")]
    public Image Right;

    private Vector2 screenCenterPoint = new Vector2();
    private float m_CurrentRadius = 2.5f;

    private void Start()
    {
        screenCenterPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    private void Update()
    {
        m_CurrentRadius = Mathf.Lerp(RadiusRange.y, RadiusRange.x, arrowShooter.GetAimingValue);
        m_CurrentRadius = Mathf.Clamp(m_CurrentRadius, RadiusRange.x, RadiusRange.y);

        if (!Left || !Right || !Top || !Bottom)
        {
            Debug.LogWarning("You haven't set the crosshair images yet.");
            return;
        }

        Left.rectTransform.position = screenCenterPoint + (Vector2.left * m_CurrentRadius);
        Right.rectTransform.position = screenCenterPoint + (Vector2.right * m_CurrentRadius);
        Top.rectTransform.position = screenCenterPoint + (Vector2.up * m_CurrentRadius);
        Bottom.rectTransform.position = screenCenterPoint + (Vector2.down * m_CurrentRadius);
    }
}
