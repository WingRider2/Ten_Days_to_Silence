using Codice.CM.Client.Differences.Graphic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEngine;

public static class ExcelSOGenerator
{ 
    public static void GenerateFromSheet(DataTable table, string sheetName, string scriptOutputPath, string assetOutputPath)
    {
        switch(sheetName)
        {
            case "UnitData":
                // var conditions = ScriptableObject.CreateInstance<ConditionDataTable>();
                // string enemyAssetPath = $"{assetOutputPath}/ConditionDataTable.asset";
                // AssetDatabase.CreateAsset(conditions, enemyAssetPath);
                // Debug.Log($"✅ {sheetName} SO 생성됨: {enemyAssetPath}");
                break;

            case "ActiveItemData":
                // ClassGenerator.GenerateDataTableClassFromTable(table, sheetName, scriptOutputPath);
                //
                // var activeItemDataTable = ScriptableObject.CreateInstance<ActiveItemDataTable>();
                //
                // for(int i = 2; i < table.Rows.Count; i++)
                // {
                //     var row = table.Rows[i];
                //     var activeItem = new ActiveItemData();
                //     activeItem.ID = int.Parse(row[0].ToString());
                //
                //     activeItemDataTable.dataList.Add(activeItem);
                // }
                //
                // string activeItemAssetPath = $"{assetOutputPath}/{table.ToString()}Table.asset";
                // AssetDatabase.CreateAsset(activeItemDataTable, activeItemAssetPath);
                // Debug.Log($"✅ {sheetName} SO 생성됨: {activeItemAssetPath}");
                break;
            case "Enums":
                break;
            default:
                Debug.LogError($"{sheetName}이상한 반복 확인");
                break;

        }
    }
    /// <summary>
    /// 리스트 파싱
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    private static List<int> ParseIntListFromCell(object cell)
    {
        var raw = cell.ToString();
        Debug.Log($"🧪 Raw cell: '{raw}'");
        var split = raw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        var list = new List<int>();

        foreach(var part in split)
        {
            if(int.TryParse(part.Trim(), out int val))
                list.Add(val);
        }

        return list;
    }

    /// <summary>
    /// Enum 파싱
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    private static T ParseEnumFromCell<T>(object cell) where T : System.Enum
    {
        var raw = cell.ToString().Trim();
        try
        {
            // Enum.Parse를 사용하여 문자열을 Enum 값으로 변환
            return (T)System.Enum.Parse(typeof(T), raw, true); // true는 대소문자 무시
        }
        catch(System.ArgumentException)
        {
            Debug.LogError($"Failed to parse '{raw}' as enum type {typeof(T).Name}. Returning default value.");
            return default(T); // 파싱 실패 시 Enum의 기본값 (보통 0번째 값) 반환
        }
    }
}
