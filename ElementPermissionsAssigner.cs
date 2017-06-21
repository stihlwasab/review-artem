namespace Lanit.Ksup.BLL.PermissionAssignerProvider
{
    using System.Collections.Generic;
    using System.Linq;
    using Lanit.Ksup.BLL.Service.ElementAssignerService;
    using Microsoft.SharePoint;

    /// <summary>
    /// Сервис назначения прав в зависимости от контентого типа
    /// </summary>
    public class ElementPermissionsAssigner : BaseElementPermissionsAssigner
    {
        private List<IElementPermissionsAssigner> assignerServices = new List<IElementPermissionsAssigner>();

        /// <summary>
        /// Создаёт экземпляр сервиса и экземпляры сервисов для назначения прав
        /// </summary>
        /// <param name="web">Веб</param>
        /// <param name="author">Создатель элемента</param>
        public ElementPermissionsAssigner(SPWeb web, SPUser author) : base(web)
        {
            this.assignerServices.Add(new SalePermissionService(this.Web, author));
            this.assignerServices.Add(new ContractPermissionService(this.Web, author));
            this.assignerServices.Add(new RefElementPermissionService(this.Web, author));
            this.assignerServices.Add(new SellerCustomerPermissionService(this.Web, author));
        }

        /// <summary>
        /// Получить сервис назначения прав
        /// </summary>
        /// <param name="id">Айди контентного типа элемента</param>
        /// <returns><see cref="IElementPermissionsAssigner"/></returns>
        public IElementPermissionsAssigner GetAssignerService(SPContentTypeId id)
        {
            return this.assignerServices.FirstOrDefault(x => id.IsChildOf(x.ContentTypeId));
        }
    }
}