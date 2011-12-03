using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;

namespace uTorrent.WebUI.Library.Infrastructure.MVVM
{
    public abstract class BaseModel : NotificationObject, IDisposable
    {
        #region Private Fields

        private readonly IUnityContainer _container;

        #endregion Private Fields

        #region Public Properties

        public IUnityContainer Container
        {
            get { return _container; }
        }

        #endregion Public Properties

        #region Constructors

        protected BaseModel(IUnityContainer container)
        {
            _container = container;
        }

        #endregion Constructors

        #region IDisposable

        public virtual void Dispose()
        {
            
        }

        #endregion IDisposable
    }
}
