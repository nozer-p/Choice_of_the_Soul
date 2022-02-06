using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splatDestroy : MonoBehaviour
{
    public float lifeTime;
    float startTime;
    SpriteRenderer sprite;
    SpriteMask mask;
    Color color;
    bool dying;

    // Start is called before the first frame update
    void Start()
    {
        mask = GetComponent<SpriteMask>();
        sprite = GetComponent<SpriteRenderer>();
        if (sprite)
        {
            color = sprite.color;
        }
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > lifeTime && !dying)
        {
            dying = true;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        float value = 0.15f;
        while (value > 0.0f)
        {
            value -= Time.deltaTime * 0.008f;
            if (sprite)
            {
                sprite.color = new Color(color.r, color.g, color.b, value+0.3f);
            }
            transform.localScale = new Vector3(value, value, value);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
