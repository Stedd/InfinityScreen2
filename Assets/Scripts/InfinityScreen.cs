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
    [SerializeField][Range(0,1)] private float _bias;
    [SerializeField] private Color _newColor;
    private Color[] _newColors;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _texture = new Texture2D(width, width);
        _sprite = Sprite.Create(_texture, new Rect(0, 0, width, height), Vector2.one * 0.5f);
        _newColors = new Color[width * height];

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

        foreach (int x in Enumerable.Range(0, width))
        {
            foreach (int y in Enumerable.Range(0, height))
            {
                if (Random.value > _bias)
                {
                    _newColors[x + y * width] = Color.black;
                }
                else
                {
                    _newColors[x + y * width] = Color.white;
                }
            }
        }

        _texture.SetPixels(_newColors);
        //_sprite.
        _texture.Apply();
    }
}