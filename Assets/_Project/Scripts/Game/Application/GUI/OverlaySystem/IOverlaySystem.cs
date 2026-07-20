using Game.Application.Asset;

namespace Game.Application.GUI.OverlaySystem
{
    public interface IOverlaySystem
    {
        void CreateOverlay(OverlayType type);
        void CreateOverlay(AssetSUID assetId);
    }
}