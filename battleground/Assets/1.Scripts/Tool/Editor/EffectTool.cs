using UnityEngine;
using UnityEditor; // Editor라는 폴더 아래에 스크립트 파일이 구문을 쓸 수 있음.
using System.Text;
using UnityObject = UnityEngine.Object;

/// <summary>
/// EffectClip 프로퍼티를 수정하기 위한 클래스이다. -> tool 
/// </summary>
public class EffectTool : EditorWindow  // Tool 안에 window를 띄울 수 있음.
{
    //UI 그리는데 필요한 변수들.
    public int uiWidthLarge = 300;
    public int uiWidthMiddle = 200;
    private int selection = 0;
    private Vector2 SP1 = Vector2.zero; //SP = ScrollPosition
    private Vector2 SP2 = Vector2.zero;

    //이펙트 클립
    private GameObject effectSource = null;
    //이펙트 데이터
    private static EffectData effectData;

    //EditorWindow를 띄우기 위한 코드
    [MenuItem("Tools/Effect Tool")]
    static void Init()
    {
        effectData = ScriptableObject.CreateInstance<EffectData>();
        effectData.LoadData();

        EffectTool window = GetWindow<EffectTool>(false, "Effect Tool");
        window.Show();
    }


    private void OnGUI()
    {
        if(effectData == null)
        {
            return;
        }
        EditorGUILayout.BeginVertical();
        {
            UnityObject source = effectSource;
            EditorHelper.EditorToolTopLayer(effectData, ref selection, ref source, this.uiWidthMiddle); // source에 Sound, Audio 등 어떤 오브젝트가 들어갈지 모르게 때문에 45번 줄에 source를 캐스팅 해줌.
            effectSource = (GameObject)source; // 캐스팅 -> 명시적변환 
        }
        EditorGUILayout.EndVertical();
    }
}
