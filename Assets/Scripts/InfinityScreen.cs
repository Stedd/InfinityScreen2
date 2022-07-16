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

    //delegate Color32 SetPixelColorDelegate(int x, int y, Color[] color);
    delegate Color32 SetPixelColorDelegate(Color[] color);

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
                Color[] colorArray = new Color[1];
                colorArray[0] = Color.white;
        UpdateColorData(SetPixelColor(colorArray));
    }

    private void UpdateColorData(SetPixelColorDelegate _colorPicker)
    {
        foreach (int x in Enumerable.Range(0, width))
        {
            foreach (int y in Enumerable.Range(0, height))
            {
                _colorPicker(x, y, colorArray);
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
                Color[] colorArray = new Color[1];
                colorArray[0] = Color.white;
                colorArray[1] = Color.black;
                SetBiasedPixelColor(x, y, colorArray);
            }
        }

        UpdateTexture();
    }

    private void SetBiasedPixelColor(Color[] _color)
    {
        if (Random.value > _bias)
        {
            _colorData[x + y * width] = _color[0];
        }
        else
        {
            _colorData[x + y * width] = _color[1];
        }
    }

    private void SetPixelColor(Color[] _color)
    {
        _colorData[x + y * width] = _color[0];
    }
}
