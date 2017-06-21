namespace Lanit.Ksup.BLL.Service.ElementAssignerService
{
    using System.Collections.Generic;
    using System.Linq;
    using Lanit.Ksup.BLL.PermissionAssignerProvider;
    using Lanit.Ksup.Constant;
    using Microsoft.SharePoint;
    using Se.Portal.Core.Utility;

    /// <summary>
    /// Назначить права на элемент списка: "Взаимодействия с заказчиками"
    /// </summary>
    public class SellerCustomerPermissionService : IElementPermissionsAssigner
    {
        /// <summary>
        /// При инициализации получаем веб с привилегиями, создателя элемента и его роли
        /// </summary>
        /// <param name="elevatedWeb">Веб</param>
        /// <param name="author">Создатель элемента</param>
        public SellerCustomerPermissionService(SPWeb elevatedWeb, SPUser author)
        {
            this.ElevatedWeb = elevatedWeb;
            this.Author = author;
            this.AuthorRoles = elevatedWeb.GetAllRoles(author).ToList();
        }

        /// <summary>
        /// Айди контентного типа
        /// </summary>
        public SPContentTypeId ContentTypeId => PortalConstant.ContentType.SellerCustomer.Id;

        private SPWeb ElevatedWeb { get; }

        private SPUser Author { get; }

        private List<SPRoleDefinition> AuthorRoles { get; }

        /// <summary>
        /// Назначить права на элемент
        /// </summary>
        /// <param name="item">Элемент</param>
        public void ApplyPermission(SPListItem item)
        {
            item.BreakRoleInheritance(true);
            item.AddPermission(this.Author, this.ElevatedWeb, PortalConstant.Role.Editor);
        }
    }
}