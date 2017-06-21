/// <summary>Asynchronous After event that occurs after a new item has been added to its containing object.</summary>
        /// <param name="properties">An <see cref="T:Microsoft.SharePoint.SPItemEventProperties"/> object that represents properties of the event handler.</param>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            var item = properties.ListItem;
            var author = item.UserValue(properties.Web, CoreConstant.Field.Author.Id);

            var permissionsAssigner = new ElementPermissionsAssigner(properties.Web, author.User);
            item = permissionsAssigner.GetItem(item);

            var assignerService = permissionsAssigner.GetAssignerService(item.ContentTypeId);
            assignerService?.ApplyPermission(item);
        }