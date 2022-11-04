using _Project.Scripts.Development;

namespace _Project.Scripts.GUi.Navigation.Entities
{
    public class Shop : NavigationButton
    {
        public override NavigationType NavType => NavigationType.Mechanic;
        

        public override void OnInteract()
        {
            UiUtilities.ScaleAndFade(() =>
            {
                PauseManager.Current.StartPause();
            }, _rect, _background, 1.2f, 0.6f, _animationDuration);
        }

        public override void OnClose()
        {
            UiUtilities.ScaleAndFade(() =>
                {
                    PauseManager.Current.StopPause();
                },
                _rect, _background, 0f, 0f, _animationDuration);
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