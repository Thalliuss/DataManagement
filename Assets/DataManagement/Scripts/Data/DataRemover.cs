using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DataManagement
{
    public class DataRemover : MonoBehaviour
    {
        public void ClearData()
        {
            var t_dataManager = DataManager.Instance;

            var t_parent = transform.parent;
            for (var i = 0; i < t_parent.name.ToCharArray().Length; i++)
            {
                if (char.IsDigit(t_parent.name, i) && i <= 6)
                {
                    var t_value = (int)char.GetNumericValue(t_parent.name.ToCharArray()[i]);

                    Debug.Log("Cleaning Data from: " + Application.persistentDataPath + "/" + t_dataManager.SaveReferences.saveData[t_value]);
                    FileUtil.DeleteFileOrDirectory(Application.persistentDataPath + "/" + t_dataManager.SaveReferences.saveData[t_value]);

                    if (t_dataManager.DataReferences.ID == t_dataManager.SaveReferences.saveData[t_value])
                        t_dataManager.DataReferences.ID = t_dataManager.DataReferences.initialID;

                    t_dataManager.SaveReferences.load.options.RemoveAt(t_value);
                    t_dataManager.SaveReferences.Init();
                }
            }
        }
    }
}
