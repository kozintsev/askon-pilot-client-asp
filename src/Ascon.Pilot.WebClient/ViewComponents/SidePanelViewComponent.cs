﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ascon.Pilot.Core;
using Ascon.Pilot.WebClient.Extensions;
using Ascon.Pilot.WebClient.ViewModels;
using Microsoft.AspNet.Mvc;

namespace Ascon.Pilot.WebClient.ViewComponents
{
    public class SidePanelViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Guid? id)
        {
            return await Task.Run(() => GetSidePanel(id)) ;
        }

        public IViewComponentResult GetSidePanel(Guid? id)
        {
            id = id ?? DObject.RootId;
            
            var serverApi = HttpContext.Session.GetServerApi();
            var rootObject = serverApi.GetObjects(new[] { id.Value }).First();

            var mTypes = HttpContext.Session.GetMetatypes();
            var model = new SidePanelViewModel
            {
                ObjectId = id.Value,
                Types = mTypes,
                Items = new List<SidePanelItem>()
            };
            
            var prevId = rootObject.Id;
            var parentId = rootObject.Id;
            do
            {
                var parentObject = serverApi.GetObjects(new[] {parentId}).First();
                var parentChildsIds = parentObject.Children
                                        .Where(x => mTypes[x.TypeId].Children.Any())
                                        .Select(x => x.ObjectId).ToArray();
                if (parentChildsIds.Length != 0)
                {
                    var parentChilds = serverApi.GetObjects(parentChildsIds);
                    var subtree = model.Items;
                    model.Items = new List<SidePanelItem>(parentChilds.Count);
                    foreach (var parentChild in parentChilds)
                    {
                        model.Items.Add(new SidePanelItem
                        {
                            Type = mTypes[parentChild.TypeId],
                            DObject = parentChild,
                            SubItems = parentChild.Id == prevId ? subtree : null,
                            Selected = parentChild.Id == id
                        });
                    }
                }
                
                prevId = parentId;
                parentId = parentObject.ParentId;
            } while (parentId != Guid.Empty);
            return View(model);
        }
    }
}
