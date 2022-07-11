using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InfinityScreen : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Texture2D _texture;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Color _newColor;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _texture = new Texture2D(width, width);
        _sprite = Sprite.Create(_texture, new Rect(0, 0, width, height), Vector2.one * 0.5f);

        foreach (int x in Enumerable.Range(0, width))
        {
            foreach (int y in Enumerable.Range(0, height))
            {
                _texture.SetPixel(x, y, Color.green);
            }
        }
        _texture.Apply();
        _renderer.sprite = _sprite;
    }

    // Update is called once per frame
    void Update()
    {
        _newColor = new Color(Random.value, Random.value, Random.value);
        _texture.SetPixel((int)(Random.value * width), (int)(Random.value * height), _newColor);
        //_sprite.
        _texture.Apply();
    }
}