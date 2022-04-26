using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 이펙트의 Data들을 읽고 불러오는, (get, load) 데이타 홀더 같은 개념  
/// </summary>
public class DataManager : MonoBehaviour
{
    private static EffectData effectData = null;
    // Start is called before the first frame update
    void Start()
    {
        if(effectData == null)
        {
            effectData = ScriptableObject.CreateInstance<EffectData>();
            effectData.LoadData();
        }
    }

    public static EffectData EffectData()
    {
        if (effectData == null)
        {
            effectData = ScriptableObject.CreateInstance<EffectData>();
            effectData.LoadData();
        }
        return effectData;
    }

}
