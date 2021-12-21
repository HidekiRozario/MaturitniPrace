using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonSystem : MonoBehaviour
{
    [SerializeField] private float maxDistDown = 0.3f;
    [SerializeField] UnityEvent onClick;


    private bool isActivated = false;
    private bool wasActivated = false;
    private bool canActivate = true;
    public bool isActive = false;

    private float cooldown = 0.7f;
    private float cooldownTime = 0;

    [SerializeField] private float activateOffset = 0.1f;
    public int btnIds = 0;


    private Vector3 buttonDefaultPosition;

    private MeshRenderer rend;
    [SerializeField] private Material activatedMat;
    [SerializeField] private Material deactivatedMat;

    private void Start()
    {
        buttonDefaultPosition = transform.localPosition;
        rend = GetComponent<MeshRenderer>();
        rend.material = deactivatedMat;
    }

    void Update()
    {
        if (cooldownTime > 0)
            cooldownTime -= Time.deltaTime;

        if ((transform.localPosition.y - buttonDefaultPosition.y) > 0.005f)
        {
            transform.localPosition = new Vector3(buttonDefaultPosition.x, buttonDefaultPosition.y, buttonDefaultPosition.z);
        }

        if (Vector3.Distance(transform.localPosition, buttonDefaultPosition) > maxDistDown)
        {
            transform.localPosition = new Vector3(buttonDefaultPosition.x, buttonDefaultPosition.y - maxDistDown, buttonDefaultPosition.z);
        }

        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);

        ButtonActivate();

        if(!wasActivated && wasActivated != isActivated)
        {
            wasActivated = !wasActivated;
            onClick.Invoke();
            isActive = true;
        }
    }

    private void ButtonActivate()
    {
        if (Vector3.Distance(transform.localPosition, buttonDefaultPosition) > (maxDistDown - activateOffset) && canActivate && cooldownTime <= 0)
        {
            isActivated = true;
            rend.material = activatedMat;
        }
        else
        {
            isActivated = false;
            canActivate = true;
            wasActivated = false;
            isActive = false;
            rend.material = deactivatedMat;
        }
    }
}
