using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFinder : MonoBehaviour
{
    public float scalarRate;
    private Vector3 scalarRateV;
    public bool exponentialScaling;

    public GameObject starFinder;

    // Start is called before the first frame update
    void Start()
    {
        scalarRateV = new Vector3(1, 1, 1) * scalarRate;
        IEnumerator coroutine = beginScaling();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator beginScaling()
    {
        var t = 0f;
        while (t < 20)
        {
            t += Time.deltaTime;
            if (exponentialScaling)
            {
                transform.localScale *= scalarRate;
            }
            else
            {
                transform.localScale += scalarRateV;
            }
            
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Finish"))
        {
            other.tag = "Finish";
            gameObject.GetComponentInParent<StarData>().constellationLineRenderer.SetPosition(0, gameObject.transform.position);
            gameObject.GetComponentInParent<StarData>().constellationLineRenderer.SetPosition(1, other.transform.position);
            GameObject.Instantiate(starFinder, other.transform);
            GameObject.Destroy(gameObject);
        }
        

    }

}
