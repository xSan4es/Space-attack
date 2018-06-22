using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystickMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Image bgImg;
    private Image joystickImage;
    private Image joystickImage2;
    private Vector3 inputVector;
    Vector3 m_StartPos;

    private void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
        joystickImage2 = transform.GetChild(1).GetComponent<Image>();
        m_StartPos = new Vector3(0, -2.5f, 0);
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            joystickImage2.rectTransform.anchoredPosition = new Vector3(ped.position.x - joystickImage2.rectTransform.sizeDelta.x, ped.position.y - joystickImage2.rectTransform.sizeDelta.y);
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
            
            inputVector = new Vector3(pos.x * 2 - 1, 0, pos.y * 2 - 1);
            
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
            joystickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 4), inputVector.z * (bgImg.rectTransform.sizeDelta.y / 4));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = m_StartPos;
        joystickImage2.rectTransform.anchoredPosition = m_StartPos;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }
}