using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverAnimatorController : MonoBehaviour
{
    [SerializeField] private float shakeCoeficient = 0.01f;
    [SerializeField] private ParticleSystem[] explosions;
    [SerializeField] private Animator bridgeCollapseAnim;
    [SerializeField] private GameObject[] fires;
    [SerializeField] private Image endImage;
    [SerializeField] private float timeImageDelta = 0;
    [SerializeField] private Transform playerCam;
    [SerializeField] private bool isShaking = false;
    [SerializeField] private float explosionWait;
    [SerializeField] private float timeCooldown = 0.05f;

    private int index = 0;
    private bool addition = true;
    private float time = 0;
    private float x;
    private float y;

    private void FixedUpdate()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            return;
        }

        if (isShaking)
        {
            if (addition)
            {
                x = Random.Range(-1f, 1f) * shakeCoeficient;
                y = Random.Range(-1f, 1f) * shakeCoeficient;
            }
            else
            {
                x = -x;
                y = -y;
            }

            playerCam.localPosition = new Vector3(playerCam.localPosition.x + x, playerCam.localPosition.y + y, playerCam.localPosition.z);
            addition = !addition;

            if (index >= 5)
            {
                endImage.color = new Color(endImage.color.r, endImage.color.g, endImage.color.b, timeImageDelta);
                timeImageDelta += Time.deltaTime / 1f;
            }

            time = timeCooldown;
        }
    }

    public void GameOver()
    {
        isShaking = true;
        bridgeCollapseAnim.Play("Bridge");
        StartCoroutine(explosionStart());
    }

    public IEnumerator explosionStart()
    {
        if (index < explosions.Length)
        {
            explosions[index].transform.parent.gameObject.SetActive(true);
            index++;

            explosions[index].transform.parent.gameObject.SetActive(true);
            index++;
        }
        else
        {
            SceneManager.LoadScene(0);
        }

        if (index >= 2)
            fires[0].SetActive(true);

        if (index >= 4)
            fires[1].SetActive(true);

        shakeCoeficient = index / 100f;

        yield return new WaitForSeconds(explosionWait);

        StartCoroutine(explosionStart());
    }
}
