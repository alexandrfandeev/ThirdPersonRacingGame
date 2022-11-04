namespace _Project.Scripts.GUi.Navigation.Entities
{
    public class Upgrade : NavigationButton
    {
        public override NavigationType NavType => NavigationType.Mechanic;

        public override void OnInteract()
        {
            
        }

        public override void OnClose()
        {
            
        }

        public override void SetPause()
        {
            _ownButton.interactable = false;
        }

        public override void Play()
        {
            _ownButton.interactable = true;
        }
    }
}
