using UnityEngine;
using UnityEditor;

public class RemoveMissingScripts : Editor
{
    [MenuItem("Tools/Remove Missing Scripts Recursively")]
    public static void RemoveMissingScriptsa()
    {
        // ���� ���� ��� GameObject�� ������� �մϴ�.
        foreach (GameObject go in GetAllObjectsInScene(true))
        {
            RemoveMissingScriptsFromGameObject(go);
        }
    }

    static void RemoveMissingScriptsFromGameObject(GameObject go)
    {
        // ������ ��ũ��Ʈ ����
        int removedCount = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
        if (removedCount > 0)
        {
            Debug.Log($"{removedCount} missing scripts removed from {go.name}", go);
        }

        // �ڽ� GameObject�鿡 ���ؼ��� ��������� ó��
        foreach (Transform child in go.transform)
        {
            RemoveMissingScriptsFromGameObject(child.gameObject);
        }
    }

    static GameObject[] GetAllObjectsInScene(bool includeInactive)
    {
        return GameObject.FindObjectsOfType<GameObject>(includeInactive);
    }
}