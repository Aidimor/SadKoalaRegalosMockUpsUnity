using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorTaza : MonoBehaviour
{
    public GameObject ParentTaza;
    public GameObject Taza;
    public GameObject MagicTaza;
    public GameObject Tshirt;
    public GameObject Thermo;
    public GameObject _thermoReal;
    public GameObject _mousePad;
    public GameObject _playMat;

    public GameObject[] _mouses;
    public GameObject _deck;

    public float speed;
    public bool rotationOn;
    public float OnAxis;

    public Color ColorAza;
    public bool OnButtonOver;
    public float pos;

    public GameObject Slider;
    public GameObject SliderMagicObject;
    public GameObject ParentPanel;
    public GameObject Camara;

    private int _ShootIndex = 0;
    public int superSize = 2;
    public GameObject FlashObject;
    public bool Recording;

    public float _sliderMagic;
    public float _sliderReal;
    public Material _sliderMaterial;

    private MeshRenderer tazaRenderer;
    private MeshRenderer thermoRealRenderer;
    private MeshRenderer tshirtRenderer;
    private MeshRenderer magicTazaRenderer;
    private MeshRenderer mouseRenderer;
    private MeshRenderer playmatRenderer;

    private ControladorImagenes controladorImagenes;

    private Camera cam;
    private Slider slider;
    private Slider sliderMagic;
    private Image flashImage;

    public int _onID;
    public int _extraID;
    public bool _mouseOn;

    public GameObject _deckObject;
    public Texture[] _deckCards;
    public Material _cardMaterial;
    void Start()
    {
        controladorImagenes = GetComponent<ControladorImagenes>();

        tazaRenderer = Taza.GetComponent<MeshRenderer>();
        thermoRealRenderer = _thermoReal.GetComponent<MeshRenderer>();
        tshirtRenderer = Tshirt.GetComponent<MeshRenderer>();
        magicTazaRenderer = MagicTaza.GetComponent<MeshRenderer>();
        mouseRenderer = _mousePad.GetComponent<MeshRenderer>();
        playmatRenderer = _playMat.GetComponent<MeshRenderer>();

        cam = Camara.GetComponent<Camera>();
        slider = Slider.GetComponent<Slider>();
        sliderMagic = SliderMagicObject.GetComponent<Slider>();
        flashImage = FlashObject.GetComponent<Image>();

        // textura inicial
        tazaRenderer.sharedMaterials[1].mainTexture = controladorImagenes.image.texture;
    }

    void Update()
    {
        ParentTaza.transform.localRotation = Quaternion.Euler(0, OnAxis, 0);

        // materiales
        tazaRenderer.sharedMaterials[2].color = ColorAza;
        tazaRenderer.sharedMaterials[1].mainTexture = controladorImagenes.image.texture;

        thermoRealRenderer.sharedMaterials[0].mainTexture = controladorImagenes.image.texture;
        tshirtRenderer.sharedMaterials[0].mainTexture = controladorImagenes.image.texture;
        magicTazaRenderer.sharedMaterials[1].mainTexture = controladorImagenes.image.texture;
        mouseRenderer.sharedMaterials[1].mainTexture = controladorImagenes.image.texture;
        playmatRenderer.sharedMaterials[1].mainTexture = controladorImagenes.image.texture;

        pos = Input.mousePosition.x;
        OnAxis = slider.value;

        _sliderMagic = sliderMagic.value;
        _sliderReal = _sliderMagic / 360f;

        HeatController();

        if (!rotationOn)
        {
            Slider.SetActive(true);
        }
        else
        {
            Slider.SetActive(false);
            slider.value += speed * Time.deltaTime;

            if (OnAxis >= 359)
                slider.value = 0;
        }

        if (Input.mousePosition.x > 675 && Input.GetMouseButtonDown(0) && !OnButtonOver && !Recording)
        {
            rotationOn = !rotationOn;
            Debug.Log("se detiene");
        }
    }

    public void ScreenShoot()
    {
        StartCoroutine(PhotoTake());
    }

    public IEnumerator PhotoTake()
    {
        ParentPanel.SetActive(false);
        cam.rect = new Rect(0, 0, 1, 1);

        // ruta de guardado
        string path = Application.persistentDataPath + "/Screenshot_" + _ShootIndex + ".png";

        ScreenCapture.CaptureScreenshot(path, superSize);
        Debug.Log("Screenshot guardado en: " + path);

        _ShootIndex++;

        flashImage.enabled = true;

        yield return new WaitForSeconds(0.1f);

        cam.rect = new Rect(0.5f, 0, 1, 1);
        ParentPanel.SetActive(true);
        flashImage.enabled = false;
    }

    public void ChangeItem(int id)
    {
        Taza.SetActive(false);
        Tshirt.SetActive(false);
        Thermo.SetActive(false);
        MagicTaza.SetActive(false);
        _mousePad.SetActive(false);
        _playMat.SetActive(false);

        switch (id)
        {
            case 0: Taza.SetActive(true); break;
            case 1: Tshirt.SetActive(true); break;
            case 2: Thermo.SetActive(true); break;
            case 3: MagicTaza.SetActive(true); break;
            case 4: _mousePad.SetActive(true); break;
            case 5: _playMat.SetActive(true); break;
        }
        _onID = id;
        MouseVoid();
    }

    public void HeatController()
    {
        // aplicar shader correctamente
        _sliderMaterial.SetFloat("_Fade", _sliderReal);
    }

    public void MouseVoid()
    {
        _mouses[0].SetActive(false);
        _mouses[1].SetActive(false);
        _deckObject.SetActive(false);

        switch (_onID)
        {
            default:

                break;
            case 4:
                _mouseOn = !_mouseOn;
                _mouses[0].SetActive(_mouseOn);
                break;
            case 5:
           
                switch (_extraID)
                {
                    case 0:
                        _mouses[1].SetActive(false);
                        _deckObject.SetActive(false);
                        _extraID++;
                        break;
                    case 1:
                        _mouses[1].SetActive(true);
                        _deckObject.SetActive(false);
                        _extraID++;
                        break;
                    case 2:
                        _deckObject.SetActive(true);
                        _cardMaterial.mainTexture = _deckCards[0];
                        _extraID++;
                        break;
                    case 3:
                        _deckObject.SetActive(true);
                        _cardMaterial.mainTexture = _deckCards[1];
                        _extraID++;
                        break;
                    case 4:
                        _deckObject.SetActive(true);
                        _cardMaterial.mainTexture = _deckCards[2];
                        _extraID = 0;
                        break;
                }
                break;
        }

           

 
    }
}