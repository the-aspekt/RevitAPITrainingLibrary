using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class RoofsUtils
    {
        internal class RoodsSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element elem)
            {
                return elem.GetType().Equals(typeof(RoofBase));
            }

            public bool AllowReference(Reference reference, XYZ position)
            {
                return false;
            }
        }

        public static List<RoofType> GetTypes(Document doc)
        {
            var roofTypes =
                new FilteredElementCollector(doc)
                    .OfClass(typeof(RoofType))
                    .Cast<RoofType>()
                    .ToList();
            return roofTypes;
        }

    }
}
