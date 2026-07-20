using System;
using UnityEngine;

namespace Game.Application.Asset
{
    [CreateAssetMenu(fileName = "SUID_Asset_", menuName = "_Project/Asset/SUID")]
    public class AssetSUID : ScriptableObject
    {
        [SerializeField] private string _uid;

        public string UID => _uid;

        private void Reset()
        {
            GenerateUID();
        }

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(_uid))
                GenerateUID();
        }

        private void GenerateUID()
        {
            _uid = Guid.NewGuid().ToString();
        }
    }
}