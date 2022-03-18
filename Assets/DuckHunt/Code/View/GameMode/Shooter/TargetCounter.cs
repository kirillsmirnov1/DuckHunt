using System;
using UnityEngine;
using UnityEngine.UI;

namespace DuckHunt.View.GameMode.Shooter
{
    public class TargetCounter : MonoBehaviour
    {
        [SerializeField] private Color todoColor = Color.grey;
        [SerializeField] private Color doneColor = Color.green;
        [SerializeField] private Color failedColor = Color.red;
        
        [SerializeField] private Image targetTagPrefab;

        private int _targetCount;
        private Image[] _targetTags;
        
        public void Init(int targetCount)
        {
            _targetCount = targetCount;
            _targetTags = new Image[_targetCount];
            for (int i = 0; i < _targetCount; i++)
            {
                _targetTags[i] = Instantiate(targetTagPrefab, transform);
                _targetTags[i].color = todoColor;
            }
        }

        public void SetTagsMode(TagMode mode)
        {
            for (int i = 0; i < _targetCount; i++)
            {
                SetTagMode(i, mode);
            }
        }

        public void SetTagMode(int iTag, TagMode mode)
        {
            _targetTags[iTag].color = mode switch
            {
                TagMode.ToDo => todoColor,
                TagMode.Current => Color.white,
                TagMode.Done => doneColor,
                TagMode.Failed => failedColor,
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };
        }

        public enum TagMode
        {
            ToDo,
            Current, 
            Done,
            Failed,
        }
        
    }
}
