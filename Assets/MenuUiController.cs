using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUiController : MonoBehaviour
{
    public string path;
    public Dropdown slicerDropdown;
    public Text slicerDescription;

    public GameConfig gameConfig;

    private List<SlicerInfo> slicerInfos;
    private int slicerIndex;
    private List<UltimateInfo> ultimateInfos;
    private int ultimateIndex;

    void Start()
    {
        slicerInfos = Resources.LoadAll<SlicerInfo>(path).ToList();
        ultimateInfos = Resources.LoadAll<UltimateInfo>(path).ToList();

        slicerDropdown = PopulateDropdown<SlicerInfo>.Populate(slicerInfos, slicerDropdown);

        for (int i = 0; i < slicerDropdown.options.Count; ++i)
        {
            if (slicerDropdown.options[i].text == gameConfig.chosenSlicer.name)
            {
                slicerIndex = i;
                break;
            }
        }

        slicerDropdown.value = slicerIndex;
        slicerDropdown.onValueChanged.Invoke(0);
    }

    class PopulateDropdown<T> where T : ScriptableObject
    {
        public static Dropdown Populate(List<T> list, Dropdown dropdown)
        {
            dropdown.options.Clear();
            foreach (var info in list)
            {
                Dropdown.OptionData data = new Dropdown.OptionData(info.name);
                dropdown.options.Add(data);
            }

            dropdown.RefreshShownValue();

            return dropdown;
        }
    };

    public void OnSlicerDropdownChanged()
    {
        gameConfig.chosenSlicer = slicerInfos[slicerDropdown.value];
        slicerDescription.text = slicerInfos[slicerDropdown.value].Description;
    }

    public void OnUltimateDropdownChanged()
    {
        return;
    }

    public void OnStartButtonPressed()
    {
        SceneManager.LoadScene("MainGame");
    }
}
