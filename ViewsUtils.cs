using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class ViewsUtils
    {
        public static List<ViewPlan> GetFloorPlanViews(Document doc)
        {
            var views
               = new FilteredElementCollector(doc)
                   .OfClass(typeof(ViewPlan))
                   .Cast<ViewPlan>()
                   .Where(p => p.ViewType == ViewType.FloorPlan)
                   .ToList();
            return views;
        }
        public static List<ViewFamilyType> GetViewFamilyTypes(Document doc)
        {
            var views
               = new FilteredElementCollector(doc)
                   .OfClass(typeof(ViewFamilyType))
                   .Cast<ViewFamilyType>()                   
                   .ToList();
            return views;
        }

        public static List<View> GetLegends(Document doc)
        {
            var views
               = new FilteredElementCollector(doc)
                   .OfClass(typeof(View))
                   .Cast<View>()
                   .Where(p => p.ViewType == ViewType.Legend)
                   .ToList();
            return views;
        }

        public static List<ViewDrafting> GetDraftingViews(Document doc)
        {
            var views
               = new FilteredElementCollector(doc)
                   .OfClass(typeof(ViewDrafting))
                   .Cast<ViewDrafting>()
                   .Where(p => p.ViewType == ViewType.DraftingView)
                   .ToList();
            return views;
        }
        public static View CreateDetailedCopy(View view)
        {
            View dependentView = null;
            ElementId newViewId = ElementId.InvalidElementId;
            if (view.CanViewBeDuplicated(ViewDuplicateOption.WithDetailing))
            {
                newViewId = view.Duplicate(ViewDuplicateOption.WithDetailing);
                dependentView = view.Document.GetElement(newViewId) as View;                
            }

            return dependentView;
        }
    }
}
