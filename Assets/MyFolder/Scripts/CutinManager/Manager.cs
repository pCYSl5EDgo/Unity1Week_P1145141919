using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week.UI.SkillCutin
{
    public sealed class Manager : MonoBehaviour
    {
        public RectTransform target;
        void Start()
        {
            var tweener = target.DOMoveX(500 - target.sizeDelta.x / 2, 1);
            tweener.OnComplete(() =>
            {
                target.pivot = new Vector2(0.5f, 0.5f);
				target.position = new Vector3(500, 300, 0);
            });
        }
    }
}