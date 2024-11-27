    public interface IPopup
    {
        public T GetPopup<T>() where T : PopupBase<T>, IPopup;

        public T ShowPopup<T>() where T : PopupBase<T>, IPopup;
    }