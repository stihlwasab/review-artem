namespace Lanit.Ksup.BLL.PermissionAssignerProvider
{
    using Microsoft.SharePoint;

    /// <summary>
    /// Интерфейс сервисов назначения прав на элементы
    /// </summary>
    public interface IElementPermissionsAssigner
    {
        /// <summary>
        /// Айди контентного типа
        /// </summary>
        SPContentTypeId ContentTypeId { get; }

        /// <summary>
        /// Назначить права на элемент
        /// </summary>
        /// <param name="item">Элемент</param>
        void ApplyPermission(SPListItem item);
    }
}