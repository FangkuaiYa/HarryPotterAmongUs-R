﻿using HarmonyLib;
using Reactor.Utilities.Attributes;
using Reactor.Utilities.Extensions;
using System;
using TMPro;
using UnityEngine;

namespace HarryPotter.Classes.UI
{
    [RegisterInIl2Cpp]
    class Tooltip : MonoBehaviour
    {
        public Tooltip(IntPtr ptr) : base(ptr)
        {
        }

        public GameObject TooltipObj { get; set; }
        public TextMeshPro TooltipTMP { get; set; }
        public RectTransform TooltipTransform { get; set; }
        public MeshRenderer TooltipRenderer { get; set; }
        public bool Enabled { get; set; }
        public string TooltipText { get; set; }

        private void Start()
        {
            Enabled = true;

            TooltipObj = new GameObject().DontDestroy();
            TooltipObj.layer = 5;

            TooltipTMP = TooltipObj.AddComponent<TextMeshPro>();
            TooltipTMP.fontSize = 1.7f;
            TooltipTMP.alignment = TextAlignmentOptions.BottomLeft;
            TooltipTMP.overflowMode = TextOverflowModes.Overflow;
            TooltipTMP.maskable = false;
            TooltipTMP.fontMaterial.SetFloat("_UnderlayDilate", 0.75f);

            TooltipRenderer = TooltipObj.GetComponent<MeshRenderer>();
            TooltipRenderer.sortingOrder = 1000;

            TooltipTransform = TooltipObj.GetComponent<RectTransform>();
            TooltipObj.SetActive(false);
        }

        public void OnDisable()
        {
            if (TooltipObj == null) return;
            TooltipObj.SetActive(false);
        }

        public void OnDestroy()
        {
            if (TooltipObj == null) return;
            TooltipObj.SetActive(false);
            TooltipObj.Destroy();
        }

        public void LateUpdate()
        {
            if (TooltipObj == null || TooltipTMP == null || TooltipTransform == null) return;

            TooltipTransform.sizeDelta = TooltipTMP.GetPreferredValues(TooltipText);
            TooltipTMP.text = "<#EEFFB3FF>" + TooltipText;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TooltipObj.transform.position = new Vector3(mousePosition.x + (TooltipTMP.renderedWidth / 2) + 0.1f, mousePosition.y);
        }

        public void FixedUpdate()
        {
            if (TooltipObj == null) return;
            TooltipObj.SetActive(false);
            if (Input.GetMouseButton(1))
                TooltipObj.SetActive(true);
        }
    }
}