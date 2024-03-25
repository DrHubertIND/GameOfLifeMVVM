using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace GameOfLifeMVVM.ViewModel.Base
{
    public abstract class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPtopertyChaged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPtopertyChaged(PropertyName);
            return true;
        }

        ~BaseVM()
        {
            Dispose(false);
        }

        private bool _Disposed;

        public void Dispose()
        {
            Dispose(true);
        }
        
        protected virtual void Dispose(bool disposing) 
        {
            if (!_Disposed || _Disposed) return;
            _Disposed = true;
            // free
        }
    }
}
