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

    delegate Color32 ColorPickerDelegate(Color32[] color);

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
        Color32[] colorArray = new Color32[1];
        colorArray[0] = Color.white;
        UpdateColorData(colorArray, SetPixelColor);
    }

    private void UpdateColorData(Color32[] color, ColorPickerDelegate colorPicker)
    {
        foreach (int x in Enumerable.Range(0, width))
        {
            foreach (int y in Enumerable.Range(0, height))
            {
                _colorData[x + y * width] = colorPicker(color);
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

        Color32[] colorArray = new Color32[2];
        colorArray[0] = Color.white;
        colorArray[1] = Color.black;

        UpdateColorData(colorArray, SetBiasedPixelColor);

        //Color32[] colorArray = new Color32[9];
        //colorArray[0] = Color.white;
        //colorArray[1] = Color.red;
        //colorArray[2] = Color.green;
        //colorArray[3] = Color.blue;
        //colorArray[4] = Color.cyan;
        //colorArray[5] = Color.magenta;
        //colorArray[6] = Color.yellow;
        //colorArray[7] = Color.black;
        //colorArray[8] = Color.grey;
        //UpdateColorData(colorArray, SetDistributedPixelColor);

        UpdateTexture();
    }

    private Color32 SetDistributedPixelColor(Color32[] colors)
    {
        Color returnColor = colors[0];
        float propabilityLimit = 1f / colors.Length;
        foreach (Color color in colors)
        {
            if (Random.value < propabilityLimit)
            {
                returnColor = color;
                break;
            }
        }

        return returnColor;
    }

    private Color32 SetBiasedPixelColor(Color32[] _color)
    {
        if (Random.value > _bias)
        {
            return _color[0];
        }
        else
        {
            return _color[1];
        }
    }

    private Color32 SetPixelColor(Color32[] _color)
    {
        return _color[0];
    }
}
