namespace MyAssets.base_building.resource_management
{
    public interface IUiController
    {
        void InitMenus();
        bool EnoughResources(int index);
    }
}