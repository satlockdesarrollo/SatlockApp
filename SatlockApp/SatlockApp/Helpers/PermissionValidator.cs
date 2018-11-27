namespace SatlockApp.Helpers
{
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using SatlockApp.Renders;

    public class PermissionValidator
    {
        public async Task<PermissionStatus> validatePermission(Permission permission)
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            bool request = false;
            if (permissionStatus == PermissionStatus.Denied)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {

                    var title = $"{permission} Permiso";
                    var question = $"Para usar este plugin el {permission} se requiere permiso. Por favor, vaya a Configuración y encienda {permission} para la aplicación.";
                    var positive = "Configuración";
                    var negative = "Ahora no";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return permissionStatus;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    return permissionStatus;
                }

                request = true;

            }

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                var newStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);

                if (!newStatus.ContainsKey(permission))
                {
                    return permissionStatus;
                }

                permissionStatus = newStatus[permission];

                if (newStatus[permission] != PermissionStatus.Granted)
                {
                    permissionStatus = newStatus[permission];
                    var title = $"{permission} Permiso";
                    var question = $"Para utilizar el plugin {permission} se requiere permiso.";
                    var positive = "Configuración";
                    var negative = "Ahora No" +
                        "";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return permissionStatus;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }
                    return permissionStatus;
                }

                DependencyService.Get<ToastMessage>().Show("Permiso concedido, vuelve a inicar la acción");

            }

            return permissionStatus;
        }

    }
}
