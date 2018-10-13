using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour
{


    private MeshRenderer renderer;
    private Material mat;
    private bool canScroll;
    private int currentPhase = -1;

    [Serializable]
    public struct TextureDef
    {
        public int startPixelH, endPixelH;
    }

    [SerializeField]
    private Texture2D mainTexture;
    private Texture2D currentTexture;
    [SerializeField]
    private TextureDef[] textureDefs;
    private bool isInTransition = false;
    private bool shouldSwitchPhase;
    private int nextPhase;
    private float textureScale, prevTextureScale;
    [SerializeField]
    private float debugScrollSpeed;

    private bool hasNextPhase { get { return currentPhase < textureDefs.Length - 1; } }

    public int textureDefCount { get { return textureDefs.Length; } }

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        mat = renderer.material;
    }

    void OnPhaseEnded(float offset)
    {
        if (isInTransition)
        {
            mat.mainTexture = GenerateTexture(currentPhase);
            mat.mainTextureScale = new Vector2(1, textureScale);
            mat.mainTextureOffset = new Vector2(0,  textureScale * offset / prevTextureScale - textureScale);
            isInTransition = false;
        }
        else if (shouldSwitchPhase)
        {

            isInTransition = true;
            mat.mainTexture = GenerateTransitionTexture(currentPhase, nextPhase);
            mat.mainTextureScale = new Vector2(1, textureScale);
            mat.mainTextureOffset = new Vector2(0, textureScale * offset / prevTextureScale -textureScale);
            currentPhase = nextPhase;
            shouldSwitchPhase = false;

        }
        else mat.mainTextureOffset = new Vector2(0, offset);

    }

    public void Init()
    {
        canScroll = true;
        currentPhase = 0;
        prevTextureScale = textureScale = 1;
        isInTransition = true;
        OnPhaseEnded(0);
    }


    public Texture2D GenerateTransitionTexture(int phaseA, int phaseB)
    {
        TextureDef def1 = textureDefs[phaseA];
        TextureDef def2 = textureDefs[phaseB];
        currentTexture = new Texture2D(mainTexture.width, def2.endPixelH - def1.startPixelH);

        Color[] colors = mainTexture.GetPixels(0, mainTexture.height - def2.endPixelH, mainTexture.width, def2.endPixelH - def1.startPixelH);
        currentTexture.SetPixels(0, 0, mainTexture.width, def2.endPixelH - def1.startPixelH, colors);
        prevTextureScale = textureScale;
        textureScale = ((float)(def1.endPixelH - def1.startPixelH)) / (def2.endPixelH - def1.startPixelH);
        currentTexture.Apply();
        return currentTexture;
    }

    public Texture2D GenerateTexture(int phase)
    {
        TextureDef def = textureDefs[phase];
        currentTexture = new Texture2D(mainTexture.width, def.endPixelH - def.startPixelH);
        Color[] colors = mainTexture.GetPixels(0, mainTexture.height - def.endPixelH, mainTexture.width, def.endPixelH - def.startPixelH);
        currentTexture.SetPixels(0, 0, mainTexture.width, def.endPixelH - def.startPixelH, colors);
        currentTexture.Apply();
        prevTextureScale = textureScale;
        textureScale = 1;
        return currentTexture;
    }

    public void SwitchToNextPhase()
    {
        if (hasNextPhase)
        {
            shouldSwitchPhase = true;
            nextPhase = currentPhase + 1;
        }


    }

    public void Scroll(float scrollAmmount)
    {
        mat.mainTextureOffset = new Vector2(0, mat.mainTextureOffset.y + scrollAmmount);

        float targetOffset = mat.mainTextureOffset.y + scrollAmmount;
        if (targetOffset > 0)
        {
            targetOffset--;
            mat.mainTextureOffset = new Vector2(0, targetOffset);
        }
        else if (targetOffset <= -1)
        {
            OnPhaseEnded(targetOffset + 1);
        }
        else
        {
            mat.mainTextureOffset = new Vector2(0, targetOffset);
        }


    }



    //DEBUG
    private void Start()
    {
        Init();
    }
    //DEBUG
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SwitchToNextPhase();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Scroll(debugScrollSpeed * Time.deltaTime * textureScale);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Scroll(-debugScrollSpeed * Time.deltaTime * textureScale);
        }
    }

}
