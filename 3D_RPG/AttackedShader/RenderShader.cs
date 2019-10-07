using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class RenderShader : MonoBehaviour
{
    public enum eValueType
    {
        Float,
        Texture,
        Vector4,
        Matrix,
    }

    #region INSPECTOR

    public string m_sShaderPropertyName = "_RimPower";
    public eValueType m_eValueType = eValueType.Float;

    public float m_fValue = 0;
    private Texture m_refTexture;
    public Vector4 m_v4Value;
    private Matrix4x4 m_matValue;

    #endregion

    MaterialPropertyBlock m_Property = null;
    private int m_nShaderID = 0;

    public bool m_bLoop = false;

    public Renderer[] m_arrRendererChildren = null;

    private void Start()
    {
        m_Property = new MaterialPropertyBlock();
        m_nShaderID = Shader.PropertyToID(m_sShaderPropertyName);

        m_arrRendererChildren = GetComponentsInChildren<Renderer>();

        switch (m_eValueType)
        {
            case eValueType.Float:
                m_Property.SetFloat(m_nShaderID, m_fValue);
                break;
            case eValueType.Texture:
                m_Property.SetTexture(m_nShaderID, m_refTexture);
                break;
            case eValueType.Vector4:
                m_Property.SetVector(m_nShaderID, m_v4Value);
                break;
            case eValueType.Matrix:
                m_Property.SetMatrix(m_nShaderID, m_matValue);
                break;
            default:
                break;
        }
    }

    public void SetFloat(float f)
    {
        m_bLoop = true;

        m_fValue = f;
        m_Property.SetFloat(m_nShaderID, m_fValue);

        foreach (var val in m_arrRendererChildren)
        {
            val.SetPropertyBlock(m_Property);
        }
    }

    public void SetActive()
    {
        SetFloat(m_fValue);
        StartCoroutine(SetActiveFalse());
    }
    private IEnumerator SetActiveFalse()
    {
        yield return YieldReturnCaching.WaitForSeconds(1f);
        m_bLoop = false;
        Clear();
    }
    bool bUp = false;

    private void Update()
    {
        if (m_bLoop == true)
        {
            float fAdd = (bUp == true) ? 0.01f : -0.01f;
            m_fValue += fAdd;

            if (bUp == true)
            {
                if (m_fValue > 1.0f)
                {
                    bUp = !bUp;
                }
            }
            else
            {
                if (m_fValue < -1.0f)
                {
                    bUp = !bUp;
                }
            }

            SetFloat(m_fValue);
        }
    }

    public void Clear()
    {
        foreach (var val in m_arrRendererChildren)
        {
            val.SetPropertyBlock(null);
        }

    }
}
