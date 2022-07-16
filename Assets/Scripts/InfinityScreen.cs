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
    [SerializeField] [Range(0, 1)] private float _bias;
    private Color32[] _colorData;

    void Start()
    {
        InitRenderer();

        InitTexture();

        InitSprite();

        InitColorData();

        UpdateTexture();
    }
    private void InitColorData()
    {
        _colorData = new Color32[width * height];
        UpdateColorData();
    }

    private void UpdateColorData()
    {
        foreach (int x in Enumerable.Range(0, width))
        {
            foreach (int y in Enumerable.Range(0, height))
            {
                _colorData[x + y * width] = Color.white;
            }
        }
    }

    private void InitSprite()
    {
        _sprite = Sprite.Create(_texture, new Rect(0, 0, width, height), Vector2.one * 0.5f);
        _renderer.sprite = _sprite;
    }

    private void InitRenderer()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void InitTexture()
    {
        _texture = new Texture2D(width, width)
        {
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Point
        };
    }

    private void UpdateTexture()
    {
        _texture.SetPixels32(_colorData);
        _texture.Apply();
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
                    _colorData[x + y * width] = Color.black;
                }
                else
                {
                    _colorData[x + y * width] = Color.white;
                }
            }
        }

        UpdateTexture();
    }
}