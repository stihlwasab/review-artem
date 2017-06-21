namespace Lanit.Ksup.BLL.PermissionAssignerProvider
{
    using Microsoft.SharePoint;
    using Se.Portal.Core.Service;

    /// <summary>
    /// Базовый класс для назначения прав на элементы
    /// </summary>
    public abstract class BaseElementPermissionsAssigner : BasePermissionService
    {
        /// <summary>
        /// Создаёт экземпляр класса
        /// </summary>
        /// <param name="web">Веб</param>
        protected BaseElementPermissionsAssigner(SPWeb web) : base(web)
        {
        }
    }
}