namespace Lanit.Ksup.BLL.Service.ElementAssignerService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Lanit.Ksup.BLL.PermissionAssignerProvider;
    using Lanit.Ksup.Constant;
    using Microsoft.SharePoint;
    using Se.Portal.Core.Constant;
    using Se.Portal.Core.Utility;

    /// <summary>
    /// Назначить права на элемент списка: "Контракты", "Зависимые контракты"
    /// </summary>
    public class ContractPermissionService : IElementPermissionsAssigner
    {
        /// <summary>
        /// При инициализации получаем веб с привилегиями, создателя элемента и его роли
        /// </summary>
        /// <param name="elevatedWeb">Веб</param>
        /// <param name="author">Создатель элемента</param>
        public ContractPermissionService(SPWeb elevatedWeb, SPUser author)
        {
            this.ElevatedWeb = elevatedWeb;
            this.Author = author;
            this.AuthorRoles = elevatedWeb.GetAllRoles(author).ToList();
        }

        /// <summary>
        /// Айди контентного типа
        /// </summary>
        public SPContentTypeId ContentTypeId => PortalConstant.ContentType.Contract.Id;

        private SPWeb ElevatedWeb { get; }

        private SPUser Author { get; }

        private List<SPRoleDefinition> AuthorRoles { get; }

        /// <summary>
        /// Назначить права на элемент
        /// </summary>
        /// <param name="item">Элемент</param>
        public void ApplyPermission(SPListItem item)
        {
            if (!this.AuthorRoles.Any(x => x.Name.Equals(PortalConstant.Role.Seller, StringComparison.InvariantCulture)))
            {
                return;
            }

            item.BreakRoleInheritance(true);
            item.AddPermission(this.Author, this.ElevatedWeb, PortalConstant.Role.Archivator);

            var parentLookupValue = item.LookupValue(PortalConstant.Field.KsupContract.Id);
            if (parentLookupValue?.LookupValue == null)
            {
                return;
            }

            this.InitIfParentitemIsMine(item, parentLookupValue);
        }

        private void InitIfParentitemIsMine(SPListItem item, SPFieldLookupValue parentLookupValue)
        {
            var parentList = item.ParentList;
            var parentItem = parentList.GetItemById(parentLookupValue.LookupId);
            var parentItemAuthor = parentItem.UserValue(this.ElevatedWeb, CoreConstant.Field.Author.Id);

            if (!this.Author.LoginName.Equals(parentItemAuthor.User.LoginName))
            {
                return;
            }

            item.AddPermission(this.Author, this.ElevatedWeb, PortalConstant.Role.Reader);
        }
    }
}
