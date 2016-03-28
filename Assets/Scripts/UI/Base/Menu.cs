using UnityEngine;
using System.Collections;

namespace Sol
{
    public class Menu : MonoBehaviour
    {
        public delegate void MenuOpen();
        public static event MenuOpen OnMenuOpen;

        public delegate void MenuClose();
        public static event MenuClose OnMenuClose;

        public AudioClip clickButtonEffect;
        public AudioClip openEffect;
        public AudioClip closeEffect;

        public GameObject root;
        public bool stopsMovement = true;

        protected bool isActive = false;
        protected SoundManager cachedSoundManager;


        public SoundManager CachedSoundManager
        {
            get { return (cachedSoundManager != null) ? cachedSoundManager : cachedSoundManager = GameManager.Get<SoundManager>();  }
        }


        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public virtual void Open()
        {
            if (!IsActive)
            {
                if (openEffect != null) CachedSoundManager.Play(openEffect);

                isActive = true;
                root.SetActive(true);
                if (stopsMovement && OnMenuClose != null) OnMenuOpen();
            }
        }


        public virtual void Close()
        {
            if (IsActive)
            {
                if (closeEffect != null) CachedSoundManager.Play(closeEffect);

                isActive = false;
                root.SetActive(false);
                if (stopsMovement) OnMenuClose();
            }
        }


        public virtual bool ClickButton()
        {
            if (clickButtonEffect != null)
            {
                CachedSoundManager.Play(clickButtonEffect);
                return true;
            }

            return false;
        }
    }
}