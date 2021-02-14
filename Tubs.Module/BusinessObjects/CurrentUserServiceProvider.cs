using System;
using DevExpress.Persistent.Base;
namespace Tubs.Module.BusinessObjects
{
    public class CurrentUserServiceProvider<T>
    {
        private static Func<T> factory;
        public static void AddService(Func<T> factory)
        {
            CurrentUserServiceProvider<T>.factory = factory;
        }
        public static T GetService()
        {
            var vm = ValueManager.GetValueManager<T>(typeof(CurrentUserServiceProvider<T>).FullName);
            T result;
            if (vm.CanManageValue)
            {
                result = vm.Value;
                if (Equals(result, default(T)))
                {
                    result = factory.Invoke();
                    vm.Value = result;
                }
            }
            else
            {
                result = factory.Invoke();
            }
            return result;
        }
    }
}